using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Context;
using MyNotes.Services;
using MyNotes.Services.ChangePasswordServices;
namespace MyNotes.Controllers;

public class ChangeController : Controller
{
    private ApplicationContext db;

    private IChangePasswordService changePasswordService;

    private IChangeLogin changeLogin;
    
    public ChangeController(ApplicationContext _db,IChangePasswordService _changePasswordService,IChangeLogin _changeLogin)
    {
        db = _db;
        changePasswordService = _changePasswordService;
        changeLogin = _changeLogin;
    }

    //check
    [HttpPost]
    public IActionResult ChangePassword(string newPassword,string oldPassword)
    {
        string? nameBuff = this.User.Claims.First(p => p.Type == ClaimTypes.Name ).Value;
        return Json(changePasswordService.ChangePassword(db, newPassword, oldPassword,nameBuff).Result);
    }
    
    [HttpPost]
    public IActionResult ChangeLogin(string newLogin,string password)
    {
        string? nameBuff = this.User.Claims.First(p => p.Type == ClaimTypes.Name ).Value;
        return  Json(changeLogin.ChangeLogin(db, newLogin, nameBuff,password).Result);
    }
}