using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Antwoord
    {
        public int AntwoordID { get; set; }
        public string JsonData { get; set; }

        //Relation
        public int EnqueteID { get; set; }
        public Enquete Enquete { get; set; }
    }
}