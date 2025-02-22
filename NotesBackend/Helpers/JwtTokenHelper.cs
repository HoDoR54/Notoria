﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

public class JwtTokenHelper
{
    private readonly string _secretKey;

    public JwtTokenHelper (IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:Key"];
    }

    public string GetSecretKey()
    {
        return _secretKey;
    }


    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7060/",
            audience: "https://localhost:7060",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
