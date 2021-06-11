using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
    public interface IStudentRepository
    {
        public PagedResponse <List<StudentDTO>> GetAllStudents(PaginationFilter filter);

        StudentDTO GetStudentById(string id);

         StudentDTO AddStudent(StudentCreateDTO student);
         bool DeleteStudent(string id);
        bool UpdateStudent(StudentDTO request);
         SLoginResponseDTO Login(StudentLoginDTO user);
    }
}
