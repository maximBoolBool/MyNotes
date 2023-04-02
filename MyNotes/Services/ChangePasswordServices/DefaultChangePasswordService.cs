using Microsoft.EntityFrameworkCore;
using MyNotes.Context;

namespace MyNotes.Services.ChangePasswordServices;

public class DefaultChangePasswordService : IChangePasswordService
{
    public async Task<bool> ChangePassword(ApplicationContext db,string newPassword,string oldPassword,string login)
    {
        try
        {
            var userbuff = await db.Users.Where(p => p.Login == login && p.Password == oldPassword).FirstOrDefaultAsync();
            if (userbuff is not null)
            {
                db.Users.Where(p => p.Login == login).FirstOrDefault().Password = newPassword;
                await db.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}