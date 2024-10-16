using AppointmentScheduler.DataAccess.Repository.IRepository;
using AppointmentScheduler.Models;
using AppointmentScheduler.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AppointmentScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
          
            var closestAppointments = _unitOfWork.Appointment
                .GetClosestUpcomingAppointments(a => a.AppointmentTime, 5)
                .Select(a => new AppointmentVM
                {
                    Id = a.Id,
                    AppointmentTitle = a.AppointmentTitle,
                    AppointmentTime = a.AppointmentTime,
                    Description = a.Description
                })
                .ToList();

            return View(closestAppointments);
        }

        public IActionResult Details(int id)
        {
            var appointmentFromDb = _unitOfWork.Appointment.GetFirstOrDefault(a => a.Id == id);

            if (appointmentFromDb == null)
            {
                return NotFound();
            }

            return View(appointmentFromDb);
        }
    }
}
