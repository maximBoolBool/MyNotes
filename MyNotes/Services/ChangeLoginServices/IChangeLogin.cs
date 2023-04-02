using MyNotes.Context;

namespace MyNotes.Services;

public interface IChangeLogin
{
    public Task<bool> ChangeLogin(ApplicationContext db, string newLogin,string login,string password);
}