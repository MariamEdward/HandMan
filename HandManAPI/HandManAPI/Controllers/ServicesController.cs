using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HandManAPI.Models;

namespace HandManAPI.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        [Route("addlist")]
        public IHttpActionResult PostList()
        {
            var ser1 = new Service { Name = "Carpenter", Image="image1"};
            var ser2= new Service { Name = "Tailor", Image = "imag21" };
            var ser3 = new Service { Name = "Driver", Image = "image3"};
            var ser4 = new Service { Name = "Painter", Image = "image4" };
            db.services.Add(ser1);
            db.services.Add(ser2);
            db.services.Add(ser3);
            db.services.Add(ser4);
            db.SaveChanges();
            return Ok();

        }

        // GET: api/Services
        public IQueryable<Service> Getservices()
        {
            return db.services;
        }


        [Route("getById/{id}")]
        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            Service service = db.services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }


        [Route("update/{id}")]
        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.ID)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        [Route("new")]
        // POST: api/Services
        [ResponseType(typeof(Service))]
        public IHttpActionResult PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.services.Add(service);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = service.ID }, service);
        }


        [Route("delete")]
        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = db.services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            db.services.Remove(service);
            db.SaveChanges();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(int id)
        {
            return db.services.Count(e => e.ID == id) > 0;
        }
    }
}