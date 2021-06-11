using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class CourseType
    {
        public CourseType()
        {
            Courses = new HashSet<Courses>();
        }

        public int CourseTypeId { get; set; }
        public string BackEnd { get; set; }
        public string FrontEnd { get; set; }
        public string Android { get; set; }
        public string Ios { get; set; }
        public string Kids { get; set; }
        public int RoomId { get; set; }

        public virtual ICollection<Courses> Courses { get; set; }
    }
}
