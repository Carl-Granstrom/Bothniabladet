using System;
using System.Collections.Generic;
using Bothniabladet.Data;
using System.ComponentModel.DataAnnotations;


namespace Bothniabladet.Models.ImageModels
{
    public class ImageSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public List<string> Keywords { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public static ImageDetailViewModel FromImage(Image image)
        {
            List<string> Keywords = new List<string>();
            //add null check if needed
            foreach (Keyword word in image.Keywords)
            {
                Keywords.Add(word.Word);
            }
            return new ImageDetailViewModel
            {
                Id = image.ImageId,
                Name = image.ImageTitle,
                Section = image.Section.ToString(),
                Keywords = Keywords,
                Date = image.Issue
            };
        }
    }
}

