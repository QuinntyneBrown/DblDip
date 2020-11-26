using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using Microsoft.Extensions.Configuration;
using ShootQ.Core.Models;
using System.Linq;

namespace ShootQ.Core.Seeding
{
    public static class SeedData
    {
        public static void Seed(IAppDbContext context, IConfiguration configuration)
        {
            var user = context.Set<User>().FirstOrDefault(x => x.Username == "quinntynebrown@gmail.com");

            if(user == null)
            {
                user = new User("quinntynebrown@gmail.com", "");
                var password = new PasswordHasher().HashPassword(user.Salt, "shootq");
                user.ChangePassword(password);
                context.Store(user);
                context.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }


    }
}
