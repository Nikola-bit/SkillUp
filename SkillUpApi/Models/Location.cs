using System;
using System.Collections.Generic;

namespace SkillUpApi.Models
{
    public partial class Location
    {
        public Location()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int LocationId { get; set; }
        public string Adress { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
