using HerentalsVuilbakken.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Models
{
    public class DBInitializer
    {
        public static void Initialize(VuilbakContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Roles.AddRange(
              new Role { Naam = "Inwoner" },
              new Role { Naam = "Bezoeker" },
              new Role { Naam = "Ophaler" },
              new Role { Naam = "Admin" }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User { Username = "Joske", Adres="Herentals Centrum 21", RoleID = 1, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot"},
                new User { Username = "Andre", Adres = "", RoleID = 2, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot" },
                new User { Username = "Bert", Adres = "Koppelandstraat 12", RoleID = 3, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot" },
                new User { Username = "JoSpiesens", Adres = "Kloosterstraat 5", RoleID = 4, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot" }

            );
            context.SaveChanges();

            context.Vuilbakken.AddRange(
                new Vuilbak { Type = VuilbakType.GFT, UserID = 4, Volheid = 12.5},
                new Vuilbak { Type = VuilbakType.Groenafval, UserID = 4, Volheid = 86.5 },
                new Vuilbak { Type = VuilbakType.PapierEnKarton, UserID = 4, Volheid = 0.0 }
            );
            context.SaveChanges();
        }
    }
}
