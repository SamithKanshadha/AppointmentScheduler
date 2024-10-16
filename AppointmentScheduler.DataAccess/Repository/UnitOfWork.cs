using AppointmentScheduler.DataAccess.Data;
using AppointmentScheduler.DataAccess.Repository.IRepository;
using AppointmentScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Appointment = new AppointmentRepository(_db);
        }
        public IAppointmentRepository Appointment { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
