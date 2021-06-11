using SkillUpApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
    public interface IAuthRepository
    {
        public string CreateToken(Teachers teachers);

        public string CreateSToken(Students students);
    }
    
}
