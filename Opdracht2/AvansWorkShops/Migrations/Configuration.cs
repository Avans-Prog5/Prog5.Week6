namespace AvansWorkShops.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AvansWorkShops.Dal;
    using AvansWorkShops.Entities;
    using AvansWorkShops.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<AvansWorkShops.Dal.AvansContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        
        protected override void Seed(AvansContext context)
        {
            var students = new List<Student>
            {
                new Student{Firstname = "Jan", Lastname = "Jansen", Email = "jan@jansen.nl" },
                new Student{Firstname = "Kees", Lastname = "Kist", Email = "kees@kist.nl" },
                new Student{Firstname = "Piet", Lastname = "Bakker", Email = "piet.den.bakker@ergens.nl" },
                new Student{Firstname = "S1", Lastname = "Student1", Email = "student1@avans.nl" },
                new Student{Firstname = "S2", Lastname = "Student2", Email = "student2@avans.nl" },
                new Student{Firstname = "S3", Lastname = "Student3", Email = "student3@avans.nl" },
                new Student{Firstname = "S4", Lastname = "Student4", Email = "student4@avans.nl" },
                new Student{Firstname = "S5", Lastname = "Student5", Email = "student5@avans.nl" },
                new Student{Firstname = "S6", Lastname = "Student6", Email = "student6@avans.nl" },
                new Student{Firstname = "S7", Lastname = "Student7", Email = "student7@avans.nl" },

            };
            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {
                new Teacher{Firstname = "Ger", Lastname = "Saris", Email = "ger@avansschool.nl" },
                new Teacher{Firstname = "Stijn", Lastname = "Smulders", Email = "stijn@avansschool.nl" },
                new Teacher{Firstname = "Ronald", Lastname = "Schellekens", Email = "ronald@avansschool.nl" },
            };
            teachers.ForEach(t => context.Teachers.AddOrUpdate(t));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department{ DepartmentCode = "JAV", Name = "Java", Budget = 100000, TeacherId = teachers.Single(t=>t.Lastname=="Saris").Id},
                new Department{ DepartmentCode = "NET", Name = ".Net", Budget = 325000, TeacherId = teachers.Single(t=>t.Lastname=="Schellekens").Id},
                new Department{ DepartmentCode = "WEB", Name = "Web", Budget = 5000, TeacherId = teachers.Single(t=>t.Lastname=="Smulders").Id},
                new Department{ DepartmentCode = "INT", Name = "Introductie", Budget = 92000, TeacherId = teachers.Single(t=>t.Lastname=="Smulders").Id},
                new Department{ DepartmentCode = "ALG", Name = "Algemeen", Budget = 0, TeacherId = teachers.Single(t=>t.Lastname=="Schellekens").Id},
            };
            departments.ForEach(d => context.Departments.AddOrUpdate(d));
            context.SaveChanges();

            var workshops = new List<Workshop>
            {
                new Workshop {
                    Name = "PROG5",
                    Summary ="Leren programmeren in C#",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "NET").Id,
                    Teachers = new List<Teacher>()
                },
                new Workshop {
                    Name = "PROG4",
                    Summary ="Leren programmeren in Java",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "JAV").Id,
                    Teachers = new List<Teacher>()
                },
                new Workshop {
                    Name = "WEBS1",
                    Summary ="Leren programmeren in HTML & CSS",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "WEB").Id,
                    Teachers = new List<Teacher>()
                },
                new Workshop {
                    Name = "Workshop1",
                    Summary ="Workshop 1",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "ALG").Id,
                    Teachers = new List<Teacher>()
                },
                new Workshop {
                    Name = "Workshop2",
                    Summary ="Workshop 2",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "ALG").Id,
                    Teachers = new List<Teacher>()
                },
                new Workshop {
                    Name = "Workshop3",
                    Summary ="Workshop 3",
                    DepartmentId = departments.Single( s => s.DepartmentCode == "ALG").Id,
                    Teachers = new List<Teacher>()
                },
            };
            workshops.ForEach(w => context.Workshops.AddOrUpdate(w));
            context.SaveChanges();

            // Registration toevoegen
            var registrations = new List<Registration>
            {
                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Jan").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "PROG5").Id,
                    Grade =Grade.Goed},
                new Registration {
                    StudentId =students.Single(s => s.Firstname == "Jan").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "PROG4").Id,
                    Grade =Grade.Voldaan},
                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Jan").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "WEBS1").Id,
                    Grade =Grade.NietVoldaan},
                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Kees").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "PROG5").Id,
                    Grade =Grade.NietVoldaan},
                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Kees").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "PROG4").Id,
                    Grade =Grade.NietVoldaan},

                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Piet").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "PROG5").Id
                },

                new Registration{
                    StudentId =students.Single(s => s.Firstname == "Piet").Id,
                    WorkshopId =workshops.Single(w=>w.Name == "WEBS1").Id
                },

            };

            AddOrUpdateTeacher(context, "PROG5", "Schellekens");
            AddOrUpdateTeacher(context, "WEBS1", "Smulders");
            AddOrUpdateTeacher(context, "PROG4", "Saris");

            foreach (Registration r in registrations)
            {
                var bewaren = context.Registrations.Where(
                    s => s.Student.Id == r.StudentId &&
                         s.Workshop.Id == r.WorkshopId).SingleOrDefault();
                if (bewaren == null)
                {
                    context.Registrations.Add(r);
                }
            }
            context.SaveChanges();

        }

        private void AddOrUpdateTeacher(AvansContext context, string workshopName, string teacherName)
        {
            var workshop = context.Workshops.SingleOrDefault(c => c.Name == workshopName);
            var assignedTeacher = workshop.Teachers.SingleOrDefault(i => i.Lastname == teacherName);
            if (assignedTeacher == null)
            {
                workshop.Teachers.Add(context.Teachers.Single(i => i.Lastname == teacherName));
            }
        }
    }
}
