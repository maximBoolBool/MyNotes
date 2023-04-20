using System.Security.Claims;
using MyNotes.Models;

public interface INotesWorkerServices
{

  
    public Task<DtoNote> AddNewNote(DtoNote newNote,string user);
    public Task<bool> UpdateNote(DtoNote updateNote);
    public Task<bool> DeleteNote(DtoNote deleteNote);
    public Task<List<DtoNote>> GetNotes(string userLogin);
}