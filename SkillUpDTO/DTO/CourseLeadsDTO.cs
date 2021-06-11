using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
   public class CourseLeadsCreateDTO
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string CourseId { get; set; }
    }
    public class CourseLeadsDTO :CourseLeadsCreateDTO
    {
        public string LeadId { get; set; }
    }
}
