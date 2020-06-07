using HandManAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HandManAPI.Controllers
{
    [RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {

        //Usermanager
        //static AppDbContext dbcontext = new AppDbContext();
        //static UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
        //static UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);
     //   var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));

        [Route("new/admin")]
        public async Task<IHttpActionResult> postUserAdmin(UserModel userModel)
        {
             AppDbContext dbcontext = new AppDbContext();
             UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
             UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);

            //check modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }




            // create admin rule and admin user
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));

            if (!RoleManager.RoleExists("admin"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("admin"));
            }

            //Map from UserModel to IdentityUser
            IdentityUser user = new IdentityUser();
            user.UserName = userModel.Name;
            user.Email = userModel.Email;

            //create 
            IdentityResult result = await manager.CreateAsync(user, userModel.Password);


            //ok
            if (result.Succeeded) 
            {
                manager.AddToRole(user.Id, "admin");
                return Created("", "User Added");
            }

            //badrequ
            return BadRequest(result.Errors.ToList()[0]);

        }

        [Route("new/handman")]
        public async Task<IHttpActionResult> postUserHandMan(HandManModel userModel)
        {

            AppDbContext dbcontext = new AppDbContext();
            UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);

            // create admin rule and admin user
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            if (!RoleManager.RoleExists("handman"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("handman"));
            }

            //check modelstate           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Map from UserModel to IdentityUser
                IdentityUser user = new IdentityUser();
                user.UserName = userModel.Name;
                user.Email = userModel.Email;
                //create 
                IdentityResult result = await manager.CreateAsync(user, userModel.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList()[0]);
                }
                else
                {
                    manager.AddToRole(user.Id, "handman");
                    var loc = new Location { Latitude = userModel.Latitude, Longtide = userModel.Longtide };

                    var newHandMan = new HandMan
                    {
                        User = user,
                        Region = userModel.Region,
                        Location = loc,
                        Service = dbcontext.services.FirstOrDefault(s => s.ID == userModel.ServiceId),
                        ServiceId = userModel.ServiceId,
                        LocationId = loc.ID,
                        Available = true

                    };
                    dbcontext.HandMen.Add(newHandMan);
                    dbcontext.SaveChanges();
                    newHandMan.Service.SubscribedHandMen.Add(newHandMan);
                   
                    //ok                   
                    return Created("", "Added handman");
                }
            }
            catch(Exception e) 
            {
               // throw e;
                return BadRequest("sorry something bad happened");
            }



            //var newHandMan = new HandMan
            //{
            //    User = user,
            //    Region = userModel.Region,
            //    Location = loc,
            //    ServiceId = userModel.  ServiceId,

            //};

        }

        [Route("new/customer")]
        public async Task<IHttpActionResult> postUserCustomer(UserModel userModel)
        {
            AppDbContext dbcontext = new AppDbContext();
            UserStore<IdentityUser> userstore = new UserStore<IdentityUser>(dbcontext);
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userstore);
            //check modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create admin rule and admin user
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            if (!RoleManager.RoleExists("customer"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("customer"));
            }

            //Map from UserModel to IdentityUser
            IdentityUser user = new IdentityUser();
            user.UserName = userModel.Name;
            user.Email = userModel.Email;

            //create 
            IdentityResult result = await manager.CreateAsync(user, userModel.Password);

            //add to customer table 
            var newCustomer = new Customer();
            newCustomer.User = user;
            dbcontext.Customers.Add(newCustomer);

            //ok
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, "customer");
                return Created("", "Added customer");
            }

            //badrequ
            return BadRequest(result.Errors.ToList()[0]);

        }


    }

}


//create admin rule and admin user

//var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
//            if (!RoleManager.RoleExists("admin"))
//            {
//                var roleresult = RoleManager.Create(new IdentityRole("admin"));
//            }
//            //Create User=Admin with password=123456
//            var user = new ApplicationUser();
//user.UserName = name;
//            var adminresult = UserManager.Create(user, password);

//            //Add User Admin to Role Admin
//            if (adminresult.Succeeded)
//            {
//                var result = UserManager.AddToRole(user.Id, name);
//            }