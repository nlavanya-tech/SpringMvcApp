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
    public class UserController : Controller
    {
        public readonly IUserServices _service;

        public UserController(IUserServices service)
        {
            _service = service;
        }

        // GET: User
        public async Task<ActionResult> AllUsers()
        {

            var user = await _service.GetAllUserAsync();
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
              await _service.RegisterAsync(user);
                return RedirectToAction(nameof(AllUsers));
            }
            catch
            {
                return View();
            }
        }

    
        public ActionResult Login()


        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var id = _service.Signin(username, password);
                return RedirectToAction(nameof(GetUser));
            }
            catch
            {
                return View();
            }
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
                var user = _service.GetUser(id);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
      
      



    }
}