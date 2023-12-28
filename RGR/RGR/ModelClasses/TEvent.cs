using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR.ModelClasses
{
    public class TEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Theme { get; set; }
        public DateOnly EventDate { get; set; } 
    }
}
