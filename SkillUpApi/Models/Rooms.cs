using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Session = new HashSet<Session>();
        }

        public int RoomsId { get; set; }
        public int RoomNumber { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
