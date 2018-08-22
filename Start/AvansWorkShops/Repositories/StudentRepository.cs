using System;
using System.Collections.Generic;
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
                return new List<Student>(); // todo: return students
            }
        }
    }
}
