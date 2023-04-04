using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.RegistrationService;

public class DefaultRegistrationService : IRegistrationService
{
    public async Task<Boolean> Registrate(DtoUser dtoUser,ApplicationContext db)
    {
        User? buff = await db.Users.FirstOrDefaultAsync(p=> p.Login == dtoUser.Login);
        
        if (buff is not null)
            return false;
       
        User newUser = new User()
        {
            Login = dtoUser.Login,
            Password = dtoUser.Password
        };
        await db.Users.AddAsync(newUser);
        db.SaveChanges();
        return true;
    }
}