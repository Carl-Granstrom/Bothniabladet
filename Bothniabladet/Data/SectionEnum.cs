
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public enum NewsSection
    {
        CULTURE,
        NEWS,
        ECONOMY,
        SPORTS
    }

    public class SectionEnum
    {
        public NewsSection Name { get; set; }
    }
}
