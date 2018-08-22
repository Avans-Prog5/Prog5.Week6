using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Entities;
using AvansWorkShops.Repositories;

namespace AvansWorkShops
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * Onderstaande code dient uitlsuitend als referentie. Er zijn meerdere (betere) oplossingen denkbaar.
             * Dit is code die je later in unittests zal plaatsen.
             */

            // EF Code plaatsen we in een eigen class. Deze noemen we een repository.
            StudentRepository studentRepository = new StudentRepository();
            WorkshopRepository workshopRepository = new WorkshopRepository();
            DepartmentRepository departmentRepository = new DepartmentRepository();

            Console.WriteLine("***** Students *****");
            var students = studentRepository.GetStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Firstname} {student.Lastname} \t- email: {student.Email}");
            }
            // Opdracht 2.1
            Console.WriteLine();
            Console.WriteLine("***** Workshops *****");
            var workshops = workshopRepository.WorkshopInformation();
            foreach (var workshop in workshops)
            {
                Console.WriteLine($"Workshop: {workshop.Name} Aantal registraties: {workshop.Registrations.Count}");
                foreach(var teacher in workshop.Teachers)
                {
                    Console.WriteLine($"\tTeacher: {teacher.Fullname}");
                }
                foreach (var registration in workshop.Registrations)
                {
                    Console.WriteLine($"\tStudent: {registration.Student.Fullname}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("***** Student met Workshop *****");
            // Opdracht 2.2
            // We halen de eerste Workshop op vanuit de lijst die we al in het geheugen hebben.
            var ws = workshops.First();
            Student s = new Student
            {
                Email = "sjaak.student@avans.nl",
                Firstname = "Sjaak",
                Lastname = "Student"
            };
            Registration r = new Registration
            {
                Student = s,
                Workshop = ws
            };
            try
            { 
                studentRepository.AddStudentWithWorkShop(r);
                Console.WriteLine($"Student {r.Student.Fullname} is bewaard en ingeschreven voor {r.Workshop.Name}");

            }
            catch (Exception ex)
            {
                // Beter om je eigen Exception class te maken
                Console.WriteLine(ex.Message);
            }

            // Opdracht 2.3
            Console.WriteLine();
            Console.WriteLine("***** Workshops zonder Registratie *****");
            int numWorkshops = workshopRepository.DeleteWorkshopWithoutRegistration();
            Console.WriteLine($"{numWorkshops} workshops zijn verwijderd omdat er geen registratie is gekoppeld. De overgebleven workshops zijn: ");
            var ovgWorkshops = workshopRepository.GetWorkshops();
            foreach (var workshop in workshops)
            {
                Console.WriteLine($"Workshop: {workshop.Name}");
            }

            // Opdracht 2.4
            Console.WriteLine();
            Console.WriteLine("***** Budgetverhoging *****");
            var departments = departmentRepository.BudgetIncrease(10);
            foreach (var department in departments)
            {
                Console.WriteLine($"Departement: {department.Name} heeft nu een budget van {department.Budget} euro");
            }

            // Opdracht 2.5
            Console.WriteLine();
            Console.WriteLine("***** Teacher Wissel *****");
            foreach (var workshop in workshops)
            {
                Console.WriteLine($"Workshop: {workshop.Name}");
                foreach (var teacher in workshop.Teachers)
                {
                    Console.WriteLine($"\tTeacher: {teacher.Fullname} - ID: {teacher.Id}");
                }
            }
            Console.Write("Geef het ID van de teacher die wil ruilen: ");
            var teacherFrom = Convert.ToInt32(Console.ReadLine());
            Console.Write("Geef het ID van de teacher met wie wordt geruild: ");
            var teacherTo = Convert.ToInt32(Console.ReadLine());
            var geruild = workshopRepository.SwapTeachers(teacherFrom, teacherTo);
            if (geruild)
            {
                Console.WriteLine("De teachers zijn omgewisseld");
            }
            else
            {
                Console.WriteLine("Geen teachers omgewisseld, controleer de ID van de teachers");
            }

            // Opdracht 2.6
            Console.WriteLine();
            Console.WriteLine("***** Studenten aantallen *****");
            Tuple<int, int> t = studentRepository.StudentenMetEnZonderRegistratie();
            Console.WriteLine($"Studenten met registratie: {t.Item1} - Zonder registratie: {t.Item2}" );

            // Einde programma
            Console.WriteLine();
            Console.Write("Press a key to exit");
            Console.ReadLine();
        }
    }
}
