using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.ChangeLoginServices;

public class DefaultChangeLoginService : IChangeLogin
{
    public async Task<bool> ChangeLogin(ApplicationContext db,string newLogin,string login, string password)
    {
        try
        {
            User? buff = await db.Users.Where(p => p.Login == newLogin && p.Password==password).FirstOrDefaultAsync();
            if (buff is not null)
                return false;
            db.Users.Where(p => p.Login == login).FirstOrDefault().Login = newLogin;
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}