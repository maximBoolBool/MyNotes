﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using MyNotes.Models;

namespace MyNotes.Services.AuthorizationServices;

public class AuthorizationService : IAuthorizationService
{
    public async Task<ClaimsPrincipal> Authorize( DtoUser person)
    {
        Claim nameClaim = new Claim(ClaimTypes.Name,person.Login);
        Claim passwordClaim = new Claim("Password",person.Password);
        var claims = new List<Claim>()
        {
            nameClaim,
            passwordClaim
        };
        var claimIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);
        return claimPrincipal;
    }
}