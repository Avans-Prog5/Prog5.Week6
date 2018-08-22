using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Repositories;

namespace AvansWorkShops
{
    class Program
    {
        static void Main(string[] args)
        {
            // EF Code plaatsen we in een eigen class. Deze noemen we een repository.
            StudentRepository studentRepository = new StudentRepository();

            Console.WriteLine("***** Students *****");
            var students = studentRepository.GetStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Firstname} {student.Lastname} \t- email: {student.Email}");
            }
            Console.WriteLine();
            Console.Write("Press a key to exit");
            Console.ReadLine();
        }
    }
}
