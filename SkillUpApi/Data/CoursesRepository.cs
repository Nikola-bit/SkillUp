using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class CoursesRepository:ICoursesRepository
    {
        private readonly IMapper mapper;
        private readonly StoreContext db;
        public CoursesRepository(IMapper _mapper, StoreContext _db)
        {
            mapper = _mapper;
            db = _db;
        }
        public CoursesDTO AddCurs(CoursesCreateDTO coursesCreate)
        {
            Courses newCourses = mapper.Map<Courses>(coursesCreate);
            db.Courses.Add(newCourses);
            db.SaveChanges();

            CoursesDTO result = mapper.Map<CoursesDTO>(newCourses);

            return result;
        }
        public PagedResponse <List<CoursesDTO>> GetAllCourses(PaginationFilter filter)
        {

            List<Courses> list = db.Courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
            // IMapper mapper = CoursesMapper.CousMapper();
            var courses = mapper.Map<List<CoursesDTO>>(list);
            int totalCount = db.Courses.Count();
            PagedResponse<List<CoursesDTO>> response = new PagedResponse<List<CoursesDTO>>()
            {
                Data = courses,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };
            return response;
            //List<CoursesDTO> courses = mapper.Map<List<CoursesDTO>>(list);

            
        }
        public List<SessionDTO> GetAllSessionByCourseId(string id)
        {
            StoreContext db = new StoreContext();
            int courseid = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            List<Session> courses = db.Session.Where(s => s.CourseId == courseid)
                                                                    .Include(s => s.Course).ToList();

            //List<Courses> list = courses.Select(x => x.Course).ToList();
           // IMapper mapper1 = CoursesMapper.CoursesMapper();//SessionMapper.SesMapper();
           List<SessionDTO> result = mapper.Map<List<SessionDTO>>(courses);

            return result;
        }
        public CoursesDTO GetCursById(string id)
        {
            StoreContext db = new StoreContext();
            int courseid = Convert.ToInt32(EncryptionHelper.Decrypt(id));


            Courses courses = db.Courses.Where(c => c.CourseId == courseid).FirstOrDefault();

            if (courses != null)
            {
               // IMapper map = CoursesMapper.CousMapper();
                CoursesDTO result = mapper.Map<CoursesDTO>(courses);

                return result;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteCourse(string id)
        {
            StoreContext db = new StoreContext();
            int courseid = Convert.ToInt32(EncryptionHelper.Decrypt(id));


            Courses courses = db.Courses.Where(c => c.CourseId == courseid).FirstOrDefault();
            //Courses courses = db.Courses.Where(c => c.CourseId == id).FirstOrDefault();

            if (courses != null)
            {
                db.Courses.Remove(courses);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateCourse(CoursesDTO request)
        {
            int courseId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CourseId));

            Courses updatecourse = db.Courses.Where(s => s.CourseId == courseId).FirstOrDefault();
            if (updatecourse != null)
            {
                updatecourse.CourseId = courseId;
                updatecourse.IsActive = request.IsActive;
                updatecourse.Price = request.Price;
                updatecourse.CoursetypeId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CoursetypeId)); 
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        
        

        // FOR LEADS
        public CourseLeadsDTO AddLead(CourseLeadsCreateDTO leadsCreate)
        {
            CourseLeads newLeads = mapper.Map<CourseLeads>(leadsCreate);
            db.CourseLeads.Add(newLeads);
            db.SaveChanges();

            CourseLeadsDTO result = mapper.Map<CourseLeadsDTO>(newLeads);

            return result;
        }
        public List<CourseLeadsDTO> GetAllLeads()
        {
            List<CourseLeads> list = db.CourseLeads.ToList();

            //List<CourseLeadsDTO> leadss = mapper.Map<List<CourseLeadsDTO>>(list);
            //IMapper mapper = CoursesMapper.CourseLeadsMapper();
            var leadss = mapper.Map<List<CourseLeadsDTO>>(list);
            

            return leadss;
        }
        public bool DeleteLead(string id)
        {
            StoreContext db = new StoreContext();
            int leadId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
            CourseLeads leadss = db.CourseLeads.Where(l => l.LeadId == leadId).FirstOrDefault();

            if (leadss != null)
            {
                db.CourseLeads.Remove(leadss);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateLead(CourseLeadsDTO request)
        {
            int leadId = Convert.ToInt32(EncryptionHelper.Decrypt(request.LeadId));

            CourseLeads update = db.CourseLeads.Where(s => s.LeadId == leadId).FirstOrDefault();
            if (update != null)
            {
                update.LeadId = leadId;
                update.CourseId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CourseId));
                update.FirstName = request.FirstName;
                update.LastName = request.LastName;
                update.EMail = request.EMail;
                update.Phone = request.Phone;
                update.Note = request.Note;
                update.IsActive = request.IsActive;
                update.DateCreated = request.DateCreated;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
