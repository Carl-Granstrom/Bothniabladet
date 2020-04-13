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

        //Convenience variables
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }
    }
}
