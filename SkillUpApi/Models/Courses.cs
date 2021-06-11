using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Courses
    {
        public Courses()
        {
            CourseLeads = new HashSet<CourseLeads>();
            Session = new HashSet<Session>();
        }

        public int CourseId { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public int CoursetypeId { get; set; }
        public int? SessionId { get; set; }

        public virtual CourseType Coursetype { get; set; }
        public virtual ICollection<CourseLeads> CourseLeads { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
