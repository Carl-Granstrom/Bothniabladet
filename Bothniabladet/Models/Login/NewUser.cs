using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bothniabladet.Models
{
  public class NewUser
  {
    [Required(ErrorMessage ="Ange ett användarnamn")]
    [Display(Name = "Användarnamn")]
    public String UserName { get; set; }

    [Required(ErrorMessage ="Ange e-mail")]
    [Display(Name = "Email")]
    [EmailAddress]
    public String Email { get; set; }

    [Required]
    [Display(Name = "Lösenord")]
    [DataType(DataType.Password)]
    public String Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Upprepa lösenord")]
    [Compare("Password", ErrorMessage = "Lösen orden matchar inte")]
    public String ConfirmPassword { get; set; }

  }
}
