using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


//this class holds the form data from the ImageModels-Create view.
namespace Bothniabladet.Models.ImageModels
{
    public class CreateImageCommand
    {
        public string ImageTitle { get; set; }
        public int BasePrice { get; set; }
        //I have to figure out how to not view the time here. Trying this.
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:MM tt}", ApplyFormatInEditMode = true)]
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

        public byte[] ImageDataByte { get; set; }

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
                ImageData = ImageDataByte,
                //placeholders creating empty licence and metadata objects
                ImageLicense = new ImageLicense(),
                ImageMetaData = new ImageMetaData()
            };
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
