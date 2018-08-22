using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Helpers;

namespace AvansWorkShops.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public int WorkshopId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }

        public Student Student { get; set; }
        public Workshop Workshop { get; set; }
    }
}
