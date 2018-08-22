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
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Voornaam dient minmaal 2 letters en maximaal 25 letters te bevatten")]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(250)]
        public string Lastname { get; set; }

        [EmailAddress]
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
