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
    public class RoomMapper:Profile
    {
        public RoomMapper()
        {
            CreateMap<Rooms, RoomDTO>()
                   .ForMember(o => o.RoomsId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.RoomsId.ToString())))
                   .ForMember(c => c.LocationId, opt => opt.MapFrom(o => EncryptionHelper.Encrypt(o.LocationId.ToString())));
                   
            CreateMap<RoomCreateDTO, Rooms>();
        }
    }
}
