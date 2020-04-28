
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public enum NewsSection
    {
        CULTURE,
        NEWS,
        INTERNATIONAL,
        ECONOMY,
        SPORTS
    }

    public class SectionEnum
    {
        public int SectionEnumId { get; set; }
        public NewsSection Name { get; set; }
    }
}
