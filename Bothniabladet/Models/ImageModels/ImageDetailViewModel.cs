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
    public class ImageDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //used to display the image
        public string ImageDataString { get; set; }
        public string Section { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string FileFormat { get; set; }
        public string GPS { get; set; }
        public string noGPS = "No GPS coordinates for this image";      //TODO: Replace this with logic in the View instead
        //Thumbnail data'
        public AddEditedCommand CreateEditedImage { get; set; }     //will be populated from the view
        public ICollection<String> ThumbNailDataStrings { get; set; }
        public ICollection<EditedImage> EditedImages { get; set; }  //could remove this with better logic in the service/query
        public int LicenseUsesLeft { get; set; }


        public static ImageDetailViewModel FromImage(Image image)
        {
            //Create keyword list
            List<Keyword> Keywords = new List<Keyword>();
            //add null check if needed
            foreach (ImageKeyword imageKeyword in image.KeywordLink)
            {
                Keywords.Add(imageKeyword.Keyword);
            }

            ImageDetailViewModel viewModel = new ImageDetailViewModel
            {
                Id = image.ImageId,
                Name = image.ImageTitle,
                ImageDataString = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.ImageData)),
                Section = image.Section.ToString(),
                Date = image.Issue,
                Height = image.ImageMetaData.Height,
                Width = image.ImageMetaData.Width,
                FileFormat = image.ImageMetaData.FileFormat,
                GPS = image.ImageMetaData.Location.ToString()
            };
            //Create EditedImage list and convert the byte[] to List<string>
            if (image.EditedImages == null)
            {
                viewModel.EditedImages = new List<EditedImage>();
            }
            ICollection<String> editedImagesDataStrings = new List<String>();
            foreach (EditedImage editedImage in image.EditedImages)
            {
                editedImagesDataStrings.Add(String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.Thumbnail)));
            }
            viewModel.ThumbNailDataStrings = editedImagesDataStrings;

            return viewModel;
        }
    }
}
