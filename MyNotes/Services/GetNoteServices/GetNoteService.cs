using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.GetNoteServices;

public class GetNoteService  : IGetNotesService
{
    public async Task<List<FrontNote>> GetNotes(ApplicationContext db,string login)
    {
        var user = db.Users.First(p=>p.Login == login);
        List<Note> notesBuff =  db.Notes.Where(p=> p.UserId == user.Id).ToList();
        List<FrontNote> notes = new List<FrontNote>();
        foreach (var note in notesBuff)
        {
            notes.Add(new FrontNote(){Id = note.Id, Head = note.Head, Body = note.Body});
        }
        
        return notes;
    }
}