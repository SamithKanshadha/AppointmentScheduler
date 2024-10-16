using AppointmentScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.DataAccess.Repository.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    { 
        
            //Update the appointment object
            void Update(Appointment appointment);

            IEnumerable<Appointment> GetAllByUserId(string userId);



    }
}
