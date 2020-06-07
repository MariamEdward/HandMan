using HandManAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HandManAPI.Controllers
{
    [RoutePrefix("api/handman")]
    public class HandManController : ApiController //use try catch and unnique response
    {

        private AppDbContext dbContext = new AppDbContext();

        ////Usermanager
        //static AppDbContext dbcontext = new AppDbContext();
        //static UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
        //static UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);
         
        [Route("getall")]     
        public IHttpActionResult Getall()
        {
            var handmanlist = new List<HandManModel>();
            foreach (var item in dbContext.HandMen.ToList() )
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == item.ID);
                var loc = dbContext.Locations.FirstOrDefault(l => l.ID == item.LocationId);
                var serv = dbContext.services.FirstOrDefault(s => s.ID == item.ServiceId);
                var newItem = new HandManModel
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Region = item.Region,
                    Latitude = loc.Latitude,
                    Longtide = loc.Longtide,
                    ServiceName = serv.Name,


                };
                handmanlist.Add(newItem);
            }
            return Ok(handmanlist);
        }


        [Route("getall/service/{id}")]
        public IHttpActionResult GetallByService(int id)
        {
            var service = dbContext.services.Find(id);
            if (service == null) 
            {
                return BadRequest("service id is not correct");

            }
            var hmen = dbContext.HandMen.Where(h => h.ServiceId == id).ToList();
            if (hmen == null || hmen.Count==0) 
            {
                return Ok<string>("there is no subscibers for this service");
            }
            ////if(service.SubscribedHandMen.Count==0)
            ////{             
            ////   return Ok<string>("there is no handmen for this service");              
            ////}

            var handmanlist = new List<HandManModel>();
            foreach (var item in hmen)
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == item.ID);
                var loc = dbContext.Locations.FirstOrDefault(l => l.ID == item.LocationId);
                var serv = dbContext.services.FirstOrDefault(s => s.ID == item.ServiceId);
                var newItem = new HandManModel
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Region = item.Region,
                    Latitude = loc.Latitude,
                    Longtide = loc.Longtide,
                    ServiceName = serv.Name,


                };
                handmanlist.Add(newItem);
            }
           
            return Ok(handmanlist);
        }

        [Route("getall/{servId}/{regName}")]
        public IHttpActionResult GetallByServiceAndRegion(int servId,string regName)
        {
            var service = dbContext.services.Find(servId);
            if (service == null)
            {
                return BadRequest("service id is not correct");

            }
            var hmen = dbContext.HandMen.Where(h => h.ServiceId == servId && h.Region==regName).ToList();
            if (hmen == null || hmen.Count == 0)
            {
                return Ok<string>("there is no subscibers for this service");
            }
            ////if(service.SubscribedHandMen.Count==0)
            ////{             
            ////   return Ok<string>("there is no handmen for this service");              
            ////}

            var handmanlist = new List<HandManModel>();
            foreach (var item in hmen)
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == item.ID);
                var loc = dbContext.Locations.FirstOrDefault(l => l.ID == item.LocationId);
                var serv = dbContext.services.FirstOrDefault(s => s.ID == item.ServiceId);
                var newItem = new HandManModel
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Region = item.Region,
                    Latitude = loc.Latitude,
                    Longtide = loc.Longtide,
                    ServiceName = serv.Name,


                };
                handmanlist.Add(newItem);
            }

            return Ok(handmanlist);
        }


        // GET: api/Services/5
        [Route("getone/{id}")]
      ///  [HttpGet]
        [ResponseType(typeof(HandManModel))]
        public IHttpActionResult GetHandMan(string id)
        {
            var handman = dbContext.HandMen.Find(id);
            if (handman == null)
            {
                return NotFound();
            }
            var user = dbContext.Users.Find(id);
            var loc = dbContext.Locations.Find(handman.LocationId);
            var ser = dbContext.services.Find(handman.ServiceId);
  
            var hman = new HandManModel
            {
                Name = user.UserName,
                Email = user.Email,
                Region = handman.Region,
                Latitude = loc.Latitude,
                Longtide = loc.Longtide,
                ServiceName = ser.Name, //not sure if it will work

             };

            //var hman = new HandManModel
            //{
            //    Name = handman.User.UserName,
            //    Email = handman.User.Email,
            //    Region = handman.Region,
            //    Latitude = handman.Location.Latitude,
            //    Longtide = handman.Location.Longtide,
            //    ServiceName = handman.Service.Name, //not sure if it will work


            //};

            return Ok(hman);
        }

        //// PUT: 
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutService(int id, Service service)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != service.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(service).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ServiceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}



        // DELETE: api/Services/5
        
        
        
        [Route("delete/{id}")]
   
        [ResponseType(typeof(HandManModel))]
        public IHttpActionResult DeleteHandMan(string id)
        {
            HandMan hman = dbContext.HandMen.Find(id);
            if (hman == null)
            {
                return NotFound();
            }

            dbContext.HandMen.Remove(hman);
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Ok();
        }


    }
}
