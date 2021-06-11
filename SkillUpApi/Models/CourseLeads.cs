using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class CourseLeads
    {
        public int LeadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public int CourseId { get; set; }

        public virtual Courses Course { get; set; }
    }
}
