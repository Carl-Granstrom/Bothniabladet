using Bothniabladet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//This is the object for calling edited images inside a image (Which is very costly)
namespace Bothniabladet.Models.ImageModels
{
    public class GetEditedImageModel
    {
        public int EditedImageId { get; set; }
        public string ImageTitle { get; set; }
        public String Thumbnail { get; set; }
        public String ImageData { get; set; }
        public int OriginalId { get; set; }

        public static GetEditedImageModel getEditedImage(EditedImage editedImage)
        {
            GetEditedImageModel getEditImageModel = new GetEditedImageModel
            {
                EditedImageId = editedImage.EditedImageId,
                ImageTitle = editedImage.ImageTitle,
                Thumbnail = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.Thumbnail)),
                ImageData = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.ImageData))
            };
            return getEditImageModel;
        }
    }
}

