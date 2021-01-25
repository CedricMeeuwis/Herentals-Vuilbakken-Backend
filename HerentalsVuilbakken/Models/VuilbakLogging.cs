using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class VuilbakLogging
    {
        public int VuilbakLoggingID { get; set; }
        public int VuilbakID { get; set; }
        public double Gewicht { get; set; }
        public double Volheid { get; set; }
    }
}
