using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.RegistrationService;

public interface IRegistrationService
{
    public Task<Boolean> Registrate(DtoUser dtoUser,ApplicationContext db);
}