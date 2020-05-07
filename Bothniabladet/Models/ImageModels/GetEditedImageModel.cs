using Bothniabladet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Models.ImageModels
{
    public class GetEditedImageModel
    {
        public int EditedImageId { get; set; }
        public string ImageTitle { get; set; }
        public String ImageData { get; set; }
        public String Thumbnail { get; set; }

        public static GetEditedImageModel getEditedImage(EditedImage editedImage)
        {
            GetEditedImageModel getEditImageModel = new GetEditedImageModel
            {
                EditedImageId = editedImage.EditedImageId,
                ImageTitle = editedImage.ImageTitle,
                ImageData = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.ImageData)),
                Thumbnail = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.Thumbnail))
            };
            return getEditImageModel;
        }
    }
}

