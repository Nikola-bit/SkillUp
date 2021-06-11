using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class SessionStudentNote
    {
        public int Noteid { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public int SessionStudentid { get; set; }

        public virtual SessionStudent SessionStudent { get; set; }
    }
}
