using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using Bothniabladet.Models.ImageModels;
using Microsoft.Extensions.Logging;

namespace Bothniabladet.Services
{
    public class ImageService
    {
        readonly AppDbContext _context;
        readonly ILogger _logger;
        //constructor
        public ImageService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<ImageService>();

        }

        //This collection can be used when loading many images for a search query, optimally we'd have some kind of thumbnail here as well.
        public ICollection<ImageSummaryViewModel> GetImages()
        {
            List<string> placeholderKeywords = new List<string>()
                    {
                        "Kungen",
                        "Stockholm",
                        "Skandal",
                        "Ferrari",
                        "Fortkörning"
                    };
            //this is translated into a database SELECT query
            return _context.Images
                //This Where-method implements a "soft delete" which hides the data from the application, but does not actually delete it from the Database
                .Where(image => !image.IsDeleted)                
                .Select(image => new ImageSummaryViewModel
                {
                    Name = image.ImageTitle,
                    Section = image.Section.ToString(),
                    //statiska nyckelord för test, ändra när keywords är implementerat 
                    Keywords = placeholderKeywords,
                    Date = image.Issue
                })
                .ToList();
        }

        //Need more logic here to actually display the image, but not sure how to do that yet. Will need to fetch the data from the db and 
        //convert it back into an image, as well as grabbing the data from the License and the MetaData.
        public ImageDetailViewModel GetImageDetail(int id)
        {
            return _context.Images
                .Where(x => x.ImageId == id)        //This generates a SELECT clause by id, so will find only one result
                .Where(x => !x.IsDeleted)           //Check for soft delete
                .Select(x => new ImageDetailViewModel
                {
                    Id = x.ImageId,
                    Name = x.ImageTitle,
                    Section = x.Section.ToString()
                })
                .SingleOrDefault();

        }

        //Add UPDATE here


        //Create a new recipe
        public int CreateImage(CreateImageCommand cmd)
        {
            Image image = cmd.ToImage();
            image.CreatedAt = DateTime.Now;
            _context.Add(image);
            _context.SaveChanges();
            return image.ImageId;
        }
    }
}
