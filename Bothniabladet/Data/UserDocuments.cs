using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class UserDocuments
    {
        public bool Closed { get; set; }

        public int SalesDocumentId { get; set; }
        public SalesDocument SalesDocument { get; set; }
        public String UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
