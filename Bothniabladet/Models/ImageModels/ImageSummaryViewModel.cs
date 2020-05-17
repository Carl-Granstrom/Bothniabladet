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
        public string ThumbnailDataString { get; set; }
        public string ImageDataString { get; set; }
        public string Section { get; set; }
        public List<string> Keywords { get; set; }
        public ICollection<ImageKeyword> KeywordLink { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public static ImageSummaryViewModel FromImage(Image image)
        {
            List<string> Keywords = new List<string>();
            //add null check if needed
            foreach (ImageKeyword imageKeyword in image.KeywordLink)
            {
                Keywords.Add(imageKeyword.Keyword.Word);
            }
            return new ImageSummaryViewModel
            {
                Id = image.ImageId,
                Name = image.ImageTitle,
                ThumbnailDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.Thumbnail)),
                ImageDataString = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                Section = image.Section.ToString(),
                Keywords = Keywords,
                Date = image.Issue
            };
        }
    }
}

