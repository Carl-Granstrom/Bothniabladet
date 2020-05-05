using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bothniabladet.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Vänligen fyll i din Email")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vänligen fyll i lösenord")]
    [Display(Name = "Lösenord")]
    [UIHint("password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
  }
}
