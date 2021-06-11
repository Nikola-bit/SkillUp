using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillUpApi.Data;
using SkillUpApi.Filter;
using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;

namespace SkillUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository repository;

        public StudentController(IStudentRepository _repository)
        {
            repository = _repository;

        }
        /// <summary>
        /// Get All Students
        /// </summary>
        /// <returns>All Students</returns>
        /// <response code = "200">Return all Students</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [AuthFilter]
        [HttpPost("list")]
        public IActionResult GetAllStudents([FromBody] PaginationFilter filter)
        {

            PagedResponse <List<StudentDTO>> studentlist = repository.GetAllStudents(filter);
            return new ObjectResult(studentlist);

        }
        /// <summary>
        /// Get Student By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Student
        ///     {
        ///         "StudentId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Student</returns>
        /// <response code = "200">Return Student by Id</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("single")]
        public IActionResult GetStudentById([FromQuery] string id)
        {
            StudentDTO student = repository.GetStudentById(id);

            if (student != null)
            {
                return new ObjectResult(student);
            }
            else
            {
                return Content("Student with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Student
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             "phone" : "23411"
        ///         }
        /// </remarks>
        /// <param name="studentCreate"></param>
        /// <returns>Updated list of students</returns>
        /// <response code = "200">Return updated list of students</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("Create")]
        public IActionResult AddStudent([FromBody] StudentCreateDTO studentCreate)
        {
            studentCreate.EMail = studentCreate.EMail == "string" ? "" : studentCreate.EMail;
            if (string.IsNullOrEmpty(studentCreate.EMail) || string.IsNullOrEmpty(studentCreate.Password))
            {
                return BadRequest("Missing items");
            }

            StudentDTO newStudent = repository.AddStudent(studentCreate);
            return new ObjectResult(newStudent);
        }
        /// <summary>
        /// Update Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Student
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             "phone" : "6512"
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="studentUpdate"></param>
        /// <returns>Updated list of students</returns>
        /// <response code = "200">Return updated list of students</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("update")]
        public IActionResult UpdateStudent([FromBody] StudentDTO studentCreate)
        {
            if (!string.IsNullOrEmpty(studentCreate.StudentId))
            {
                bool result = repository.UpdateStudent(studentCreate);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: StudentId");
            }
        }
        /// <summary>
        /// Delete Student by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Lead
        ///     {
        ///         "StudentId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Student</returns>
        /// <response code = "200">Return single Student</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("Delete")]
        public IActionResult DeleteStudent([FromQuery] string id)
        {
            bool result = repository.DeleteStudent(id);

            return new ObjectResult(result);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] StudentLoginDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required!");
            }
            else
            {
                SLoginResponseDTO response = repository.Login(user);

                if (response != null)

                    return new ObjectResult(response);

                else
                    return NotFound("Wrong Credentials!");
            }
        }
    }
}