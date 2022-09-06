using Person.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Person.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonModel person)
        {
            string FullName = person.FullName;
            string Gender = person.Gender;
            string Province = person.Province;
            DateTime DateOfBirth = person.DateOfBirth;
            string Email = person.Email;
            string Password = person.Password;
            return Json(person);
        }
           
    }
}