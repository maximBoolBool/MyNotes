using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using MyNotes.Models.ResponseClasses;
using MyNotes.Services.UserAcountWorkersServices;

namespace MyNotes.Controllers;

public class AccountController : Controller
{
    private IUserAcountWorker acountService;

    public AccountController(IUserAcountWorker _acountService)
    {
        acountService = _acountService;
    }

    [HttpPost]
    [Route("Account/Registrate")]
    public async Task<IActionResult> Registrate(DtoUser newUser)
    {

        bool flag = await acountService.Registrate(newUser);

        if (!flag)
            return Json(new ResponseClass<bool>(flag));

        ClaimsPrincipal cp = acountService.Authorize(newUser).Result;

        await HttpContext.SignInAsync(cp);

        return Json(new ResponseClass<bool>(flag));
    }

    [HttpPost]
    [Route("Account/Authenticate")]
    public async Task<IActionResult> Authenticate(DtoUser oldUser)
    {
        bool flag = await acountService.Authenticate(oldUser);

        if (!flag)
            return Json(new ResponseClass<bool>(false));

        ClaimsPrincipal cp = acountService.Authorize(oldUser).Result;

        await HttpContext.SignInAsync(cp);

        return Json(new ResponseClass<bool>(flag));
    }

    [HttpGet]
    [Route("Account/IsUserAutheticate")]
    public async Task<IActionResult> IsUserAuthenticate()
    {
        bool flag = User.Identity.IsAuthenticated;

        return Json(new ResponseClass<bool>(flag));
    }


    [HttpGet]
    [Route("Account/SignOut")]
    public async Task SignOut()
    {
        await HttpContext.SignOutAsync();
    }

    [HttpPost]
    [Route("Account/ChangePassword")]
    public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
    {
        string login = User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Name)).Value;

        bool flag = await acountService.ChangePassword(newPassword,oldPassword,login);

        if (!flag)
            return Json(new ResponseClass<bool>(flag));

        ClaimsPrincipal cp =  await acountService.Authorize(new DtoUser(){Login = login,Password = newPassword});

        await HttpContext.SignInAsync(cp);
        
        return Json(new ResponseClass<bool>(flag));
    }

    [HttpPost]
    [Route("Account/ChangeLogin")]
    public async Task<IActionResult> ChangeLogin(string newLogin, string password)
    {
        string? oldLogin = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;

        bool flag = await acountService.ChangeLogin(newLogin, oldLogin, password);

        if (!flag)
            return Json(new ResponseClass<bool>(flag));
        
        ClaimsPrincipal cp =  await acountService.Authorize(new DtoUser(){Login = newLogin,Password = password});

        await HttpContext.SignInAsync(cp);
        
        return Json(new ResponseClass<bool>(flag));
        
    }


    [HttpPost]
    [Route("GetClaims")]
    public IActionResult GetClaims()
    {

        StringBuilder k = new StringBuilder();

        foreach (var claim in User.Identities.FirstOrDefault().Claims )
        {
            k.Append(claim.Value+"\n");
        }


        return Json(new ResponseClass<string>(k.ToString()));
    }
    
}