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
    public class CoursesMapper:Profile
    {
        public CoursesMapper()
        {
            CreateMap<Courses, CoursesDTO>()
                .ForMember(o => o.CourseId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.CourseId.ToString())))
                .ForMember(o => o.CoursetypeId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.CoursetypeId.ToString()))); ;
            CreateMap<CoursesCreateDTO, CoursesDTO>();
            CreateMap<CourseLeads, CourseLeadsDTO>()
                .ForMember(o => o.LeadId, opt => opt.MapFrom(l => EncryptionHelper.Encrypt(l.LeadId.ToString())))
                 .ForMember(o => o.CourseId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.CourseId.ToString())));
            CreateMap<CourseLeadsCreateDTO, CourseLeads>()
                .ForMember(l=>l.CourseId,opt=>opt.MapFrom(c=>EncryptionHelper.Decrypt(c.CourseId).ToString()))
            .ForMember(x => x.DateCreated, 
                opt => opt.MapFrom(d => DateTime.Now));
        }
         
        //public static IMapper CourseLeadsMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<CourseLeads, CourseLeadsDTO>()
        //            .ForMember(o => o.LeadId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.LeadId.ToString())))
        //            .ForMember(o => o.CourseId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.CourseId.ToString())));
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}
        //public static IMapper CousMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Courses, CoursesDTO>()
        //            .ForMember(o => o.CourseId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.CourseId.ToString())))
        //            .ForMember(o => o.CoursetypeId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.CoursetypeId.ToString())));
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}

    }

}
