using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.IAddNoteServices;

public interface IAddNoteService
{
    public Task<bool> AddNote(ApplicationContext db,Note note,string login);
}