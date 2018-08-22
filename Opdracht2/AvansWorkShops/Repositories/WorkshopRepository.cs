using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Dal;
using AvansWorkShops.Entities;

namespace AvansWorkShops.Repositories
{
    public class WorkshopRepository
    {
        public List<Workshop> WorkshopInformation()
        {
            using (var db = new AvansContext())
            {
                // Probeer eens te achterhalen welke query EF daadwerkelijk naar SQL Server stuurt?
                return db.Workshops
                    .Include(r => r.Registrations.Select(s => s.Student))
                    .Include(t => t.Teachers)
                    .ToList();
            }
        }

        public List<Workshop> GetWorkshops()
        {
            using (var db = new AvansContext())
            {
                return db.Workshops.ToList();
            }
        }

        public int DeleteWorkshopWithoutRegistration()
        {
            using (var db = new AvansContext())
            {
                var wsNoReg = from ws in db.Workshops
                              where !ws.Registrations.Any()
                              select ws;
                var numWorkshops = wsNoReg.Count();

                db.Workshops.RemoveRange(wsNoReg);
                db.SaveChanges();
                return numWorkshops;
            }
        }

        public bool SwapTeachers(int teacherFrom, int teacherTo)
        {
            using (var db = new AvansContext())
            {
                // teachers moeten wel bestaan natuurlijk
                var fromTeacher = db.Teachers.Include(x=>x.Workshops).SingleOrDefault(x => x.Id == teacherFrom);
                var toTeacher = db.Teachers.Include(x => x.Workshops).SingleOrDefault(x => x.Id == teacherTo);
                if (fromTeacher == null || toTeacher == null)
                {
                    return false;
                }
                // Kan dit eenvoudiger?
                var workshopsFrom = fromTeacher.Workshops;
                var workshopsTo = toTeacher.Workshops;

                foreach (var wsf in workshopsFrom)
                {
                    wsf.Teachers.Add(toTeacher);
                    wsf.Teachers.Remove(fromTeacher);
                }
                foreach (var wst in workshopsTo)
                {
                    wst.Teachers.Remove(toTeacher);
                    wst.Teachers.Add(fromTeacher);
                }
                return db.SaveChanges() > 0;
            }
        }
    }
}
