using SkillUpApi.Wrappers;
using SkillUpDTO.DTO;
using SkillUpDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Data
{
    public interface ICoursesRepository
    {
        public PagedResponse <List<CoursesDTO>> GetAllCourses(PaginationFilter cfilter);

        public CoursesDTO GetCursById(string id);

        public CoursesDTO AddCurs(CoursesCreateDTO courses);
        public bool DeleteCourse(string id);
        public bool UpdateCourse(CoursesDTO courseRequest);
        public CourseLeadsDTO AddLead(CourseLeadsCreateDTO leads);
        public List<CourseLeadsDTO> GetAllLeads();
        public bool DeleteLead(string id);
        public bool UpdateLead(CourseLeadsDTO leadRequest);
        public List<SessionDTO> GetAllSessionByCourseId(string id);

    }
}
