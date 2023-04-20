using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    
    public async Task<List<DtoNote>?> Authenticate(DtoUser dtoUser)
    {
        User buff = await db.Users.FirstAsync(p=> p.Login.Equals(dtoUser.Login) && p.Password.Equals(dtoUser.Password));

        if (buff is null)
            return null;
        
        List<Note> notesBuff = await db.Notes.Where(p => p.UserId.Equals(buff.Id)).ToListAsync();

        List<DtoNote> notes = new List<DtoNote>();

        foreach (var note in notesBuff)
        {
            notes.Add(new ()
            {
                Id = note.Id,
                Head = note.Head,
                Body = note.Body
            });
        }
        return notes;

    }

    public async Task<string> Authorize(DtoUser dtoUser)
    {
        List<Claim> claims = new List<Claim>()
        {
            new ("Login", dtoUser.Login),
            new ("Password", dtoUser.Password),
        };

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDINCE,
            claims : claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(30)),
            signingCredentials:new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(jwt);
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