using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using news_api.Core.Model;
using news_api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace news_api.Extensions
{
    public static class ExtensionMethods
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager,
            NewsDbContext newsDbContext)
        {
            if (userManager.Users.Any()) return;

            var roles = new List<Role>()
            {
                new Role(){Name = Constants.RoleNameAdmin},
                new Role(){Name = Constants.RoleNameNormalUser}
            };

            roles.ForEach(role => roleManager.CreateAsync(role).Wait()); 

            var adminUser = new User(){UserName = "admin"};
            userManager.CreateAsync(adminUser, "Password*123").Wait();
            userManager.AddToRoleAsync(adminUser, Constants.RoleNameAdmin).Wait();
        }

        public static List<KeyValuePair<int, string>> GetEnumStringValues<T>() where T : struct {
            var result = new List<KeyValuePair<int, string>>();              
            foreach(var elem in Enum.GetValues(typeof(T)))
                result.Add(new KeyValuePair<int, string>((int)elem, elem.ToString()));
            return result;
        }

        public static IIncludableQueryable<QueryObjectEverythingRequest, object> EagerLoadRelatedObjects(
            this DbSet<QueryObjectEverythingRequest> dbSetQueryObjectEverythingRequest, bool includeRelated = true){
            if (includeRelated)
               return dbSetQueryObjectEverythingRequest
                        .Include(res => res.CreatedByUser);

            return dbSetQueryObjectEverythingRequest.Include(r => r);             
        }

        public static IIncludableQueryable<QueryObjectTopHeadLinesRequest, object> EagerLoadRelatedObjects(
            this DbSet<QueryObjectTopHeadLinesRequest> dbSetQueryObjectTopHeadLinesRequest, bool includeRelated = true){
            if (includeRelated)
               return dbSetQueryObjectTopHeadLinesRequest
                        .Include(res => res.CreatedByUser);

            return dbSetQueryObjectTopHeadLinesRequest.Include(r => r);             
        }
    }
}
