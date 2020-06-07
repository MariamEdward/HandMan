using HandManAPI.Models;
using HandManAPI.Services;
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
        private HandManService hmService = new HandManService();

        ////Usermanager
        //static AppDbContext dbcontext = new AppDbContext();
        //static UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
        //static UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);
         
        [Route("getall")]     
        public IHttpActionResult Getall()
        {
            var handmanlist = new List<HandManModel>();
            foreach (var item in dbContext.HandMen.ToList())
            {
                var newHm = hmService.convertToModel(item);
                handmanlist.Add(newHm);
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

            var handmanlist = new List<HandManModel>();
            foreach (var item in hmen)
            {
                var newItem = hmService.convertToModel(item);
                handmanlist.Add(newItem);
            }
           
            return Ok(handmanlist);
        }

       
        //get all by service and region
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

            var handmanlist = new List<HandManModel>();
            foreach (var item in hmen)
            {
                var newItem = hmService.convertToModel(item);
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
            var hman = hmService.convertToModel(handman);

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

            return Ok("Deleted");
        }


    }
}
