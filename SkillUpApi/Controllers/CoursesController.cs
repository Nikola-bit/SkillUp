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
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesRepository repository;

        public CoursesController(ICoursesRepository _repository)
        {
            repository = _repository;

        }
        /// <summary>
        /// Get all Courses
        /// </summary>
        /// <returns>All Courses</returns>
        /// <response code = "200">Return all Courses</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("list")]
        public IActionResult GetAllCourses(PaginationFilter paginationFilter)
        {

           PagedResponse <List<CoursesDTO>> courseslist = repository.GetAllCourses(paginationFilter);
            return new ObjectResult(courseslist);

        }
        /// <summary>
        /// Get Session by CourseId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /single/session
        ///     {
        ///         "CourseId" : 1
        ///     }
        ///     
        /// mandatory Field* : CourseId
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code = "200">Return Session by CourseId</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("sessionsByCourse")]
        public IActionResult GetAllSessionByCourseId([FromQuery]string id)
        {
            List<SessionDTO> sessions = repository.GetAllSessionByCourseId(id);
            if (sessions != null)
            {
                return new ObjectResult(sessions);
            }
            else
            {
                return Content("session with that ID is not found!");
            }
        }
        /// <summary>
        /// Get Course by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /single/Course
        ///     {
        ///         "CourseId" : 1
        ///     }
        ///     
        /// mandatory Field* : CourseId
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code = "200">Return Course by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetCursById([FromQuery] string id)
        {
            CoursesDTO course = repository.GetCursById(id);

            if (course != null)
            {
                return new ObjectResult(course);
            }
            else
            {
                return Content("Course with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /course/Create
        ///     {
        ///         "Price" : 200,
        ///         "IsActive": true,
        ///         "CoursetypeId" : 1
        ///      }
        ///      
        /// Mandatory Fields* : Price, IsActive, CourseType
        /// </remarks>
        /// <param name="courseCreate"></param>
        /// <returns>Updated list of Courses</returns>
        /// <response code = "200">Return updated list of Courses</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create")]
        public IActionResult AddCurs([FromBody] CoursesCreateDTO courseCreate)
        {
            CoursesDTO newCourse = repository.AddCurs(courseCreate);

            return new ObjectResult(newCourse);
        }
        /// <summary>
        /// Delete Course by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Course
        ///     {
        ///         "CourseId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Course</returns>
        /// <response code = "200">Return single Course</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("delete")]
        public IActionResult DeleteCourse([FromQuery] string id)
        {
            bool result = repository.DeleteCourse(id);

            return new ObjectResult(result);
        }
        /// <summary>
        /// Update Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Course
        ///         {
        ///             "CourseID": 1,
        ///             "Price" : 200,
        ///             "isActive" : true,
        ///             "CourseTypeId" : 1
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="updateCourse"></param>
        /// <returns>Updated list of courses</returns>
        /// <response code = "200">Return updated list of courses</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("update")]
        public IActionResult UpdateCourse([FromBody] CoursesDTO createCourse)
        {
            if (!string.IsNullOrEmpty(createCourse.CourseId))
            {
                bool result = repository.UpdateCourse(createCourse);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: CourseId");
            }
        }
        
        // FOR LEADS
        /// <summary>
        /// Create a new Lead
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /course/CourseLead/create
        ///     {
        ///         "FirstName" : "Example',
        ///         "LastName": "num1",
        ///         "EMail" : "some@enail",
        ///         "Phone" : "12354",
        ///         "Note" : "some note",
        ///         "IsActive" : 1,
        ///         "CourseId" : 1
        ///      }
        ///      
        /// </remarks>
        /// <param name="leadsCreate"></param>
        /// <returns>Updated list of Leads</returns>
        /// <response code = "200">Return updated list of Leads</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("CourseLead/create")]
        public IActionResult AddLead([FromBody] CourseLeadsCreateDTO leadsCreate)
        {
            CourseLeadsDTO newLead = repository.AddLead(leadsCreate);

            return new ObjectResult(newLead);
        }
        /// <summary>
        /// Get all Leads
        /// </summary>
        /// <returns>All Leads</returns>
        /// <response code = "200">Return all Leads</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("CourseLead/allLeads")]
        public IActionResult GetAllLeads()
        {

            List<CourseLeadsDTO> leadslists = repository.GetAllLeads();
            return new ObjectResult(leadslists);

        }
        /// <summary>
        /// Delete Lead by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Lead
        ///     {
        ///         "LeadId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Lead</returns>
        /// <response code = "200">Return single lead</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("Lead/delete")]
        public IActionResult DeleteLead([FromQuery] string id)
        {
            bool result = repository.DeleteLead(id);

            return new ObjectResult(result);
        }
        /// <summary>
        /// Update Lead
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Lead
        ///         {
        ///            "LeadId": 1,
        ///            "FirstName" : "name",
        ///            "LastName" : "last",
        ///            "email" : "somemail",
        ///            "phone" : "234673",
        ///            "note" : "some note",
        ///            "isActive" : true,
        ///            "CourseId": 1
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="updateLead"></param>
        /// <returns>Updated list of leads</returns>
        /// <response code = "200">Return updated list of leads</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("Lead/update")]
        public IActionResult UpdateLead([FromBody] CourseLeadsDTO updateLead)
        {
            if (!string.IsNullOrEmpty(updateLead.LeadId))
            {
                bool result = repository.UpdateLead(updateLead);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: LeadId");
            }
        }
    }
}