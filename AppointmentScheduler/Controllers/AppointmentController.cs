using AppointmentScheduler.DataAccess.Repository.IRepository;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _userManager.GetUserAsync(User);
            var appointments = _unitOfWork.Appointment.GetAllByUserId(user.Id);
            return View(appointments);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            Appointment appointment = new Appointment();
            if (id == null)
            {
                // Create
                appointment.ApplicationUserId = user.Id;
                return View(appointment);
            }

            // Update
            appointment = _unitOfWork.Appointment.GetFirstOrDefault(a => a.Id == id && a.ApplicationUserId == user.Id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Appointment appointment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if (appointment.Id == 0)
                {
                    appointment.ApplicationUserId = user.Id;
                    _unitOfWork.Appointment.Add(appointment);
                }
                else
                {
                    var appointmentFromDb = _unitOfWork.Appointment.GetFirstOrDefault(a => a.Id == appointment.Id && a.ApplicationUserId == user.Id);
                    if (appointmentFromDb == null)
                    {
                        return NotFound();
                    }

                    appointmentFromDb.AppointmentTime = appointment.AppointmentTime;
                    appointmentFromDb.AppointmentTitle = appointment.AppointmentTitle;
                    appointmentFromDb.Description = appointment.Description;
                    _unitOfWork.Appointment.Update(appointmentFromDb);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        #region API CALLS

        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var allobj = _unitOfWork.Appointment.GetAllByUserId(user.Id);
            return Json(new { data = allobj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var objFromDb = _unitOfWork.Appointment.GetFirstOrDefault(a => a.Id == id && a.ApplicationUserId == user.Id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Appointment.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
