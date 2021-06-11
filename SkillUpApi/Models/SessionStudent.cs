using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class SessionStudent
    {
        public SessionStudent()
        {
            Session = new HashSet<Session>();
            SessionStudentNote = new HashSet<SessionStudentNote>();
        }

        public int SessionStudentId { get; set; }
        public int StudentId { get; set; }
        public int SessionId { get; set; }

        public virtual Students Student { get; set; }
        public virtual ICollection<Session> Session { get; set; }
        public virtual ICollection<SessionStudentNote> SessionStudentNote { get; set; }
    }
}
