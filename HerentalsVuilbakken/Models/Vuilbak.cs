using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Vuilbak
    {
        public int VuilbakID { get; set; }
        public double Volheid { get; set; }
        public string Straat { get; set; }
        public double Breedtegraad { get; set; }
        public double Lengtegraad { get; set; }
    }
}
