using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;


namespace Bothniabladet.Models.ImageModels
{
    public class ImageSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public DateTime Date { get; set; }

        public static ImageSummaryViewModel FromImage(Image image)
        {
            return new ImageSummaryViewModel
            {
                Id = image.ImageId,
                Name = image.ImageTitle,
                Section = image.Section.ToString(),
                Date = image.Issue
            };
        }
    }
}
