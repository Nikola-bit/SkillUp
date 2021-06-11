using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Students
    {
        public Students()
        {
            SessionStudent = new HashSet<SessionStudent>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Password { get; set; }

        public virtual ICollection<SessionStudent> SessionStudent { get; set; }
    }
}
