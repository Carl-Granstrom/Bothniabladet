using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bothniabladet.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Vänligen fyll i användarnamn")]
    [Display(Name = "Användarnamn")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Lösenord")]
    [Display(Name = "Lösenord")]
    [UIHint("password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
  }
}
