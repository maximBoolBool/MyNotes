using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.GetNoteServices;

public interface IGetNotesService
{
    public Task<List<DtoNote>> GetNotes(ApplicationContext db, string login);
  
}