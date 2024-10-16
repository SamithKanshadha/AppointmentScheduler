using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Models.ViewModels
{
    public class AppointmentVM
    {
        public int Id { get; set; }
        public string AppointmentTitle { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Description { get; set; }
    }
}
