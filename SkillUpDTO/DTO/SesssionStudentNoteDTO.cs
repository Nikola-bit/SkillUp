using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
    public class SesssionStudentNoteCreateDTO
    {
        
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public string SessionStudentid { get; set; }
    }
    public class SessionStudentNoteDTO : SesssionStudentNoteCreateDTO
    {
        public string Noteid { get; set; }
    }
}
