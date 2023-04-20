using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using MyNotes;
using MyNotes.Context;
using MyNotes.Services.IdentytiUserServices;
using MyNotes.Services.NotesWorkerServices;
using MyNotes.Services.UserAcountWorkersServices;

var builder = WebApplication.CreateBuilder();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(opt=>opt.UseNpgsql(connection));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
           ValidateIssuer  = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDINCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddTransient<IUserAcountWorker, DefaultUserAcountWorkerService>();
builder.Services.AddTransient<INotesWorkerServices, DefaultNotesWorkerServices>();
builder.Services.AddTransient<IIdentityUserService, DefaultIdentityService>();


var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

app.Map("/", () => { });

app.MapControllers();

app.Run();