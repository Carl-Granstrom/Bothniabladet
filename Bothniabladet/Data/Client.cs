using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Bothniabladet.Data
{//sdsddafs
  public class Client
    {

        /*VARIABLES*/
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public float PriceReduction { get; set;}

        /*LINKS*/
        public ICollection<Invoice> Invoices { get; set; }

        /*METHODS*/

        /*Convenience variables and methods*/
        //ADD THE METHOD IN THE CONFIG
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}