using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
    public class CoursesCreateDTO
    {
        
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public string CoursetypeId { get; set; }
    }
    public class CoursesDTO : CoursesCreateDTO
    {
        public string CourseId { get; set; }
    }
}
