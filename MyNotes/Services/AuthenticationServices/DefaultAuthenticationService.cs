using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.AuthenticationServices;

public class DefaultAuthenticationService : IAuthenticationService
{
    public async Task<bool> Authenticate(Man man,ApplicationContext db)
    {
        User? user = await db.Users.FirstOrDefaultAsync(p=> p.Login==man.Login);
        {
            if (user is not null)
            {
                if (man.Password.Equals(user.Password))
                {
                    return true;
                }
            }
        }
        return false;
    }
}