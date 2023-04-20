using System.Text;

namespace MyNotes;
using Microsoft.IdentityModel.Tokens;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDINCE = "MyAuthClient";
    public const string KEY = "mySuperProtectKey_!117";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
    
}