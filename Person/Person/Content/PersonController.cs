﻿using System;

        // GET: Person
        public ActionResult Index()



        [HttpGet]
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());
            //if (person[0].Image != null)
            //{
            //    return File(person[0].Image, "image/jpg");
            //}
            //else
            //{
            //    return null;
            //}
            //return View(person[0]);

            if (id != null)


            //return View(id != null ? person[0] : "") ; 
        }
        //create and update person data using same action 
        public ActionResult CreateUpdate(PersonModel person)
            //if (person == null) return View();
            System.Web.HttpPostedFileBase file = Request.Files["ImageData"];
                //HTTP POST
                var postTask = client.PostAsJsonAsync<PersonModel>("api/WebAPI/Post", person);

        public byte[] ConvertToBytes(HttpPostedFileBase image)
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());

            //TempData["imgData"] =  person[0].Image;
            return View(person[0]);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var getdetailTask = client.GetAsync("api/WebAPI/GetDetail/" + id.ToString());


            if (person[0].Image != null)


    }


}