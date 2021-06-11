using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
  public interface ITeacherRepository
    {
        public PagedResponse <List<TeacherDTO>> GetAllTeachers(PaginationFilter filters);

        public TeacherDTO GetTeacherById(string id);

        public TeacherDTO AddTeacher(TeacherCreateDTO teacher);
        public bool DeleteTeacher(string id);
        bool UpdateTeacher(TeacherDTO request);
        public LoginResponseDTO Login(TeacherLoginDTO user);
    }
}
