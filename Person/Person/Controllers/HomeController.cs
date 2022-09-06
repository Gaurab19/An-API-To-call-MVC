using Person.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Person.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethod(PersonModel person)
        {
            string FullName =person.FullName ;
            string Gender = person.Gender;
            string Province = person.Province;
            DateTime DateOfBirth = person.DateOfBirth;
            string Email=person.Email;
            string Password=person.Password;
            return Json(person);
        }
    }
}