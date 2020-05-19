using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class DocumentOfSalesIndex
    {
        //Links
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int SalesId { get; set; }
        public DocumentOfSales SalesDocument { get; set; }
    }
}
