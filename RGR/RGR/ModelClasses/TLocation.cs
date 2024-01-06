using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR.ModelClasses
{
    public class TLocation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfSeats { get; set; }
        public int OwnerId { get; set; }
    }
}
