using System.IdentityModel.Tokens.Jwt;

namespace MyNotes.Services.IdentytiUserServices;

public class DefaultIdentityService : IIdentityUserService
{

    public async Task<string> GetPassword(string token)
    {
        JwtSecurityTokenHandler handler = new();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        string password = jwt.Claims.FirstOrDefault(p => p.Type.Equals("Password")).Value;

        return password;
    }


    public async Task<string> GetLogin(string token)
    {
        JwtSecurityTokenHandler handler = new();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        string name = jwt.Claims.FirstOrDefault(p => p.Type.Equals("Login")).Value;

        return name;
    }
}