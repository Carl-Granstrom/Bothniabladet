using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bothniabladet.Data
{
    public class GPSCoordinate
    {
        /*VARIABLES*/
        public int GPSCoordinateId { get; set; }


        //Convenience fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}
