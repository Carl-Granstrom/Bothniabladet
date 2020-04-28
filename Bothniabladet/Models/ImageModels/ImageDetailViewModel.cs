using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Bothniabladet.Models.ImageModels
{
    //this class is supposed to actually view the image and all the data, for now it's jus ta copy of the summary class
    public class ImageDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageDataString { get; set; }
        public string Section { get; set; }
        public List<string> Keywords { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string FileFormat { get; set; }
        public string GPS { get; set; }
        public string noGPS = "No GPS coordinates for this image";

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
                ImageDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                Section = image.Section.ToString(),
                Keywords = Keywords,
                Date = image.Issue,
                Height = image.ImageMetaData.Height,
                Width = image.ImageMetaData.Width,
                FileFormat = image.ImageMetaData.FileFormat,
                GPS = image.ImageMetaData.Location.ToString()
            };
        }
    }
}
