using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Context;
using MyNotes.Models;
using MyNotes.Services.AuthorizationServices;
using MyNotes.Services.RegistrationService;
using IAuthenticationService = MyNotes.Services.AuthenticationServices.IAuthenticationService;

namespace MyNotes.Controllers;

public class MainController : Controller
{
    private ApplicationContext db;

    private IRegistrationService registrationService;

    private IAuthenticationService authenticationService;

    private IAuthorizationService authorizationService;
    
    public MainController(ApplicationContext _db,
        IRegistrationService _registrationService,
        IAuthenticationService _authenticationService,
        IAuthorizationService _authorizationService
        )
    {
        db = _db;
        registrationService = _registrationService;
        authenticationService = _authenticationService;
        authorizationService = _authorizationService;
    }

    //check
    [HttpPost]
    public IActionResult Registrate(Man man)
    {
        bool flag = registrationService.Registrate(man, db).Result;

        if (flag)
        {
            var claims = authorizationService.Authorize(man).Result;
            HttpContext.SignInAsync(claims);
        }
        return Json(new List<bool>()
        {
            flag,
            this.HttpContext.User.Identity.IsAuthenticated
        });
    }

    //check
    [HttpPost]
    public IActionResult Authenticate(Man man)
    {
        bool flag = authenticationService.Authenticate(man, db).Result;

        if (flag)
        {
            var claims = authorizationService.Authorize(man).Result;
            HttpContext.SignInAsync(claims);
        }

        return Json(this.User.Claims);
    }
}