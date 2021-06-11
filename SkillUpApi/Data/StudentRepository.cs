using AutoMapper;
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
    public class StudentRepository:IStudentRepository
    {
        private readonly IMapper mapper;
        private readonly IAuthRepository authStudentRepository;
        private readonly StoreContext db;
        public StudentRepository(IMapper _mapper, IAuthRepository _authStudentRepository)
        {
            mapper = _mapper;
            authStudentRepository = _authStudentRepository;
           // db = _db;
        }
        public StudentDTO AddStudent(StudentCreateDTO student)
        {
            StoreContext db = new StoreContext();
            Students newStudent = mapper.Map<Students>(student);
            db.Students.Add(newStudent);
            db.SaveChanges();

            StudentDTO result = mapper.Map<StudentDTO>(newStudent);

            return result;
        }
        public bool UpdateStudent(StudentDTO request)
        {
            StoreContext db = new StoreContext();
            int studentId = Convert.ToInt32(EncryptionHelper.Decrypt(request.StudentId));

            Students updateStudent = db.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
            if (updateStudent != null)
            {
                updateStudent.FirstName = request.FirstName;
                updateStudent.LastName = request.LastName;
                updateStudent.EMail = request.EMail;
                updateStudent.Phone = request.Phone;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public PagedResponse <List<StudentDTO>> GetAllStudents(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();
            List<Students> list = db.Students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
           // IMapper mapper = StudentMapper.SMapper();
            var students = mapper.Map<List<StudentDTO>>(list);
            int totalCount = db.Students.Count();
            // List<StudentDTO> students = mapper.Map<List<StudentDTO>>(list);
            PagedResponse<List<StudentDTO>> response = new PagedResponse<List<StudentDTO>>()
            {
                Data = students,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };
            return response;
        }
        public StudentDTO GetStudentById(string id)
        {
            StoreContext db = new StoreContext();
            int stId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
            
            Students studentss = db.Students.Where(s => s.StudentId == stId).FirstOrDefault();

            if (studentss != null)
            {
                //IMapper mapper = StudentMapper.SMapper();
                StudentDTO result = mapper.Map<StudentDTO>(studentss);

                return result;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteStudent(string id)
        {
            StoreContext db = new StoreContext();
            int studentId = Convert.ToInt32(EncryptionHelper.Decrypt(id));

            Students students = db.Students.Where(s => s.StudentId == studentId).FirstOrDefault();

            if (students != null)
            {
                db.Students.Remove(students);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public SLoginResponseDTO Login(StudentLoginDTO user)
        {
            StoreContext db = new StoreContext();

            string encryptedEmail = EncryptionHelper.Encrypt(user.Email);
            string encryptedPassword = EncryptionHelper.Encrypt(user.Password);

            Students studentss = db.Students.Where(x => x.EMail == encryptedEmail
                                                    && x.Password == encryptedPassword).FirstOrDefault();

            if (studentss != null)
            {
                return new SLoginResponseDTO()
                {
                    student = mapper.Map<StudentDTO>(studentss),
                    AuthToken = authStudentRepository.CreateSToken(studentss)
                };
            }
            else
            {
                return null;
            }

        }
    }
}
