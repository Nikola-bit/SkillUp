using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillUpApi.Mapping;
using SkillUpApi.Models;
using SkillUpApi.Security;
using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
    public class SessionRepository : ISessionRepository
    {

        private readonly IMapper mapper;
        private readonly StoreContext db;
        public SessionRepository(IMapper _mapper, StoreContext _db)
        {
            mapper = _mapper;
            db = _db;
        }
        public SessionDTO AddSession(SessionCreateDTO sessionCreate)
        {
            Session newSessions = mapper.Map<Session>(sessionCreate);
            db.Session.Add(newSessions);
            db.SaveChanges();

            SessionDTO result = mapper.Map<SessionDTO>(newSessions);

            return result;
        }
        public PagedResponse <List<SessionDTO>> GetAllSessions(PaginationFilter filter)
        {
            List<Session> list = db.Session
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
            var sessions = mapper.Map<List<SessionDTO>>(list);
            int totalCount = db.Session.Count();
            PagedResponse<List<SessionDTO>> response = new PagedResponse<List<SessionDTO>>()
            {
                Data = sessions,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };
            return response;

        }
        public List<RoomDTO> GetAllFreeRooms()
        {
            // za da gi zeme site rooms
            List<int> rooms = db.Session.Select(x => x.RoomId).ToList();

            List<Rooms> freeRooms = db.Rooms.Where(x => !rooms.Contains(x.RoomsId )).ToList();
            var maprooms = mapper.Map<List<RoomDTO>>(freeRooms);
            return maprooms;

        }
        public List<StudentDTO> GetAllStudentBySessionId(string id)
        {
            StoreContext db = new StoreContext();
            int sessionid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            List<SessionStudent> sessionStudents = db.SessionStudent.Where(s => s.SessionId == sessionid)
                                                                    .Include(s => s.Student).ToList();

            List<Students> list = sessionStudents.Select(x => x.Student).ToList();
            List<StudentDTO> result = mapper.Map<List<StudentDTO>>(list);

            return result;
        }
        public List<RoomDTO> GetRoomByLocation(string id)
        {
            StoreContext db = new StoreContext();
            int locationid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            List<Rooms> rooms = db.Rooms.Where(s => s.LocationId == locationid)
                                                                     .Include(s => s.Location).ToList();          
            List<RoomDTO> result = mapper.Map<List<RoomDTO>>(rooms);

            return result;
        }
        public SessionDTO GetSessionById(string id)
        {
            StoreContext db = new StoreContext();
            int sessionid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            Session sessionss = db.Session.Where(s => s.SessionId == sessionid).FirstOrDefault();

            if (sessionss != null)
            {
                //IMapper mapper = SessionMapper.SesMapper();
                SessionDTO result = mapper.Map<SessionDTO>(sessionss);

                return result;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteSession(string id)
        {
            StoreContext db = new StoreContext();
            int sessionid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            Session session = db.Session.Where(c => c.SessionId == sessionid).FirstOrDefault();

            if (session != null)
            {
                db.Session.Remove(session);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateSession(SessionDTO request)
        {
            int sessionid = Convert.ToInt32(EncryptionHelper.Decrypt(request.SessionId));
            Session update = db.Session.Where(c => c.SessionId == sessionid).FirstOrDefault();
            if (update != null)
            {
                update.SessionId = sessionid;
                update.DateFrom = request.DateFrom;
                update.DateTo = request.DateTo;
                update.RoomId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RoomId));
                update.CourseId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CourseId));
                update.SessionStudentId = Convert.ToInt32(EncryptionHelper.Decrypt(request.SessionStudentId));
                update.SessionTeacherId = Convert.ToInt32(EncryptionHelper.Decrypt(request.SessionTeacherId));
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        // FOR NOTE
        public SessionStudentNoteDTO AddNote(SesssionStudentNoteCreateDTO noteCreate)
        {


            SessionStudent student = db.SessionStudent.Where(x => x.SessionStudentId == Convert.ToInt32(EncryptionHelper.Decrypt(noteCreate.SessionStudentid)))
                .FirstOrDefault();

            if (student != null)
            {

                SessionStudentNote newSessionsNote = mapper.Map<SessionStudentNote>(noteCreate);
                db.SessionStudentNote.Add(newSessionsNote);
                db.SaveChanges();

                SessionStudentNoteDTO result = mapper.Map<SessionStudentNoteDTO>(newSessionsNote);

                return result;
            }
            else return null;
        }
        public List<SessionStudentNoteDTO> GetAllNotes()
        {
            List<SessionStudentNote> list = db.SessionStudentNote.ToList();
           // IMapper mapper = SessionMapper.NoteMapper();
            var sessions = mapper.Map<List<SessionStudentNoteDTO>>(list);
            // List<SessionDTO> sessionsList = mapper.Map<List<SessionDTO>>(list);

            return sessions;
        }
        public bool DeleteNote(string id)
        {
            StoreContext db = new StoreContext();
            int noteid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            SessionStudentNote sessionss = db.SessionStudentNote.Where(s => s.Noteid == noteid).FirstOrDefault();

            if (sessionss != null)
            {
                db.SessionStudentNote.Remove(sessionss);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateNote(SessionStudentNoteDTO request)
        {
            int sessionNoteid = Convert.ToInt32(EncryptionHelper.Decrypt(request.Noteid));
            SessionStudentNote update = db.SessionStudentNote.Where(c => c.Noteid == sessionNoteid).FirstOrDefault();
            if (update != null)
            {
                update.Noteid = sessionNoteid;
                update.DateCreated = request.DateCreated;
                update.Note = request.Note;
                update.SessionStudentid = Convert.ToInt32(EncryptionHelper.Decrypt(request.SessionStudentid));
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
