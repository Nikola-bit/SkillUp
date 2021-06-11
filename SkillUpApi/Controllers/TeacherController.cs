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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository repository;

        public TeacherController(ITeacherRepository _repository)
        {
            repository = _repository;

        }
        /// <summary>
        /// Get All Teachers
        /// </summary>
        /// <returns>All Teachers</returns>
        /// <response code = "200">Return all Teachers</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("list")]
        public IActionResult GetAllTeachers(PaginationFilter filter)
        {

            PagedResponse <List<TeacherDTO>> teacherlist = repository.GetAllTeachers(filter);
            return new ObjectResult(teacherlist);

        }
        /// <summary>
        /// Get Teacher By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Teacher
        ///     {
        ///         "TeacherId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Teacher</returns>
        /// <response code = "200">Return Teacher by Id</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("single")]
        public IActionResult GetTeacherById([FromQuery] string id)
        {
            TeacherDTO teacher = repository.GetTeacherById(id);

            if (teacher != null)
            {
                return new ObjectResult(teacher);
            }
            else
            {
                return Content("Teacher with that ID is not found!");
            }
        }

        /// <summary>
        /// Create a new Teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Teacher
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             
        ///         }
        ///         
        /// Mandatory Fields* : FirstName, LastName
        /// </remarks>
        /// <param name="teacherCreate"></param>
        /// <returns>Updated list of teachers</returns>
        /// <response code = "200">Return updated list of teachers</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("create")]
        public IActionResult AddTeacher([FromBody] TeacherCreateDTO teacherCreate)
        {
            TeacherDTO newTeacher = repository.AddTeacher(teacherCreate);

            return new ObjectResult(newTeacher);
        }

        /// <summary>
        /// Update Teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/Teacher
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="teacherupdate"></param>
        /// <returns>Updated list of teachers</returns>
        /// <response code = "200">Return updated list of teachers</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("update")]
        public IActionResult UpdateTeacher([FromBody] TeacherDTO teacherCreate)
        {
            if (!string.IsNullOrEmpty(teacherCreate.TeacherId))
            {
                bool result = repository.UpdateTeacher(teacherCreate);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: TeacherId");
            }
        }
        /// <summary>
        /// Delete Teacher by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Teacher
        ///     {
        ///         "TeacherId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Teacher</returns>
        /// <response code = "200">Return single Teacher</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("Delete")]
        public IActionResult DeleteTeacher([FromQuery] string id)
        {
            bool result = repository.DeleteTeacher(id);

            return new ObjectResult(result);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] TeacherLoginDTO  user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required!");
            }
            else
            {
                LoginResponseDTO response = repository.Login(user);
            
            if (response != null)
            
                return new ObjectResult(response);
            
            else
                return NotFound("Wrong Credentials!");
            }
        }
    }
}