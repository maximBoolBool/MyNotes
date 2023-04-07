using Microsoft.EntityFrameworkCore;
using MyNotes.Context;
using MyNotes.Models;

namespace MyNotes.Services.NotesWorkerServices;

public class DefaultNotesWorkerServices : INotesWorkerServices
{
    private ApplicationContext db;

    public DefaultNotesWorkerServices(ApplicationContext _db)
    {
        db = _db;
    }
    
    public async Task<bool> AddNewNote(DtoNote newNote,string userLogin)
    {

        User? userBuff = await db.Users.FirstOrDefaultAsync(p=> p.Login.Equals(userLogin));

        if (userBuff is null)
            return false;
        
        userBuff.Notes.Add(new Note()
        {
            Owner = userBuff,
            Head = newNote.Head,
            Body = newNote.Body
        });
        
        db.Users.Update(userBuff);
        await db.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> UpdateNote(DtoNote updateNote)
    {
        Note? buff = await db.Notes.FirstOrDefaultAsync(p=> p.Id.Equals(updateNote.Id));

        if (buff is null)
            return false;


        buff.Head = updateNote.Head;
        buff.Body = updateNote.Body;

        db.Notes.Update(buff);

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNote(DtoNote deleteNode)
    {
        try
        {

            Note? buff = await db.Notes.FirstOrDefaultAsync(p=> p.Id.Equals(deleteNode.Id));
            
            db.Notes.Remove(buff);
            
            await db.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<DtoNote>> GetNotes(string userLogin)
    {
        User? user = await db.Users.FirstOrDefaultAsync(p=> p.Login.Equals(userLogin));

        var notes = await db.Notes.Where(p => p.UserId.Equals(user.Id)).ToListAsync();

        var response = new List<DtoNote>();
        
        foreach (var note in  notes)
        {
            response.Add(new DtoNote()
            {
                Id = note.Id,
                Head = note.Head,
                Body = note.Body
            });
        }

        return response;

    }

}