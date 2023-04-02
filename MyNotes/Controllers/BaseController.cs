﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Context;
using MyNotes.Models;
using MyNotes.Services.GetNoteServices;
using MyNotes.Services.IAddNoteServices;
namespace MyNotes.Controllers;
public class BaseController : Controller
{
    private ApplicationContext db;

    private IAddNoteService noteService;

    private IGetNotesService getNotesService;
    
    public BaseController(ApplicationContext _db,IAddNoteService _noteService,IGetNotesService _getNotesService)
    {
        db = _db;
        noteService = _noteService;
        getNotesService = _getNotesService;
    }

    //check
    public IActionResult isUserAuthenticated()
    {
        return Json(this.HttpContext.User.Identity.IsAuthenticated);
    }
    
    //check
    [HttpGet]
    public void SignOut()
    {
        this.HttpContext.SignOutAsync();
    }
    
    //check
    [HttpPost]
    public IActionResult AddNewNote(string header,string body)
    {
        try
        {
            string? loginbuff = User.Claims.First(p => p.Type == ClaimTypes.Name ).Value;
            var flag = noteService.AddNote(db, new Note()
            {
                Head = header,
                Body = body,
            }, loginbuff).Result;
            return Json(flag);
        }
        catch (Exception e)
        {
            return Json(false);
        }
    }
        
    [HttpGet]
    public IActionResult GetNotes()
    {
        try
        {
            string login = User.Claims.First(p => p.Type == ClaimTypes.Name).Value;
            List<FrontNote> notes = getNotesService.GetNotes(db,login).Result;
            return Json(notes);
        }
        catch (Exception e)
        {
            return Json(null);
        }
    }
}