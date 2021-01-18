using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public enum VuilbakType { Restafval, GFT, Groenafval, PapierEnKarton };
    public class Vuilbak
    {
        public int VuilbakID { get; set; }
        public double Volheid { get; set; }
        public VuilbakType Type { get; set; }

        //Relations
        public int? UserID { get; set; }
        public User User { get; set; }
    }
}
