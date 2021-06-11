using System;
using System.Collections.Generic;
using System.Text;

namespace SkillUpDTO.DTO
{
   public class RoomCreateDTO
    {
        public int RoomNumber { get; set; }
        public string LocationId { get; set; }
    }

    public class RoomDTO : RoomCreateDTO
    {
        public string RoomsId { get; set; }
    }
}
