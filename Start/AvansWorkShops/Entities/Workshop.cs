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

        public string Name { get; set; }

        public string Summary { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // todo: navigatie properties (Department, Registrations en teachers)

    }
}
