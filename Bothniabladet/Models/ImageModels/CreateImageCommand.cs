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
using ExifLib;


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
        public Image ToImage()
        {
            //placeholder before I cba to do the string conversion to correct enum.
            NewsSection TmpSection = NewsSection.NEWS;

            return new Image
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
            var jpgInfo = ExifReader.ReadJpeg(image);
            if (jpgInfo != null)
            {
                var GPSLatitude = jpgInfo.GpsLatitude;
                var GPSLongitude = jpgInfo.GpsLongitude;
                DateTime dateTaken = DateTime.MinValue; //if no datetime exif exists then set this value to the minimum, might need to think this though.
                if (jpgInfo.DateTime != null)
                {
                    dateTaken = DateTime.Parse(jpgInfo.DateTime);
                }
                int width = jpgInfo.Width;
                int height = jpgInfo.Height;
                int fileSize = jpgInfo.FileSize;
                extractedMetaData.Height = height;
                extractedMetaData.Width = width;
                extractedMetaData.FileSize = fileSize;
                extractedMetaData.DateTaken = dateTaken;
                //Placeholder until I can figure out the format of the JpegInfo.GpsLatitude, why is it an array of doubles?
                extractedMetaData.Location = new Point(1, 1); 
            }
            else
            {
                //This might need thinking through
                extractedMetaData.Height = -1;
                extractedMetaData.Width = -1;
                extractedMetaData.FileSize = -1;
                extractedMetaData.DateTaken = DateTime.MinValue;
                extractedMetaData.Location = new Point(-1, -1);

            }

            return extractedMetaData;
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
