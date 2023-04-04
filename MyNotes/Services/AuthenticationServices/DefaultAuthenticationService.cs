using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.AuthenticationServices;

public class DefaultAuthenticationService : IAuthenticationService
{
    public async Task<bool> Authenticate(DtoUser dtoUser,ApplicationContext db)
    {
        User? user = await db.Users.FirstOrDefaultAsync(p=> p.Login==dtoUser.Login);

        return user is not null && dtoUser.Password.Equals(user.Password);
    }
}