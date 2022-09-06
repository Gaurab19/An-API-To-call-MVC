using System;using System.Collections.Generic;using System.Linq;using System.Web;using Person.Models;using System.Web.Mvc;using System.Net.Http;using System.Net.Http.Headers;using Newtonsoft.Json;using System.Threading.Tasks;using System.IO;//using System.Web.UI.DataVisualization.Charting;using System.Drawing;namespace Person.Content{    public class PersonController : Controller    {

        // GET: Person
        public ActionResult Index()        {            List<PersonModel> PersonInfo = new List<PersonModel>();            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                var Res = client.GetAsync("api/WebAPI/GetPerson?searchText=");                Res.Wait();                var result = Res.Result;                if (result.IsSuccessStatusCode)                {                    var response = result.Content.ReadAsStringAsync().Result;                    PersonInfo = JsonConvert.DeserializeObject<List<PersonModel>>(response);                }                return View(PersonInfo);            }        }        public ActionResult GetFilterData(string searchText)        {            List<PersonModel> PersonInfo = new List<PersonModel>();            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                var Res = client.GetAsync("api/WebAPI/GetPerson?searchText=" + searchText);                Res.Wait();                var result = Res.Result;                if (result.IsSuccessStatusCode)                {                    var response = result.Content.ReadAsStringAsync().Result;                    PersonInfo = JsonConvert.DeserializeObject<List<PersonModel>>(response);                }                return View("Index", PersonInfo);            }        }



        [HttpGet]        public ActionResult CreateUpdate(int? id)        {            List<PersonModel> person = new List<PersonModel>();            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());                getdetailTask.Wait();                var result = getdetailTask.Result;                if (result.IsSuccessStatusCode)                {                    var response = result.Content.ReadAsStringAsync().Result;                    person = JsonConvert.DeserializeObject<List<PersonModel>>(response);                }            }
            //if (person[0].Image != null)
            //{
            //    return File(person[0].Image, "image/jpg");
            //}
            //else
            //{
            //    return null;
            //}
            //return View(person[0]);

            if (id != null)            {                return View(person[0]);            }            else            {                return View();            }


            //return View(id != null ? person[0] : "") ; 
        }        [HttpPost]
        //create and update person data using same action 
        public ActionResult CreateUpdate(PersonModel person)        {
            //if (person == null) return View();
            System.Web.HttpPostedFileBase file = Request.Files["ImageData"];            person.Image = ConvertToBytes(file);            person.Title = file.FileName;            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP POST
                var postTask = client.PostAsJsonAsync<PersonModel>("api/WebAPI/Post", person);                postTask.Wait();                var result = postTask.Result;                var data = result.Content.ReadAsStringAsync();                if (result.IsSuccessStatusCode)                {                    return RedirectToAction("Index");                }            }            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");            return View(person);        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)        {            byte[] imageBytes = null;            BinaryReader reader = new BinaryReader(image.InputStream);            imageBytes = reader.ReadBytes((int)image.ContentLength);            return imageBytes;        }        public ActionResult Delete(int id)        {            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                var deleteTask = client.DeleteAsync("api/WebAPI/Delete/" + id.ToString());                deleteTask.Wait();                var result = deleteTask.Result;                if (result.IsSuccessStatusCode)                {                    return RedirectToAction("Index");                }            }            return View();        }        [HttpGet]        public ActionResult Details(int id)        {            List<PersonModel> person = new List<PersonModel>();            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());                getdetailTask.Wait();                var result = getdetailTask.Result;                if (result.IsSuccessStatusCode)                {                    var response = result.Content.ReadAsStringAsync().Result;                    person = JsonConvert.DeserializeObject<List<PersonModel>>(response);                }            }

            //TempData["imgData"] =  person[0].Image;
            return View(person[0]);        }        public ActionResult RetrieveImage(int id)        {            List<PersonModel> person = new List<PersonModel>();            using (var client = new HttpClient())            {                client.BaseAddress = new Uri("https://localhost:44358/");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());                getdetailTask.Wait();                var result = getdetailTask.Result;                if (result.IsSuccessStatusCode)                {                    var response = result.Content.ReadAsStringAsync().Result;                    person = JsonConvert.DeserializeObject<List<PersonModel>>(response);                }            }


            if (person[0].Image != null)            {                var extArray = person[0].Title.Split('.');                var extention = extArray[1];                if (extention == "jpg")                {                    return File(person[0].Image, "image/jpg", person[0].Title);                }                else if (extention == "jpeg")                {                    return File(person[0].Image, "image/jpeg", person[0].Title);                }                else if (extention == "png")                {                    return File(person[0].Image, "image/png", person[0].Title);                }                else if (extention == "pdf")                {                    return File(person[0].Image, "application/pdf", person[0].Title);                }                else                {                    return null;                }            }            else            {                return null;            }        }


    }


}