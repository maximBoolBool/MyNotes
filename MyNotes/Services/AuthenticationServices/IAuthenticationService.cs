using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.AuthenticationServices;

public interface IAuthenticationService
{
    public Task<bool> Authenticate(Man man, ApplicationContext db);
}