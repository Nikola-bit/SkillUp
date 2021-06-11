﻿using Microsoft.IdentityModel.Tokens;
using SkillUpApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
    public class AuthRepository:IAuthRepository
    {
        public string CreateToken(Teachers teachers)
        {
            //User information (name, email etc..)
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, teachers.TeacherId.ToString()),
                new Claim(ClaimTypes.Name, teachers.EMail)
            };

            //Read the token key used for token creation/validation
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.TokenKey));

            //Create token credentials from the key provided
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Token settings like lifetime, user informations, key etc.
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Create a token according the options from the token desctiptor
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            //Return the token as string
            return tokenHandler.WriteToken(token);
        }
        public string CreateSToken(Students students)
        {
            //User information (name, email etc..)
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, students.StudentId.ToString()),
                new Claim(ClaimTypes.Name, students.EMail)
            };

            //Read the token key used for token creation/validation
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.TokenKey));

            //Create token credentials from the key provided
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Token settings like lifetime, user informations, key etc.
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Create a token according the options from the token desctiptor
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            //Return the token as string
            return tokenHandler.WriteToken(token);
        }
    }
    
}

