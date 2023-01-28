using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JsonWebTokenWork
{
    public class JwtTokenGenerator

    {
        //Token Üretme metotdumuz
        public string GenerateToken()      // ctrl + shift + space metotların overloadunu göstermeye yarayan bir kısayol
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hakan_hakan_hakan_hakan123."));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Member"));
            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", audience: "http://localhost",claims:null, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
