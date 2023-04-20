namespace MyNotes.Services.IdentytiUserServices;

public interface IIdentityUserService
{
    public Task<string> GetPassword(string token);

    public Task<string> GetLogin(string token);
}