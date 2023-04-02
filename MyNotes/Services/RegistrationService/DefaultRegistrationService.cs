using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.RegistrationService;

public class DefaultRegistrationService : IRegistrationService
{
    public async Task<Boolean> Registrate(Man man,ApplicationContext db)
    {
        User? buff = await db.Users.FirstOrDefaultAsync(p=> p.Login == man.Login);
        if (buff is null)
        {
            User newUser = new User()
            {
                Login = man.Login,
                Password = man.Password
            };
            await db.Users.AddAsync(newUser);
            db.SaveChanges();
            return true;
        }
        return false;
    }
}