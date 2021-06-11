using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
    public class StudentCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class StudentDTO : StudentCreateDTO
    {
        public string StudentId { get; set; }
        //public string Token { get; set; }
        

    }
    
    public class StudentLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SLoginResponseDTO
    {
        public StudentDTO student { get; set; }
        public string AuthToken { get; set; }
    }
}
