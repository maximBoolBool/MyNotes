using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyNotes.Context;
using MyNotes.Services;
using MyNotes.Services.AuthenticationServices;
using MyNotes.Services.AuthorizationServices;
using MyNotes.Services.ChangeLoginServices;
using MyNotes.Services.ChangeNoteServices;
using MyNotes.Services.ChangePasswordServices;
using MyNotes.Services.GetNoteServices;
using MyNotes.Services.IAddNoteServices;
using MyNotes.Services.RegistrationService;

var builder = WebApplication.CreateBuilder();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseNpgsql(connection));
builder.Services.AddTransient<IAuthenticationService,DefaultAuthenticationService>();
builder.Services.AddTransient<IAuthorizationService,AuthorizationService>();
builder.Services.AddTransient<IRegistrationService,DefaultRegistrationService>();
builder.Services.AddTransient<IAddNoteService, AddNoteService>();
builder.Services.AddTransient<IChangePasswordService, DefaultChangePasswordService>();
builder.Services.AddTransient<IChangeLogin, DefaultChangeLoginService>();
builder.Services.AddTransient<IGetNotesService, GetNoteService>();
builder.Services.AddTransient<IChangeNoteService, DefaultChangeNoteService>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Map("/", () => { });

app.MapControllerRoute(name:"default",
    pattern:"{controller=Main}/{action=Registrate}");
app.MapControllerRoute(name:"default",
    pattern:"{controller=Main}/{action=Authenticate}");
app.MapControllerRoute(name:"default",
    pattern:"{controller=Base}/{action=isUserAuthenticated}");
app.MapControllerRoute(name:"default",
    pattern:"{controller=Base}/{action=AddNewNote}");
app.MapControllerRoute(name: "default",
    pattern:"{controller=Change}/{action=ChangePassword}");
app.MapControllerRoute(name:"default",
    pattern:"{controller=Change}/{action=ChangeLogin}");
app.MapControllerRoute(name: "default",
    pattern: "{controller=Change}/{action=ChangePassword}");
app.MapControllerRoute(name:"default",
    pattern:"{controller=Base}/{action=UpdateNote}");


app.Run();