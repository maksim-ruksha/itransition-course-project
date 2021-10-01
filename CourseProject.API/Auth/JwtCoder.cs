using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseProject.BLL.Models;
using CourseProject.BLL.Services;
using CourseProject.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CourseProject.Auth
{
    public class JwtCoder
    {
        public static string Encode(ClaimsIdentity claimsIdentity)
        {
            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                JwtAuthOptions.Issuer,
                JwtAuthOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(JwtAuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(JwtAuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static JwtSecurityToken Decode(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken
            );

            return (JwtSecurityToken) validatedToken;
        }
    }
}