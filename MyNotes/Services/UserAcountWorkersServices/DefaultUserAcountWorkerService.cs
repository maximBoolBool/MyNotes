using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.UserAcountWorkersServices;

public class DefaultUserAcountWorkerService : IUserAcountWorker
{
    private ApplicationContext db;
    
    public DefaultUserAcountWorkerService(ApplicationContext _db)
    {
        db = _db;
    }

    public async Task<bool> Registrate(DtoUser dtoUser)
    {

        User? userBuff = await db.Users.FirstOrDefaultAsync(p=> p.Login.Equals(dtoUser.Login) && p.Password.Equals(dtoUser.Password));

        if (userBuff is not null)
            return false;
        

        User newUser = new User()
        {
            Login = dtoUser.Login,
            Password = dtoUser.Password
        };

        await db.Users.AddAsync(newUser);
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> Authenticate(DtoUser dtoUser)
    {
        User buff = await db.Users.FirstAsync(p=> p.Login.Equals(dtoUser.Login) && p.Password.Equals(dtoUser.Password));

        return (buff is null) ? false : true;
    }

    public async Task<ClaimsPrincipal> Authorize(DtoUser dtoUser)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, dtoUser.Login),
            new Claim("Password", dtoUser.Password),
        };

        ClaimsIdentity idClaim = new ClaimsIdentity(claims,"Cookies");

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(idClaim);

        return claimsPrincipal;
    }

    public async Task<bool> ChangePassword(string newPassword,string oldPassword,string login)
    {
        try
        {
            var userbuf = await db.Users
                .Where(p => p.Login.Equals(login) && p.Password.Equals(oldPassword))
                .FirstOrDefaultAsync();

            if (userbuf is null)
                return false;

            userbuf.Password = newPassword;

            db.Update(userbuf);
            await db.SaveChangesAsync();

            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ChangeLogin(string newLogin,string oldLogin,string password)
    {
        User? buff = await db.Users.FirstOrDefaultAsync(p=> p.Login.Equals(oldLogin) && p.Password.Equals(password));

        if (buff is null)
            return false;

        buff.Login = newLogin;
        
        db.Users.Update(buff);
        await  db.SaveChangesAsync();
        return true;
    }
    
}