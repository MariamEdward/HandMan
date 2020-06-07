using HandManAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace HandManAPI.Services
{
    public class HandManService
    {

        static AppDbContext dbContext = new AppDbContext();
        public HandManModel convertToModel(HandMan _hman)
        {
            IdentityUser user = dbContext.Users.FirstOrDefault(u => u.Id == _hman.ID);
            Location loc = dbContext.Locations.FirstOrDefault(l => l.ID == _hman.LocationId);
            Service serv = dbContext.services.FirstOrDefault(s => s.ID == _hman.ServiceId);
            var transactions = dbContext.Transactions.Where(t => t.CustomerId == _hman.ID).ToList();
            var reviews = new List<string>();
            foreach (var item in transactions)
            {
                reviews.Add(item.Review);
            }
            HandManModel newhman = new HandManModel
            {
                ID = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Region = _hman.Region,
                Latitude = loc.Latitude,
                Longtide = loc.Longtide,
                ServiceName = serv.Name,
                Reviews = reviews,
                Rate=_hman.Rate
            };
            return newhman;
        }


    }
}



