using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bothniabladet.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bothniabladet.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
    {
      userManager = userMgr;
      signInManager = signInMgr;

    }

    [AllowAnonymous]
    public ViewResult Login(string returnUrl)
    {
      return View(new LoginModel
      {
        ReturnUrl = returnUrl
      });
    }

  }
}
