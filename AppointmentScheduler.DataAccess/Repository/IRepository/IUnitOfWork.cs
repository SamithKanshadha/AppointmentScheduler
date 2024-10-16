using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointment {  get ; }
        void Save();
    }
}
