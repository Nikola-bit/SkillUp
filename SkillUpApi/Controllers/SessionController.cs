using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillUpApi.Data;
using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;

namespace SkillUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository repository;

        public SessionController(ISessionRepository _repository)
        {
            repository = _repository;

        }
        /// <summary>
        /// Get All Sessions
        /// </summary>
        /// <returns>All Sessions</returns>
        /// <response code = "200">Return all session</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("list")]
        public IActionResult GetAllSessions(PaginationFilter filt)
        {
           PagedResponse <List<SessionDTO>> sessionlist = repository.GetAllSessions(filt);
            return new ObjectResult(sessionlist);

        }
        /// <summary>
        /// Get AllS tudent By SessionId
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /students/BySession
        ///     {
        ///         "SessonId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Students</returns>
        /// <response code = "200">Return students</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("studentsBySession")]
        public IActionResult GetAllStudentBySessionId([FromQuery]string id)
        {
            List<StudentDTO> studentDTO = repository.GetAllStudentBySessionId(id);
            if (studentDTO != null)
            {
                return new ObjectResult(studentDTO);
            }
            else
            {
                return Content("session with that ID is not found!");
            }
        }
        /// <summary>
        /// Get Session by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Session
        ///     {
        ///         "SessonId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single session</returns>
        /// <response code = "200">Return single session</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetSessionById([FromQuery] string id)
        {
            SessionDTO session = repository.GetSessionById(id);

            if (session != null)
            {
                //return new ObjectResult(session);
                return new ObjectResult(new Response<SessionDTO>(session));
            }
            else
            {
                return Content("session with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Session
        /// </summary>
        /// <remarks >
        /// 
        /// Sample request:
        /// 
        ///     POST /session/Create
        ///     {
        ///         "DateFrom": 2020-07-23,
        ///         "DateTo": 2020-08-12,
        ///         "RoomId": 1,
        ///         "SessionTeacherId": 1,
        ///         "SessionStudentId": 1,
        ///         "CourseId":1
        ///     }
        /// </remarks>
        /// <param name="sessionCreate"></param>
        /// <returns>Updated list of session</returns>
        /// <response code = "200">Return updated list of session</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create")]

        public IActionResult AddSession([FromBody] SessionCreateDTO sessionCreate)
        {
            SessionDTO newSession = repository.AddSession(sessionCreate);

            return new ObjectResult(newSession);
        }
        /// <summary>
        /// Delete Session by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Session
        ///     {
        ///         "SessionId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Session</returns>
        /// <response code = "200">Return single Session</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("Delete")]
        public IActionResult DeleteSession([FromQuery] string id)
        {
            bool result = repository.DeleteSession(id);

            return new ObjectResult(result);
        }
        /// <summary>
        /// Update Session
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Session
        ///         {
        ///             "Sessionid": 1,
        ///             "RoomId" : 1,
        ///             "SessionTeacherId" : 1,
        ///             "SessionStudentId" : 1,
        ///             "CourseId": 1,
        ///             "DateFrom": 2020-07-08,
        ///             "Date to" 2020-10-10
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="updateSession"></param>
        /// <returns>Updated list of sessions</returns>
        /// <response code = "200">Return updated list of sessions</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("update")]
        public IActionResult UpdateSession([FromBody] SessionDTO updateSession)
        {
            if (!string.IsNullOrEmpty(updateSession.SessionId))
            {
                bool result = repository.UpdateSession(updateSession);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: CourseId");
            }
        }
        /// <summary>
        /// Get All Free Rooms
        /// </summary>
        /// <returns>All free rooms</returns>
        /// <response code = "200">Return all free rooms</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("free/rooms")]
        public IActionResult GetAllFreeRooms()
        {

            List<RoomDTO> roomlist = repository.GetAllFreeRooms();
            return new ObjectResult(roomlist);

        }
        /// <summary>
        /// Get Room by LocationId
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Room
        ///     {
        ///         "LocationId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single room</returns>
        /// <response code = "200">Return single room</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("roomBylocation")]
        public IActionResult GetRoomByLocation([FromQuery]string id)
        {
            List<RoomDTO> rooms = repository.GetRoomByLocation(id);
            if (rooms != null)
            {
                return new ObjectResult(rooms);
            }
            else
            {
                return Content("Room with that LocationID is not found!");
            }
        }

        // FOR NOTE
        /// <summary>
        /// Create a new Note
        /// </summary>
        /// <remarks >
        /// 
        /// Sample request:
        /// 
        ///     POST /Session/Student/Note/Create
        ///     {
        ///         "Note" : "Sample note",
        ///         "SessionStudentid" : 1
        ///     }
        /// </remarks>
        /// <returns>Updated list of notes</returns>
        /// <response code = "200">Return updated list of notes</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("Student/Note/Create")]

        public IActionResult AddNote([FromBody] SesssionStudentNoteCreateDTO sessionCreateNote)
        {
            SessionStudentNoteDTO newSessionNote = repository.AddNote(sessionCreateNote);

            if (newSessionNote != null)
                return new ObjectResult(newSessionNote);
            else
                return NotFound("Student not found!");
        }
        /// <summary>
        /// Get All notes
        /// </summary>
        /// <returns>All notes</returns>
        /// <response code = "200">Return all notes</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("list/notes")]
        public IActionResult GetAllNotes()
        {

            List<SessionStudentNoteDTO> notelist = repository.GetAllNotes();
            return new ObjectResult(notelist);

        }
        /// <summary>
        /// Delete Note by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Note
        ///     {
        ///         "NoteId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single note</returns>
        /// <response code = "200">Return single note</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("Student/Note/Delete")]
        public IActionResult DeleteNote([FromQuery] string id)
        {
            bool result = repository.DeleteNote(id);

            return new ObjectResult(result);
        }
        /// <summary>
        /// Update Note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Note
        ///         {
        ///             "NoteId": 1,
        ///             "Note": "some note",
        ///             "SessionStudentid" : 1      
        ///         }
        ///         
        /// </remarks>
        /// <param name="updateNote"></param>
        /// <returns>Updated list of notes</returns>
        /// <response code = "200">Return updated list of notes</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("update/Note")]
        public IActionResult UpdateNote([FromBody] SessionStudentNoteDTO updateNote)
        {
            bool result = repository.UpdateNote(updateNote);
            return new ObjectResult(result);
        }
    }
}