using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Dal;
using AvansWorkShops.Entities;

namespace AvansWorkShops.Repositories
{
    /// <summary>
    /// Repository class voor het verwerken van Studenten gegevens
    /// </summary>
    public class StudentRepository 
    {
        public List<Student> GetStudents()
        {
            using (var db = new AvansContext())
            {
                return db.Students.ToList();
            }
        }

        public void AddStudentWithWorkShop(Registration registration)
        {
            using (var db = new AvansContext())
            {
                // valideer data. Heeft de student een achternaam? Bestaat de Workshop? 
                if (!db.Workshops.Any(x=>x.Name == registration.Workshop.Name))
                {
                    throw new Exception($"De Workshop {registration.Workshop.Name} bestaat niet. Kies een bestaande workshop");
                }
                if (string.IsNullOrEmpty(registration.Student.Lastname))
                {
                    throw new Exception("De student dient minimaal een achternaam te hebben");
                }

                if (string.IsNullOrEmpty(registration.Student.Email) || db.Students.Any(x=>x.Email.ToLower() == registration.Student.Email.ToLower()))
                {
                    throw new Exception($"Het emailadres {registration.Student.Email} bestaat al of is leeg.");
                }

                /*  Omdat we een nieuwe AvansContext hebben aangemaakt weet de ChangeTracker niet dat Workshop al aanwezig is in de database.
                 *  Als we niets doen zal EF een Workshop gaan aanmaken en willen opslaan in de database. Dit zorgt voor een error. Welke error verwacht je?
                 *  We dienen daarom de Context te vertellen dat de State van Workshop niet gewijzigd is.
                 *  
                 *  Je kunt ook van een ander principe gebruik maken. Wat zou je ook kunnen doen om dit probleem op te lossen? Probeer dit zelf het uit te zoeken voor dat je het aan de docent vraagt!
                 */
                db.Entry(registration.Workshop).State = EntityState.Unchanged;

                // Nu alle checks en states zijn gezet kan de Registratie worden gekoppeld aan de context. EF zorgt voor het aanmaken van de data in de diverse tabellen
                db.Registrations.Add(registration);
                
                db.SaveChanges();
            }
        }

        public Tuple<int, int> StudentenMetEnZonderRegistratie()
        {
            using (var db = new AvansContext())
            {
                var studentWithReg = db.Students.Where(x => x.Registrations.Any()).Count();
                var studentWithoutReg = db.Students.Where(x => !x.Registrations.Any()).Count();

                return new Tuple<int, int>(studentWithReg, studentWithoutReg);
            }
        }
    }
}
