using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace WebApi.Controllers
{

    public class WebAPIcontroller : ApiController
    {
        PersonDBEntities db = new PersonDBEntities();
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        [Route("api/WebAPI/PostNewPerson")]
        [HttpPost]
        public IHttpActionResult PostNewPerson(PersonModel person)
        {
            PersonModel obj = new PersonModel();
            if (ModelState.IsValid)
            {

                obj.Id = person.Id;
                obj.FullName = person.FullName;
                obj.Gender = person.Gender;
                obj.Province = person.Province;
                obj.DateOfBirth = person.DateOfBirth;
                obj.Email = person.Email;
                obj.Password = person.Password;

            }
            db.PersonModels.Add(obj);
            db.SaveChanges();
            return Ok();
        }


        [Route("api/WebAPI/GetPerson")]

        [HttpGet]
        public IHttpActionResult GetPerson(string searchtext)
        {
            var datalist = db.GetAllPersonData(searchtext);
            return Ok(datalist);
        }
        //[Route("api/WebAPI/Post")]
        //[HttpPost]
        ////for create only
        //public IHttpActionResult Post(PersonModel person)
        //{
        //    PersonDBEntities db = new PersonDBEntities();
        //    db.CreateData(person.FullName, person.Gender, person.Province, person.DateOfBirth, person.Email, person.Password, person.Title, person.Image);
        //    //db.SaveChanges();
        //    return Ok();
        //}

        [Route("api/WebAPI/Post")]
        [HttpPost]
        //for both create and update person data.
        public IHttpActionResult Post(PersonModel person)
        {
            using (PersonDBEntities entities = new PersonDBEntities())
            {
                if (person.Id != 0)
                {
                    db.UpdatePersonData(person.Id, person.FullName, person.Gender, person.Province, person.DateOfBirth, person.Email, person.Password, person.Title, person.Image);
                }
                else
                {
                    db.CreateData(person.FullName, person.Gender, person.Province, person.DateOfBirth, person.Email, person.Password, person.Title, person.Image);
                }
            }
            return Ok(person);
        }
        [Route("api/WebAPI/Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            db.DeletePersonData(id);
            return Ok();
        }
        [Route("api/WebAPI/GetDetail/{id}")]
        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            // var data = db.GetPersonById(id);
            var data = db.Database.SqlQuery<PersonModel>("GetPersonById @id", new SqlParameter("id", id)).ToList();
            return Ok(data);
        }
    }
}