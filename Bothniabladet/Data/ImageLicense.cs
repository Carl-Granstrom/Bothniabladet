using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bothniabladet.Data
{
    [Owned]
    public class ImageLicense
    {
        /* ENUMS */
        public enum LicenseType
        {
            OWNED,      //Our own photographers' images. Some logical overlap between OWNED and BOUGHT
            LICENSED,   //Licensed from another company
            BOUGHT      //Purchased from an individual
        }

        /*VARIABLES*/
        public LicenseType LicenceType { get; set; }
        public int UsesLeft { get; set; }  //this should probably not be get/set via these methods, but need to think about it. Also, use -1 as gatekeeper for OWNED and BOUGHT types and specify in constructor?

        /*METHODS*/
    }
}