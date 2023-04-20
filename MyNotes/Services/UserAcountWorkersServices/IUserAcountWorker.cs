using System.Security.Claims;
using MyNotes.Models;

namespace MyNotes.Services.UserAcountWorkersServices;

public interface IUserAcountWorker
{
    public Task<bool> Registrate(DtoUser dtoUser);
    public Task<List<DtoNote>> Authenticate(DtoUser dtoUser);
    public  Task<string> Authorize(DtoUser dtoUser);
    public Task<bool> ChangePassword(string oldPassword,string newPassword,string login);
    public Task<bool> ChangeLogin(string newLogin,string oldLogin, string Password);
}