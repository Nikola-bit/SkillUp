using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class TeacherRepository:ITeacherRepository
    {
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        private readonly StoreContext db;
        public TeacherRepository(IMapper _mapper,  IAuthRepository _authRepository)
        {
            mapper = _mapper;
           // db = _db;
            authRepository = _authRepository;
        }
        public TeacherDTO AddTeacher(TeacherCreateDTO teacher)
        {
            StoreContext db = new StoreContext();
            Teachers newTeacher = mapper.Map<Teachers>(teacher);
            db.Teachers.Add(newTeacher);
            db.SaveChanges();

            TeacherDTO result = mapper.Map<TeacherDTO>(newTeacher);

            return result;
        }
       
        public bool UpdateTeacher(TeacherDTO request)
        {
            StoreContext db = new StoreContext();
            int teacherId = Convert.ToInt32(EncryptionHelper.Decrypt(request.TeacherId));

            Teachers updateteacher = db.Teachers.Where(s => s.TeacherId == teacherId).FirstOrDefault();
            if (updateteacher != null)
            {
                updateteacher.FirstName = request.FirstName;
                updateteacher.LastName = request.LastName;
                updateteacher.EMail = request.EMail;
                updateteacher.DateCreated = request.DateCreate;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public PagedResponse <List<TeacherDTO>> GetAllTeachers(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();
            List<Teachers> list = db.Teachers
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            // List<TeacherDTO> teachers = mapper.Map<List<TeacherDTO>>(list);
            //IMapper mapper = TeacherMapper.TMapper();
            List<TeacherDTO> teachers = mapper.Map<List<TeacherDTO>>(list);
            int totalCount = db.Teachers.Count();
            PagedResponse<List<TeacherDTO>> response = new PagedResponse<List<TeacherDTO>>()
            {
                Data = teachers,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };
            return response;

        }
        public TeacherDTO GetTeacherById(string id)
        {
            StoreContext db = new StoreContext();
            int teacherId = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            Teachers teacherss = db.Teachers.Where(t => t.TeacherId == teacherId).FirstOrDefault();

            if (teacherss != null)
            {
                //IMapper mapper = TeacherMapper.TMapper();
                TeacherDTO result = mapper.Map<TeacherDTO>(teacherss);

                return result;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteTeacher(string id)
        {
            StoreContext db = new StoreContext();
            int teacId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
            Teachers teachers = db.Teachers.Where(t => t.TeacherId == teacId).FirstOrDefault();

            if (teachers != null)
            {
                db.Teachers.Remove(teachers);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public LoginResponseDTO Login(TeacherLoginDTO user)
        {
            StoreContext db = new StoreContext();

            string encryptedEmail = EncryptionHelper.Encrypt(user.Email);
            string encryptedPassword = EncryptionHelper.Encrypt(user.Password);
            
            Teachers teacherss = db.Teachers.Where(x => x.EMail == encryptedEmail
                                                    && x.Password == encryptedPassword).FirstOrDefault();

            if (teacherss != null)
            {
                return new LoginResponseDTO()
                {
                   // IMapper map=TeacherMapper.TMapper();
                    teacher = mapper.Map<TeacherDTO>(teacherss),
                    AuthToken = authRepository.CreateToken(teacherss)
                };
            }
            else
            {
                return null;
            }

        }

    }
}
