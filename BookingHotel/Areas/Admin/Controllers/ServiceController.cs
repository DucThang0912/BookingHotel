using BookingHotel.Models;
using BookingHotel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace BookingHotel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();
            return View(services);
        }

        public async Task<IActionResult> Add()
        {
            var services = await _serviceRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceRepository.AddAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }
        public async Task<IActionResult> Update(int id)
        {
            var roomType = await _serviceRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _serviceRepository.UpdateAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
