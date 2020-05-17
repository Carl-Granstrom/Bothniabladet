using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using Bothniabladet.Models.ImageModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bothniabladet.Services
{
    public class ImageService
    {
        AppDbContext _context;
        readonly ILogger _logger;
        //constructor
        public ImageService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<ImageService>();

        }

        //Loads all the images
        public ICollection<ImageSummaryViewModel> GetImages()
        {
            List<Image> images = _context.Images
              .Where(image => image.IsDeleted == false)
              .Include(image => image.KeywordLink)
              .ThenInclude(imageKeywords => imageKeywords.Keyword)
              .ToList();

            ICollection<ImageSummaryViewModel> imageSummaryViewModels = new List<ImageSummaryViewModel>();
            foreach (Image image in images)
            {
                List<string> keywordStrings = new List<string>();
                foreach (ImageKeyword imageKeyword in image.KeywordLink)
                {
                    keywordStrings.Add(imageKeyword.Keyword.Word);
                }
                imageSummaryViewModels.Add(new ImageSummaryViewModel
                {
                    Id = image.ImageId,
                    Name = image.ImageTitle,
                    ThumbnailDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.Thumbnail)),
                    ImageDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                    Section = image.Section.ToString(),
                    //statiska nyckelord för test, ändra när keywords är implementerat
                    Keywords = keywordStrings,
                    Date = image.Issue
                });
            }


            return imageSummaryViewModels;
        }

        //Loads all the images
        public ICollection<ImageSummaryViewModel> GetSearchedImages(string searchString)
        {
            //Search the titles
            List<Image> imagesByTitle = _context.Images
              .Where(image => (image.IsDeleted == false) && (image.ImageTitle.Contains(searchString)))
              .Include(image => image.KeywordLink)
              .ThenInclude(imageKeywords => imageKeywords.Keyword)
              .ToList();

            List<Keyword> foundKeywords = _context.Keywords
                .Where(keyword => keyword.Word.Contains(searchString))
                .Include(imgKey => imgKey.KeywordLink)
                .ThenInclude(image => image.Image)
                .ToList();

            List<Image> imagesByKeyword = new List<Image>();
            foreach (Keyword keyword in foundKeywords)
            {
                foreach (ImageKeyword imgKey in keyword.KeywordLink)
                {
                    imagesByKeyword.Add(imgKey.Image);
                }
            }

            //The unions prevent the same image from appearing twice.
            imagesByTitle = imagesByTitle.Union(imagesByKeyword).ToList();  //Create a Union(no duplicates) of the keyword and title search lists

            //Add search by section

            ICollection<ImageSummaryViewModel> imageSummaryViewModels = new List<ImageSummaryViewModel>();
            foreach (Image image in imagesByTitle)
            {
                List<string> keywordStrings = new List<string>();
                foreach (ImageKeyword imageKeyword in image.KeywordLink)
                {
                    keywordStrings.Add(imageKeyword.Keyword.Word);
                }
                imageSummaryViewModels.Add(new ImageSummaryViewModel
                {
                    Id = image.ImageId,
                    Name = image.ImageTitle,
                    ThumbnailDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.Thumbnail)),
                    Section = image.Section.ToString(),
                    Keywords = keywordStrings,
                    Date = image.Issue
                });
            }


            return imageSummaryViewModels;
        }

        public ImageDetailViewModel GetImageDetail(int? id)
        {
            ImageDetailViewModel imageViewModel = _context.Images
                .Where(image => image.ImageId == id)        //This generates a SELECT clause by id, so will find only one result
                .Where(image => !image.IsDeleted)           //Check for soft delete
                .Select(image => new ImageDetailViewModel
                {
                    Id = image.ImageId,
                    Name = image.ImageTitle,
                    ImageDataString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                    Section = image.Section.ToString(),
                    Date = image.Issue,
                    Height = image.ImageMetaData.Height,
                    Width = image.ImageMetaData.Width,
                    FileFormat = image.ImageMetaData.FileFormat,
                    GPS = image.ImageMetaData.Location.ToString(),
                    EditedImages = image.EditedImages,   //would prefer not to query for the whole image, only the thumbnail.
                    LicenseUsesLeft = image.ImageLicense.UsesLeft
                })
                .SingleOrDefault();
            if (imageViewModel.EditedImages == null) { imageViewModel.EditedImages = new List<EditedImage>(); }

            return imageViewModel;
        }


        public GetEditedImageModel GetImageModel(int? id, int? editId)
        {
            GetEditedImageModel imageViewModel = _context.EditedImages
                .Where(editedImage => editedImage.Image.ImageId == id)
                .Where(editedImage => editedImage.EditedImageId == editId)
                .Where(editedImage => !editedImage.IsDeleted)
                .Include(editedImage => editedImage.Image)
                .Select(editedImage => new GetEditedImageModel
                {
                    OriginalId = editedImage.Image.ImageId,
                    EditedImageId = editedImage.EditedImageId,
                    ImageTitle = editedImage.ImageTitle,
                    Thumbnail = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.Thumbnail)),
                    ImageData = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.ImageData))
                }).SingleOrDefault();
            return imageViewModel;
        }
        //Return a Bitmap of a single image by ID, this is used for file download
        public Data.Image GetImage(int? id)
        {
            Data.Image image = _context.Images.Find(id);
            return image;

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

        //Add UPDATE here, low prio

        //Soft Delete
        public void SoftDeleteImage(int id)
        {
            Image image = _context.Images.Find(id);
            //Here we could possibly throw an error if the image is already deleted.
            image.IsDeleted = true;
            _context.SaveChanges();
        }

        //Create a new Image
        public int CreateImage(CreateImageCommand cmd)
        {
            List<Keyword> oldKeywords = _context.Keywords
                .Include(keyword => keyword.KeywordLink)
                .ToList();
            if (cmd.Keywords.First() != null && cmd.Keywords.Count > 0)
            {
                cmd.Keywords = cmd.Keywords.First().Split(',');
            }
            Image image = cmd.ToImage();
            image.KeywordLink = new List<ImageKeyword>();           //the many-many link between image and keyword
            _context.Add(image);
            image.CreatedAt = DateTime.Now;
            _context.SaveChanges();

            ICollection<Keyword> tmpKeywords = new List<Keyword>(); //the new keywords that need to be added

            bool added = false;
            foreach (string keywordString in cmd.Keywords)
            {
                added = false;
                foreach (Keyword oldKeyword in oldKeywords)
                {
                    if (oldKeyword.Word == keywordString)
                    {
                        image.KeywordLink.Add(new ImageKeyword { Keyword = oldKeyword, KeywordId = oldKeyword.KeywordId, Image = image, ImageId = image.ImageId });
                        added = true;
                    }
                }
                if (!added)
                {
                    tmpKeywords.Add(new Keyword { Word = keywordString });
                }
            }
            //Add the many-many link image<-->keyword
            foreach (Keyword keyword in tmpKeywords)
            {
                image.KeywordLink.Add(new ImageKeyword
                {
                    Image = image,
                    Keyword = keyword
                });
            }
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

        //Use Licenced Image - This only updates the number of uses and throws an error if there are no uses left
        //Make sure to have the view lock the button and display a warning if there are no uses left
        //returns the number of Licensed uses left.
        public int UseLicense(int id)
        {
            Image image = _context.Images.Find(id);
            if (image.ImageLicense.LicenceType == ImageLicense.LicenseType.LICENSED)
            {
                if (image.ImageLicense.UsesLeft > 0)
                {
                    image.ImageLicense.UsesLeft--;
                }
                else
                {
                    throw new Exception("The Licence of this image permits no further use.");
                }
            }
            else
            {
                throw new Exception("This image is not licensed and therefore has unlimited uses");
            }

            _context.SaveChanges();
            return image.ImageLicense.UsesLeft;
        }
    }
}
