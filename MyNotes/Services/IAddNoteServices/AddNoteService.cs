using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;
namespace MyNotes.Services.IAddNoteServices;

public class AddNoteService : IAddNoteService
{
    public async  Task<bool> AddNote(ApplicationContext db,Note note,string login)
    {
        try
        {
            var user = db.Users.Where(p => p.Login == login).FirstOrDefault(p => p.Login == login);
            note.Owner = user;
            db.Notes.Add(note);
            db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}