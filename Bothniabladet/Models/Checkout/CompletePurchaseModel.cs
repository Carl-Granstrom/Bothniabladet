using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bothniabladet.Models.Checkout
{
    public class CompletePurchaseModel
    {
        [Required]
        [Display(Name = "Förnamn")]
        public string fName { get; set; }
        [Required]
        [Display(Name = "Efternamn")]
        public string lName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public String Address { get; set; }

        [Required]
        public String PaymentMethod { get; set; }
        [Required]
        public bool Private { get; set; } // If false the the person is doing this for a company
        
        [Display(Name = "XXX-XXX-XX")]
        public double Discount { get; set; }

        public Data.SalesDocument ToSalesDocument()
        {
            //ICollection<EditedImage> editedImages = new List<EditedImage>();
            Data.SalesDocument document = new Data.SalesDocument
            {
                fName = fName,
                lName = lName,
                Address = Address,
                PaymentMethod = PaymentMethod,
                Private = Private,
                Discount = Discount
            };

            return document;
        }
    }
}
