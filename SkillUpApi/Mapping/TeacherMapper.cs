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
    public class TeacherMapper:Profile
    {
        public TeacherMapper()
        {
            CreateMap<Teachers, TeacherDTO>()
                .ForMember(o => o.TeacherId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.TeacherId.ToString())));
                //.ForMember(t => t.TeacherId, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.TeacherId.ToString())));
                //.ForMember(t => t.EMail, 
                //opt => opt.MapFrom(s => EncryptionHelper.Decrypt(s.EMail)));
            CreateMap<TeacherCreateDTO, Teachers>()
                .ForMember(t => t.EMail, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.EMail)))
                .ForMember(t => t.Password, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.Password)))
                .ForMember(t => t.DateCreated, opt => opt.MapFrom(d => DateTime.Now));
        }
        //public static IMapper TMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Teachers, TeacherDTO>()
        //            .ForMember(o => o.TeacherId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.TeacherId.ToString())));
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}
    }
}
