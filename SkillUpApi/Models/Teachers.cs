using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Teachers
    {
        public Teachers()
        {
            SessionTeacher = new HashSet<SessionTeacher>();
        }

        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Password { get; set; }

        public virtual ICollection<SessionTeacher> SessionTeacher { get; set; }
    }
}
