using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Wachtwoord { get; set; }
        [NotMapped]
        public string Token { get; set; }

        //Relations
        public int? RoleID { get; set; }
        public Role Role { get; set; }

    }
}
