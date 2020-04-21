using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bothniabladet.Data
{
    public class Invoice
    {

        /*VARIABLES*/
        public int InvoiceId { get; set; }
        public float DollarAmount { get; set; }
        public bool Paid { get; set; } //här kan man ju utöka med delbetald summa osv men känns overkill för uppgiften.
        public DateTime DueAt { get; set; } //add logic to set due date one month after creation date or similar business logic

        /*LINKS*/
        public Client Client { get; set; }

        /*METHODS*/

        /*Convenience variables and methods*/
        //ADD THE METHOD IN THE CONFIG
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}
