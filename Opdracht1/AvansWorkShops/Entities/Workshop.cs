using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansWorkShops.Entities
{
    public class Workshop
    {
        // Voeg de juiste eigenschappen toe aan de properties. Name is verplicht, uniek en niet langer dan 50 tekens
        // Summary is optioneel en mag niet langer zijn dan 250 tekens

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Summary { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // navigatie properties (Department, Registrations en teachers)
        public Department Department { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Teacher> Teachers { get; set; }


    }
}
