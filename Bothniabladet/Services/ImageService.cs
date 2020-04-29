using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using Bothniabladet.Models.ImageModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //Placeholder, not storing keywords yet.
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
                    Id = image.ImageId,
                    Name = image.ImageTitle,
                    ThumbnailDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.Thumbnail)),
                    Section = image.Section.ToString(),
                    //statiska nyckelord för test, ändra när keywords är implementerat 
                    Keywords = placeholderKeywords,
                    Date = image.Issue
                })
                .ToList();
        }

        public ImageDetailViewModel GetImageDetail(int? id)
        {
            //Placeholder, not storing keywords yet.
            List<string> placeholderKeywords = new List<string>()
                    {
                        "Kungen",
                        "Stockholm",
                        "Skandal",
                        "Ferrari",
                        "Fortkörning"
                    };

            ImageDetailViewModel imageViewModel = _context.Images
                .Where(image => image.ImageId == id)        //This generates a SELECT clause by id, so will find only one result
                .Where(image => !image.IsDeleted)           //Check for soft delete
                .Select(image => new ImageDetailViewModel
                {
                    Id = image.ImageId,
                    Name = image.ImageTitle,
                    ImageDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                    Section = image.Section.ToString(),
                    //statiska nyckelord för test, ändra när keywords är implementerat 
                    Keywords = placeholderKeywords,
                    Date = image.Issue,
                    Height = image.ImageMetaData.Height,
                    Width = image.ImageMetaData.Width,
                    FileFormat = image.ImageMetaData.FileFormat,
                    GPS = image.ImageMetaData.Location.ToString(),
                    EditedImages = image.EditedImages   //would prefer not to query for the whole image, only the thumbnail.
                })
                .SingleOrDefault();
            if (imageViewModel.EditedImages == null) { imageViewModel.EditedImages = new List<EditedImage>(); }

            return imageViewModel;

        }

        public ICollection<SelectListItem> GetSectionChoices()
        {
            //returns the enums as SelectListItems with the enum name as Text and Value
            return _context.Enums.Select(item => new SelectListItem
            {
                Text = item.Name.ToString(),
                Value = item.Name.ToString()
            }
            ).ToList();

        }

        //Add UPDATE here


        //Create a new Image
        public int CreateImage(CreateImageCommand cmd)
        {
            Image image = cmd.ToImage();
            image.CreatedAt = DateTime.Now;
            _context.Add(image);
            _context.SaveChanges();
            return image.ImageId;
        }

        //Create a new EditedImage
        public int CreateEditedImage(AddEditedCommand cmd)
        {
            Image originalImage = _context.Images.Find(cmd.OriginalId);
            if (originalImage == null) { throw new Exception("Unable to find the image"); }
            if (originalImage.IsDeleted) { throw new Exception("Unable to add an edited image to a deleted image"); }
            EditedImage editedImage = cmd.ToEditedImage();
            if (originalImage.EditedImages == null)
            {
                originalImage.EditedImages = new List<EditedImage>();
            }
            originalImage.EditedImages.Add(editedImage);
            _context.SaveChanges();
            return originalImage.ImageId;
        }
    }
}
