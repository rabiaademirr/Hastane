using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            ViewData["Title"] = "Admin Dashboard";
            return View();
        }

        public IActionResult Patients()
        {
            ViewData["Title"] = "Patient Management";
            return View();
        }

        public IActionResult Doctors()
        {
            ViewData["Title"] = "Doctor Management";
            return View();
        }

        public IActionResult Appointments()
        {
            ViewData["Title"] = "Appointment Management";
            return View();
        }

        public IActionResult Clinics()
        {
            ViewData["Title"] = "Clinic Management";
            return View();
        }
    }
}