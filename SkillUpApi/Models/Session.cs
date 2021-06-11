using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Session
    {
        public int SessionId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomId { get; set; }
        public int SessionTeacherId { get; set; }
        public int SessionStudentId { get; set; }
        public int CourseId { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual SessionStudent SessionStudent { get; set; }
        public virtual SessionTeacher SessionTeacher { get; set; }
    }
}
