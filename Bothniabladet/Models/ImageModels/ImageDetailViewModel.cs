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
        public string Section { get; set; }
        private List<string> keywords;
        public string KeywordsString
        {
            get
            {
                string concatString = String.Join(", ", keywords);

                return concatString;
            }

        }
        public List<string> Keywords { get; set; }
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
