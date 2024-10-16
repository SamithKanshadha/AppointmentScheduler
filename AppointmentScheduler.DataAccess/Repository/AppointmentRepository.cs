using AppointmentScheduler.DataAccess.Data;
using AppointmentScheduler.DataAccess.Repository.IRepository;
using AppointmentScheduler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.DataAccess.Repository
{
    public class AppointmentRepository : Repository<Appointment>,IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Appointment appointment)
        {
            var objFromDb = _db.Appointments.FirstOrDefault(s => s.Id == appointment.Id);

            if (objFromDb != null)
            {
                //the retrieved(objFromDb.Name) Name will be updated from the given one(category.Name)
                objFromDb.AppointmentTime = appointment.AppointmentTime;
                objFromDb.AppointmentTitle = appointment.AppointmentTitle;
                objFromDb.Description = appointment.Description;
                _db.SaveChanges();
            }

        }

        public IEnumerable<Appointment> GetAllByUserId(string userId)
        {
            return _db.Appointments.Where(a => a.ApplicationUserId == userId).ToList();
        }
    }

}
