using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.ChangeNoteServices;

public interface IChangeNoteService
{
    public Task<bool> ChangeNote(ApplicationContext db, DtoNote newNote);
}