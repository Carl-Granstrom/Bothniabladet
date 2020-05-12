using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bothniabladet.Models;
using Bothniabladet.Data;


namespace Bothniabladet.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;
    private RoleManager<IdentityRole> roleManager;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr)
    {
      userManager = userMgr;
      signInManager = signInMgr;
      roleManager = roleMgr;
    }
    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }
    [AllowAnonymous]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel login)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await userManager.FindByEmailAsync(login.Email);
        if (user != null)
        {
          await signInManager.SignOutAsync();
          if ((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
          {
            return Redirect(login?.ReturnUrl ?? "/Images/Index");
          }
          else
          {
            ViewBag.Message = "Fel e-mail adress eller lösenord";
          }
        }
      }

      return View(login);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(NewUser newUser)
    {
      if (ModelState.IsValid)
      {

        var user = new IdentityUser
        {
          UserName = newUser.UserName,
          Email = newUser.Email
        };
        var insertrec = await userManager.CreateAsync(user, newUser.Password);
        var roleExist = await roleManager.RoleExistsAsync("Customer");
        if (!roleExist)
        {
          var role = new IdentityRole();
          role.Name = "Customer";
          await roleManager.CreateAsync(role);
        }
        await userManager.AddToRoleAsync(user, "Customer");
        if (insertrec.Succeeded)
        {
          ModelState.Clear();
          ViewBag.Message = "Användaren " + newUser.UserName + " är nu registrerad";
        }
        else
        {
          foreach (var error in insertrec.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
        }
      }
      return View();
    }
    public async Task<IActionResult> signOut(string returnurl = "/")
    {
      await signInManager.SignOutAsync();
      return Redirect(returnurl);
    }
  }
}
