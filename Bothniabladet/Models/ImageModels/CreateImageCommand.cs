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
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Title must start with a capital letter")]
        [StringLength(40, ErrorMessage = "Title can be no more than 40 characters long")]
        [Display(Name = "Image Title")]
        public string ImageTitle { get; set; }
        [Required]
        public int BasePrice { get; set; }
        //I have to figure out how to not view the time here. Trying this.
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Issue { get; set; }

        [Required]
        public NewsSection Section { get; set; } //this field will be populated from the Select Tag Helper in the view
        //this need to be populated from the databse(in the controller?) but using PH for now
        //this uses SelectListItems and they need to be converted to the enums again
        public ICollection<SelectListItem> Sections { get; set; }

        [Required]
        [BindProperty]
        public ImageData ImageData { get; set; }
        //not sure that this is a good way to use a stream. Refactor.
        public MemoryStream ImageMemoryStream { get; set; }

        public ICollection<string> Keywords { get; set; }

        /*METHODS*/

        //this method handles the metadata extraction and the creation of a new image from the command object.
        //change it so it matches EditedImage method instead, which is cleaner and siposes of streams properly since this implementation might cause a memory leak.
        public Data.Image ToImage()
        {
            //ICollection<EditedImage> editedImages = new List<EditedImage>();
            Data.Image image = new Data.Image
            {
                ImageTitle = ImageTitle,
                BasePrice = BasePrice,
                EditedImages = new List<EditedImage>(),
                Issue = Issue,
                Section = Section,
                Thumbnail = null,
                ImageData = ImageMemoryStream.ToArray(),
                //placeholders ImageLicense
                ImageLicense = new ImageLicense() { LicenceType = ImageLicense.LicenseType.LICENSED, UsesLeft = 3 },
                //Metadata extraction
                ImageMetaData = ExtractMetaData(ImageMemoryStream),
            };


            //This is all a bit convoluted, I'm sure it could be refactored to be a lot neater and clearer
            //Get a thumbnail and put it in image.Thumbnail
            //the new memorystream might potentially cause a mem leak, I'm not sure about streams. Should work a "using" in there.
            System.Drawing.Image tmpThumb = System.Drawing.Image.FromStream(new MemoryStream(image.ImageData)).GetThumbnailImage(50, 50, null, IntPtr.Zero);
            ImageConverter _converter = new ImageConverter();
            image.Thumbnail = (byte[])_converter.ConvertTo(tmpThumb, typeof(byte[]));

            return image;
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
            //check that the length of the rational is correct, if not, return 0
            if (rational.Length != 24){ return 0; }
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
