using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class SessionTeacher
    {
        public SessionTeacher()
        {
            Session = new HashSet<Session>();
        }

        public int SessionTeacherId { get; set; }
        public int TeacherId { get; set; }
        public int SessionId { get; set; }

        public virtual Teachers Teacher { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
