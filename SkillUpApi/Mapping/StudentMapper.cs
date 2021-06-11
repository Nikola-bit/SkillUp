using AutoMapper;
using SkillUpApi.Models;
using SkillUpApi.Security;
using SkillUpDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Mapping
{
    public class StudentMapper:Profile
    {
        public StudentMapper()
        {
            CreateMap<Students, StudentDTO>()
                .ForMember(d => d.StudentId, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.StudentId.ToString())))
                .ForMember(t => t.EMail,opt => opt.MapFrom(s => EncryptionHelper.Decrypt(s.EMail))); 
            CreateMap<StudentCreateDTO, Students>()
                .ForMember(s => s.EMail, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.EMail)))
                .ForMember(t => t.Password, opt => opt.Ignore())
                .ForMember(s => s.DateCreated, opt => opt.MapFrom(d => DateTime.Now));
        }
        //public static IMapper SMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Students, StudentDTO>()
        //            .ForMember(o => o.StudentId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.StudentId.ToString())));
                    
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}

    }
}
