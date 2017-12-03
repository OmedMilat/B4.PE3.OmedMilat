using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.OmedM.Domain.Models
{
    public class Location
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime GpsTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        //public List<Location> test { get; set; }
    }
}
