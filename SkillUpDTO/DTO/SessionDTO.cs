using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
    public class SessionCreateDTO
    {
       
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string RoomId { get; set; }
        public string SessionTeacherId { get; set; }
        public string SessionStudentId { get; set; }
        public string CourseId { get; set; }
    }
    public class SessionDTO : SessionCreateDTO
    {
        public string SessionId { get; set; }
    }
}
