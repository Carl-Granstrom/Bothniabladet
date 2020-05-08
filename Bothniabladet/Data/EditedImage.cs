using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bothniabladet.Data
{
    public class EditedImage
    {
        /*VARIABLES*/
        public int EditedImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool IsDeleted { get; set; }

        /*LINKS*/
        public Image Image { get; set; }

        //Convenience variables
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //not working, doing it manually atm
        public DateTime CreatedAt { get; set; }
    }
}
