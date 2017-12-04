using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.OmedM.Domain.Models
{
   public class ListLocation
    {
        public string NameList { get; set; }
        public Location Location { get; set; }
        public List<Location> Locations { get; set; }
    }
}
