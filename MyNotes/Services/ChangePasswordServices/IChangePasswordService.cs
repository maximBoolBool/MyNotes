using MyNotes.Context;

namespace MyNotes.Services.ChangePasswordServices;

public interface IChangePasswordService
{
    public Task<bool> ChangePassword(ApplicationContext db,string newPassword,string oldPassword,string login);
}