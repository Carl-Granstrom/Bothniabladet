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
        public int ImageID { get; set; }
        public int ImageLicenseId { get; set; }
        public int ImageMetaDataId { get; set; }
        public int basePrice { get; set; }
        public DateTime issue { get; set; }
        public NewsSection sectionPublished { get; set; }
        public HashSet<string> keywords { get; set; }      //Using a Hashset

        /*METHODS*/

        /*Convenience variables and methods*/
        //ADD THE METHOD IN THE CONFIG
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }

    }
}
