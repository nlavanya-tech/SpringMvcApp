using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpringMvcApp.BusinessLayer.Interfaces;
using SpringMvcApp.Entities;

namespace SpringMvcApp_EFCore.Controllers
{
    public class AdminController : Controller
    {
        public readonly IAdminServices _service;

        public AdminController(IAdminServices service)
        {
            _service = service;
        }

        public async Task<ActionResult> AllUsersForAdmin()
        {
            var user = await _service.GetAllUserAsync();
            return View(user);
        }

        public ActionResult GetUser()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUser(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var user = _service.GetUserById(id);
                return RedirectToAction(nameof(GetUser));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteUser()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var user = _service.DeleteUserAsync(id);
                return RedirectToAction(nameof(GetUser));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdateUser()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(User user, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var users = _service.UpdateUserAsync(user);
                return RedirectToAction(nameof(GetUser));
            }
            catch
            {
                return View();
            }
        }

    }
}