using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
   public  class TeacherCreateDTO
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }
    }

    public class TeacherDTO:TeacherCreateDTO
    {
        public string TeacherId { get; set; }
        //public string token { get; set; }
    }
    public class TeacherLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public TeacherDTO teacher { get; set; }
        public string AuthToken { get; set; }
    }
}
