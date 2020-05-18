using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    // Many to many relationship between Image and ShoppingCart
    public class ShoppingCartImage
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
