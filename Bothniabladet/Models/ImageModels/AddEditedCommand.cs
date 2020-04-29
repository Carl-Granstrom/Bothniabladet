using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;
using System.IO;
using System.Drawing;

namespace Bothniabladet.Models.ImageModels
{
    public class AddEditedCommand
    {
        /*VARIABLES*/
        [Required]
        [BindProperty]
        public ImageData ImageData { get; set; }
        public string ImageTitle { get; set; }
        public int OriginalId { get; set; }

        /*METHODS*/
        //this method handles the metadata extraction and the creation of a new image from the command object.
        public EditedImage ToEditedImage()
        {
            MemoryStream streamForThumbNail = new MemoryStream();
            ImageData.FormFile.CopyTo(streamForThumbNail);
            System.Drawing.Image tmpImg = System.Drawing.Image.FromStream(streamForThumbNail);
            System.Drawing.Image tmpThumb = tmpImg.GetThumbnailImage(50, 50, null, IntPtr.Zero);
            streamForThumbNail.Dispose();   //Dispose of stream
            ImageConverter _converter = new ImageConverter();
            MemoryStream streamForFullSize = new MemoryStream();
            ImageData.FormFile.CopyTo(streamForFullSize);
            EditedImage image = new EditedImage
            {
                ImageTitle = ImageTitle,
                Thumbnail = (byte[])_converter.ConvertTo(tmpThumb, typeof(byte[])),
                ImageData = streamForFullSize.ToArray(),
                CreatedAt = DateTime.Now
            };
            streamForFullSize.Dispose();    //Dispose of stream
            //This is all a bit convoluted, I'm sure it could be refactored to be a lot neater and clearer
            return image;
        }
    }
}
