using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Neme { get; set; }
        public double Latitude { get; set; }
        public double Longtude { get; set; }
        public int Order { get; set; }
        public DateTime Arrivial { get; set; }
    }
}
