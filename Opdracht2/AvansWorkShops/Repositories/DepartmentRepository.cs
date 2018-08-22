using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansWorkShops.Dal;
using AvansWorkShops.Entities;

namespace AvansWorkShops.Repositories
{
    public class DepartmentRepository
    {
        public List<Department> BudgetIncrease(int increaseBy)
        {
            using (var db = new AvansContext())
            {
                int percentage = 100 + increaseBy;
                // Haal de workshops op met meer dan 2 registraties
                var workshopDepartments = db.Workshops.Where(w => w.Registrations.Count > 2).Select(x=>x.DepartmentId);
                // We hebben een lijstje met DeparmentId's. Dit lijstje kun je gebruiken om te bepalen of een ID van Department er in voorkomt.
                var departments = db.Departments.Where(x => workshopDepartments.Contains(x.Id));

                foreach (var dp in departments)
                {
                    dp.Budget = dp.Budget * percentage / 100;
                }
                db.SaveChanges();
                return departments.ToList();
            }
        }
    }
}
