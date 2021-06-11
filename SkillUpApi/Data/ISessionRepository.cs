using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
   public interface ISessionRepository
    {
        public PagedResponse <List<SessionDTO>> GetAllSessions(PaginationFilter sfilter);

        public SessionDTO GetSessionById(string id);

        public SessionDTO AddSession(SessionCreateDTO session);
        public bool DeleteSession(string id);
        public bool UpdateSession(SessionDTO updatesession);
        public SessionStudentNoteDTO AddNote(SesssionStudentNoteCreateDTO note);
        public List<SessionStudentNoteDTO> GetAllNotes();
        public bool DeleteNote(string id);
        public bool UpdateNote(SessionStudentNoteDTO updateNote);
        public List<StudentDTO> GetAllStudentBySessionId(string id);
        public List<RoomDTO> GetAllFreeRooms();
        public List<RoomDTO> GetRoomByLocation(string id);
    }
}
