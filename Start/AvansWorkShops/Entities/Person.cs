using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansWorkShops.Entities
{
    public abstract class Person
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }


        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {
                return $"{Firstname} {Lastname}";
            }
        }
    }
}
