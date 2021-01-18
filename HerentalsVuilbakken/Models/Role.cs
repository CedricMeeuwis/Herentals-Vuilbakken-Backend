using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Naam { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}
