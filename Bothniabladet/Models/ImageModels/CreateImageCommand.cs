using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using NetTopologySuite.Geometries;
using XmpCore;
using System.Drawing;
using System.Drawing.Imaging;


//this class holds the form data from the ImageModels-Create view.
//I think it probably exposes way to much logic to the users, but can't wrap my head around exactly how I should have done it.
namespace Bothniabladet.Models.ImageModels
{
    public class CreateImageCommand
    {
        public string ImageTitle { get; set; }
        public int BasePrice { get; set; }
        //I have to figure out how to not view the time here. Trying this.
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Issue { get; set; }
        public string Section { get; set; } //this field will be populated from the Select Tag Helper in the view
        //this need to be populated from the databse(in the controller?) but using PH for now
        //this uses SelectListItems and they need to be converted to the enums again
        public List<SelectListItem> Sections { get; } = new List<SelectListItem>
        {
            new SelectListItem() { Value = "CULTURE", Text = "CULTURE" },
            new SelectListItem() { Value = "NEWS", Text = "NEWS" },
            new SelectListItem() { Value = "ECONOMY", Text = "ECONOMY" },
            new SelectListItem() { Value = "SPORTS", Text = "SPORTS" }
        };

        //still need to add information needed by ImageLicense and ImageMetaData


        [BindProperty]
        public ImageData ImageData { get; set; }

        public MemoryStream ImageMemoryStream { get; set; }

        //this method will eventually handle the metadata extraction and the creation of a new image from the command object.
        public Data.Image ToImage()
        {
            //placeholder before I cba to do the string conversion to correct enum.
            NewsSection TmpSection = NewsSection.NEWS;

            return new Data.Image
            {
                ImageTitle = ImageTitle,
                BasePrice = BasePrice,
                Issue = Issue,
                Section = TmpSection,
                ImageData = ImageMemoryStream.ToArray(),
                //placeholders ImageLicense
                ImageLicense = new ImageLicense() { LicenceType = ImageLicense.LicenseType.LICENSED, UsesLeft = 3},
                //Metadata extraction hopefully works now
                ImageMetaData = ExtractMetaData(ImageMemoryStream)
            };
        }

        //Extract image metadata to ImageMetaData objects using ExifLib
        //we could extract a lot more metadata from exif tags
        private ImageMetaData ExtractMetaData(MemoryStream image)
        {
            ImageMetaData extractedMetaData = new ImageMetaData();


            System.Drawing.Image drawingImage = System.Drawing.Image.FromStream(image);
            PropertyItem[] propItems = drawingImage.PropertyItems;
            //The following code extracts the longitude and latitude out from the extremely convoluted PropertyItem-array
            byte[] latitudeRational = new byte[] { 0, 0 };
            byte[] longitudeRational = new byte[] { 0, 0 };
            foreach (PropertyItem property in propItems)
            {
                //check for Latitude property (https://docs.microsoft.com/sv-se/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions#propertytaggpslatitude)
                if (property.Id == 0x0002)
                {
                    latitudeRational = property.Value;
                }
                //check for Longitude property (https://docs.microsoft.com/sv-se/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions#propertytaggpslongitude)
                if (property.Id == 0x0004)
                {
                    longitudeRational = property.Value;
                }
            }
            extractedMetaData.Location = new NetTopologySuite.Geometries.Point(CoordinateToDouble(latitudeRational), CoordinateToDouble(longitudeRational));
            extractedMetaData.Height = drawingImage.Height;
            extractedMetaData.Width = drawingImage.Width;
            extractedMetaData.FileFormat = drawingImage.RawFormat.ToString();

            return extractedMetaData;
        }

        //This methods extracts degrees, minutes and seconds but only returns the degrees. We could get a lot more fancy with our coordinates.
        private double CoordinateToDouble(byte[] rational)
        {
            uint degreesNumerator = BitConverter.ToUInt32(rational, 0);
            uint degreesDenominator = BitConverter.ToUInt32(rational, 4);
            uint minutesNumerator = BitConverter.ToUInt32(rational, 8);
            uint minutesDenominator = BitConverter.ToUInt32(rational, 12);
            uint secondsNumerator = BitConverter.ToUInt32(rational, 16);
            uint secondsDenominator = BitConverter.ToUInt32(rational, 20);
            if (degreesDenominator == 0) { return 0; }     //could also make this harder for myself by returning NaN and handling it further up.
            return degreesNumerator / degreesDenominator;
        }
    }



    //strange name bc copied from documentation, might refactor later
    public class ImageData
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
