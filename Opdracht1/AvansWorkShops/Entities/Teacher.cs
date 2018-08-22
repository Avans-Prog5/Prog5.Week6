using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansWorkShops.Entities
{
    public class Teacher : Person
    {
        // Many to Many
        public ICollection<Workshop> Workshops { get; set; }
        
    }
}
