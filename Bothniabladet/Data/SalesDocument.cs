using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class SalesDocument
    {
        public int SalesDocumentId { get; set; }
        public bool Closed { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public String Address { get; set; }
        public String PaymentMethod { get; set; }
        public bool Private { get; set; } // If false the the person is doing this for a company
        public double Discount { get; set; }

        //links
        public ICollection<UserDocuments> UserDocuments { get; set; }
    }
}
