using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.GetNoteServices;

public class GetNoteService  : IGetNotesService
{
    public async Task<List<DtoNote>> GetNotes(ApplicationContext db,string login)
    {
        var user = db.Users.First(p=>p.Login == login);
        List<Note> notesBuff =  db.Notes.Where(p=> p.UserId == user.Id).ToList();
        List<DtoNote> notes = new List<DtoNote>();
        foreach (var note in notesBuff)
        {
            notes.Add(new DtoNote(){Id = note.Id, Head = note.Head, Body = note.Body});
        }
        
        return notes;
    }
}