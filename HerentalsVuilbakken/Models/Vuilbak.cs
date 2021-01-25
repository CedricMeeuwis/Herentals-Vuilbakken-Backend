using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Vuilbak
    {
        public int VuilbakID { get; set; }
        public double Volheid { get; set; }
        public double Gewicht { get; set; }
        public string Straat { get; set; }
        public double Breedtegraad { get; set; }
        public double Lengtegraad { get; set; }
        public bool Brand { get; set; }
        public double WanneerVol { get; set; }

        [JsonIgnore]
        public ICollection<VuilbakLogging> VuilbakLoggings { get; set; }
    }
}
