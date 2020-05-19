using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class DocumentOfSales
    {
        //Variables
        public int SalesId { get; set; }
        public String fName { get; set; }
        public String lName { get; set; }
        public String Address { get; set; }
        public String PaymentMethod { get; set; }
        public bool Private { get; set; } // If false the the person is doing this for a company
        public double Discount { get; set; }
        
        //Links
        public ICollection<DocumentOfSalesIndex> DocumentOfSalesIndex { get; set; }
        public ApplicationUser User { get; set; } // User is linked directly to this document
    }
}
