using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class Image
    {
        /* ENUMS */
        public enum NewsSection
        {
            CULTURE,
            NEWS,
            ECONOMY,
            SPORTS
        }

        /*VARIABLES*/
        public int ImageId { get; set; }
        public int ImageLicenseId { get; set; }     //1-1 unidirectional(?) to the image license object
        public int ImageMetaDataId { get; set; }    //1-1 unidirectional(?) to the metadata object
        public int basePrice { get; set; }
        public DateTime issue { get; set; }
        public NewsSection sectionPublished { get; set; }
        //the below line has been edited out to make the db migration work while testing, needs an advanced solution.
        //public HashSet<string> keywords { get; set; }      //Using a Hashset

        /*LINKS*/
        public ICollection<EditedImage> EditedImages { get; set; }

        /*METHODS*/

        /*Convenience variables and methods*/
        //ADD THE METHOD IN THE CONFIG
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }

    }
}
