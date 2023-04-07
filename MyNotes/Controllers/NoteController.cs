using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using MyNotes.Models;
using MyNotes.Models.ResponseClasses;
namespace MyNotes.Controllers;

public class NoteController : Controller
{
    private INotesWorkerServices noteService;

    public NoteController(INotesWorkerServices _noteService)
    {
        noteService = _noteService;
    }

    [HttpGet]
    [Route("Note/GetNotes")]
    public async Task<IActionResult> GetNotes()
    {
        string? name =  User.Claims.FirstOrDefault(p=>p.Type.Equals(ClaimTypes.Name) ).Value;
        
        List<DtoNote>? notes = await noteService.GetNotes(name);

        return Json(new ResponseClass<List<DtoNote>>((notes is null)? new List<DtoNote>() : notes));
    }

    [HttpPost]
    [Route("Note/UpdateNote")]
    public async Task<IActionResult> UpdateNote(DtoNote updateNote)
    {
        bool flag = await noteService.UpdateNote(updateNote);

        return Json(new ResponseClass<bool>(flag));

    }

    [HttpPost]
    [Route("Note/DeleteNote")]
    public async Task<IActionResult> DeleteNote(DtoNote deleteNote)
    {
        bool flag = await noteService.DeleteNote(deleteNote);

        return Json(new ResponseClass<bool>(flag));
    }


    [HttpPost]
    [Route("Note/AddNewNote")]
    public async Task<IActionResult> AddNewNote(DtoNote newNote)
    {
        string? name =  User.Claims.FirstOrDefault(p=>p.Type.Equals(ClaimTypes.Name) ).Value;

        bool flag = await noteService.AddNewNote(newNote,name);

        return Json(new ResponseClass<bool>(flag));
    }
}