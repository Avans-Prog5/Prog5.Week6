using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansWorkShops.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentCode { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        // Optionele Foreign key 
        public int? TeacherId { get; set; }
        public Teacher Administrator { get; set; }

        // Koppeling met Workshops
        public ICollection<Workshop> Workshops { get; set; }
    }
}
