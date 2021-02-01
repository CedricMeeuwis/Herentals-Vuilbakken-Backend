using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public string Naam { get; set; }

        [JsonIgnore]
        public ICollection<Vuilbak> Vuilbakken { get; set; }
    }
}
