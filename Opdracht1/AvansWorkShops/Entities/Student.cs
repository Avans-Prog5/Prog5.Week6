using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansWorkShops.Entities
{
    public class Student : Person
    {
        // Koppeling met registration
        public ICollection<Registration> Registrations { get; set; }
    }
}
