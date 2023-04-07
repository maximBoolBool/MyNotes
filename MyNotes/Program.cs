using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyNotes.Context;
using MyNotes.Services.NotesWorkerServices;
using MyNotes.Services.UserAcountWorkersServices;

var builder = WebApplication.CreateBuilder();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseNpgsql(connection));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddTransient<IUserAcountWorker, DefaultUserAcountWorkerService>();
builder.Services.AddTransient<INotesWorkerServices, DefaultNotesWorkerServices>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Map("/", () => { });

app.MapControllers();

app.Run();