using BuildingBlocks.Abstractions;
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
            
            user ??= new User("quinntynebrown@gmail.com", "shootq");
            
            context.Store(user);

            context.SaveChangesAsync(default).GetAwaiter().GetResult();
        }
    }
}
