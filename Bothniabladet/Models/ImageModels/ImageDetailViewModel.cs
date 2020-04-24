using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;

namespace Bothniabladet.Models.ImageModels
{
    public class ImageDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public DateTime Date { get; set; }

        public static ImageDetailViewModel FromImage(Image image)
        {
            return new ImageDetailViewModel
            {
                Id = image.ImageId,
                Name = image.ImageTitle,
                Section = image.Section.ToString(),
                Date = image.Issue
            };
        }
    }
}
