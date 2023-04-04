using System.Security.Claims;
using MyNotes.Models;

namespace MyNotes.Services.AuthorizationServices;

public interface IAuthorizationService
{
    public  Task<ClaimsPrincipal> Authorize(DtoUser dtoUser);
}