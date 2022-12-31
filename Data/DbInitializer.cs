using Controle_ACE_2.Models;
using System.Linq;

namespace Controle_ACE_2.Data
{
    public class DbInitializer
    {
        public static void Initialize(DbSalarieContext context)
        {
            context.Database.EnsureCreated();

            // Look for any salatie.
            if (context.Salarie.Any())
            {
                return;   // DB has been seeded
            }

            var salaries = new Salarie[]
            {
            new Salarie{ID=1,Nom="Carson",Prenom="Alexander",Fonction="Manager",Salaire=15000.00,DepartementId=2},
            new Salarie{ID=2,Nom="Meredith",Prenom="Alonso",Fonction="RH",Salaire=11000.00,DepartementId=2},
            new Salarie{ID=3,Nom="Arturo",Prenom="Anand",Fonction="Tester",Salaire=12000.00,DepartementId = 1},
            new Salarie{ID=4,Nom="Gytis",Prenom="Barzdukas",Fonction="Scrum Master",Salaire=18000.00,DepartementId = 1},
            new Salarie{ID=5,Nom="Yan",Prenom="Li",Fonction="Developer",Salaire=11000.00,DepartementId = 1},
            new Salarie{ID=6,Nom="Peggy",Prenom="Justice",Fonction="Developer",Salaire=11000.00,DepartementId = 1},
            new Salarie{ID=7,Nom="Laura",Prenom="Norman",Fonction="Developer",Salaire=11000.00,DepartementId = 1},

            };
            foreach (Salarie s in salaries)
            {
                context.Salarie.Add(s);
            }

            var departments = new Departement[]
            {
            new Departement{DepartementId=1,Description="IT",Location="Casablanca, Sidi Maarouf",Salaries=salaries[2..7]},
            new Departement{DepartementId=2,Description="RH",Location="Casablanca, Sidi Maarouf",Salaries=salaries[0..2]}
            };
            foreach (Departement d in departments)
            {
                context.Departement.Add(d);
            }
            context.SaveChanges();       
        }
    }
}
