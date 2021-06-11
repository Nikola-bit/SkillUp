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
    public class SessionMapper:Profile
    {
        public SessionMapper()
        {
            CreateMap<Session, SessionDTO>()
                    .ForMember(o => o.SessionId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionId.ToString())))
                    .ForMember(c => c.CourseId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.CourseId.ToString())))
                    .ForMember(c => c.SessionStudentId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionStudentId.ToString())))
                    .ForMember(c => c.SessionTeacherId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionTeacherId.ToString())))
                    .ForMember(c => c.RoomId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.RoomId.ToString()))); ;
            CreateMap<SessionCreateDTO, Session>();
                
           
            CreateMap<SessionStudentNote, SessionStudentNoteDTO>()
                .ForMember(o => o.Noteid, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.Noteid.ToString())))
                .ForMember(c => c.SessionStudentid, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionStudentid.ToString()))); ;
            CreateMap<SesssionStudentNoteCreateDTO, SessionStudentNote>()
                 .ForMember(s => s.DateCreated, opt => opt.MapFrom(d => DateTime.Now))
                 .ForMember(e => e.SessionStudentid, opt => opt.MapFrom(x => EncryptionHelper.Decrypt(x.SessionStudentid.ToString())));
        }
        //public static IMapper SesMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Session, SessionDTO>()
        //            .ForMember(o => o.SessionId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionId.ToString())))
        //            .ForMember(c => c.CourseId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.CourseId.ToString())))
        //            .ForMember(c => c.SessionStudentId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionStudentId.ToString())))
        //            .ForMember(c => c.SessionTeacherId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionTeacherId.ToString())))
        //            .ForMember(c => c.RoomId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.RoomId.ToString())));

        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}
        //public static IMapper NoteMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<SessionStudentNote, SessionStudentNoteDTO>()
        //            .ForMember(o => o.Noteid, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.Noteid.ToString())))
        //            .ForMember(c => c.SessionStudentid, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.SessionStudentid.ToString())));

        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper;
        //}
    }
    
}
