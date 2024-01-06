using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR.ModelClasses
{
    public class TEvent
    {
        public int ID { get; set; }
        public int EventNameId { get; set; }
        public string Theme { get; set; }
        public DateOnly EventDate { get; set; } 
    }
}
