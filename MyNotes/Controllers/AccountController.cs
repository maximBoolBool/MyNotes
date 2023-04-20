using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using MyNotes.Models.ResponseClasses;
using MyNotes.Services.IdentytiUserServices;
using MyNotes.Services.UserAcountWorkersServices;

namespace MyNotes.Controllers;

public class AccountController : Controller
{
    private IUserAcountWorker acountService;

    private IIdentityUserService identityUserService;

    public AccountController(IUserAcountWorker _acountService,IIdentityUserService _identityUserService)
    {
        acountService = _acountService;
        identityUserService = _identityUserService;
    }

    [HttpPost]
    [Route("Account/Registrate")]
    public async Task<IActionResult> Registrate(DtoUser newUser)
    {

        bool flag = await acountService.Registrate(newUser);

        if (!flag)
            return Json(new ResponseClass<bool>(flag));
        
        
        string jwtString = acountService.Authorize(newUser).Result;

        return Json(new ResponseClass<string>(jwtString));
    }

    [HttpPost]
    [Route("Account/Authenticate")]
    public async Task<IActionResult> Authenticate(DtoUser oldUser)
    {
        Console.WriteLine($"DtoUser-{oldUser.Login}-{oldUser.Password}");
        
        List<DtoNote> flag = await acountService.Authenticate(oldUser);

        if (flag is null)
            return Json(new ResponseClass<bool>(false));

        string jwtString = acountService.Authorize(oldUser).Result;

        return Json(new ResponseClass<List<DtoNote>>(flag)
        {
            token = jwtString,
        });
    }
}