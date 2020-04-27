using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using GeoAPI.Geometries;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;

namespace Bothniabladet.Data
{
    [Owned]
    public class ImageMetaData
    {
        /*VARIABLES*/
        public int Height { get; set;}     //height in pixels
        public int Width { get; set; }     //width in pixels
        public long FileSize { get; set; } //Placeholder, not sure how to handle this
        public DateTime DateTaken { get; set; }

        [Column(TypeName = "geometry")]
        public Point Location { get; set; }
    }
}
