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

    [StringLength(420, ErrorMessage ="Lösenordet måste vara minst 6 tecken",MinimumLength = 6)]
    [Required(ErrorMessage = "Ange ett lösenord")]
    [Display(Name = "Lösenord")]
    [DataType(DataType.Password)]
    public String Password { get; set; }
    [Required(ErrorMessage = "Upprepa lösenord")]
    [DataType(DataType.Password)]
    [Display(Name = "Upprepa lösenord")]
    [Compare("Password", ErrorMessage = "Lösen orden matchar inte")]
    public String ConfirmPassword { get; set; }

  }
}
