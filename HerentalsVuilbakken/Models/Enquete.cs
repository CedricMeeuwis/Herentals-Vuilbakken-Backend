using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Enquete
    {
        public int EnqueteID { get; set; }
        public string JsonData { get; set; }
        public string Naam { get; set; } 
        public bool Actief { get; set; }

        [JsonIgnore]
        public ICollection<Antwoord> Antwoorden { get; set; }
    }
}