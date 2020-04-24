using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bothniabladet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Bothniabladet.Models.ImageModels
{
    public class CreateImageViewModel
    {
        public byte[] ImageData { get; set; }
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
    }
}
