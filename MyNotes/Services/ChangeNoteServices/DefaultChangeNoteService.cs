using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.ChangeNoteServices;

public class DefaultChangeNoteService :  IChangeNoteService
{
    public async Task<bool> ChangeNote(ApplicationContext db, DtoNote updateNote)
    {
        try
        {
            Note? note = await db.Notes.FirstOrDefaultAsync(n=> n.Id == updateNote.Id);
            if (note is null)
            {
                return false;
            }
            note.Head = updateNote.Head;
            note.Body = updateNote.Body;
            db.Notes.Update(note);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}