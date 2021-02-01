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
              new Role { Naam = "Ophaler" },
              new Role { Naam = "Admin" },
              new Role { Naam = "Groendienst" }
            );
            context.SaveChanges();

            context.Zones.AddRange(
              new Zone { Naam = "Kerk" }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User { Username = "Bert", RoleID = 1, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot"},
                new User { Username = "Andre",  RoleID = 1, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot" },
                new User { Username = "JoSpiesens", RoleID = 2, Wachtwoord = "o6w8D+OE1/nhuMhyD1iKz4JkqmDFH3Sk8s5kV1FnNPRah5ot" }

            );
            context.SaveChanges();

            context.Vuilbakken.AddRange(
                new Vuilbak { Straat = "Olympiadelaan", Breedtegraad = 51.18381, Lengtegraad = 4.83032, Gewicht = 23, Volheid = 12.5, Brand = false, WanneerVol = 100},
                new Vuilbak { Straat = "Begijnenstraat", Breedtegraad = 51.18017, Lengtegraad = 4.83548, Gewicht = 40.3, Volheid = 86.5, Brand = false, WanneerVol = 98.9 },
                new Vuilbak { Straat = "De Zaatweg", Breedtegraad = 51.18203, Lengtegraad = 4.82885, Gewicht = 10.8, Volheid = 0.0 , Brand = false, WanneerVol = 103}
            );
            context.SaveChanges();

            context.VuilbakLoggings.AddRange(
                new VuilbakLogging { VuilbakID = 2, Datum = DateTime.Now.AddDays(-3), Gewicht = 10.3, Volheid = 35.6 },
                new VuilbakLogging { VuilbakID = 2, Datum = DateTime.Now.AddDays(-2), Gewicht = 19.5, Volheid = 50.8 },
                new VuilbakLogging { VuilbakID = 2, Datum = DateTime.Now.AddDays(-1), Gewicht = 34.3, Volheid = 60.0 },
                new VuilbakLogging { VuilbakID = 1, Datum = DateTime.Now.AddDays(-1), Gewicht = 14.1, Volheid = 8.0 }
            );
            context.SaveChanges();
        }
    }
}
