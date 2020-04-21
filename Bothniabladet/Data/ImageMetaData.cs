using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using GeoAPI.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;

namespace Bothniabladet.Data
{
    [Owned]
    public class ImageMetaData
    {
        /*VARIABLES*/
        [Key]
        public int ImageMetaDataId { get; set; }
        private int Height { get; set;}     //height in pixels
        private int Width { get; set; }     //width in pixels
        private long FileSize { get; set; } //Placeholder, not sure how to handle this

        [Column(TypeName = "geometry")]
        public Point Location { get; set; }

        //Convenience fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

    }
}
