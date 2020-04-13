using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using GeoAPI.Geometries;

namespace Bothniabladet.Data
{
    public class ImageMetaData
    {
        /*VARIABLES*/
        public int ImageMetaDataId { get; set; }
        private int height { get; set;}     //height in pixels
        private int width { get; set; }     //width in pixels
        private long fileSize { get; set; } //Placeholder, not sure how to handle this
                                            //Need to read more about NetTopologySuite and how to use it to store geographical data.
        [Column(TypeName = "geometry")]     //I'm not sure about this annotation-based way of doing things, or if it can all go in the config file.
        private IPoint Location { get; set; }

        //Convenience fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }

    }
}
