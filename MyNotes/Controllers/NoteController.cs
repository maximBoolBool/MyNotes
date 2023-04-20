using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using MyNotes.Models.ResponseClasses;
using MyNotes.Services.IdentytiUserServices;

namespace MyNotes.Controllers;

public class NoteController : Controller
{
    private INotesWorkerServices noteService;

    private IIdentityUserService identityUserService;
    public NoteController(INotesWorkerServices _noteService,IIdentityUserService _identityUserService)
    {
        noteService = _noteService;
        identityUserService = _identityUserService;
    }

    [HttpGet]
    [Route("Note/GetNotes")]
    public async Task<IActionResult> GetNotes()
    {
        string? name = User.Claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name)).Value;
        return Json((name is null)? "sdfsdf" : name );
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
        Console.WriteLine($"Noten\n{newNote.Head}\n{newNote.Body}");


        string token = HttpContext.Request.Headers["auth"];

        string name = await identityUserService.GetLogin(token);
        
        var flag = await noteService.AddNewNote(newNote,name);

        return Json(flag);
    }
}