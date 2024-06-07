using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Repositories.DbContext;

namespace PetHealthcare.Server.Helpers
{
    public class DataSeeder
    {
        public static async void SeedRoles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                 $"Data Source=MEOMATLON\\SQLEXPRESS; User = sa; Password =MukuroHoshimiya;" +
                 $"Initial Catalog=PetHealthCareSystemAuth;Integrated Security=True;" +
                 $"Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;" +
                 $"Application Intent=ReadWrite;Multi Subnet Failover=False");

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
                var roles = new List<(string name, string normalizedName)>
                {
                    ("Admin", "ADMIN"),
                    ("Staff", "STAFF"),
                    ("Customer", "Customer"),
                    ("Vet", "VET"),
                    //("Guest", "GUEST")
                };

                foreach (var (name, normalizedName) in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == name))
                    {
                        await roleStore.CreateAsync(new ApplicationRole
                        {
                            Name = name,
                            NormalizedName = normalizedName
                        });
                    }
                }

                await context.SaveChangesAsync();
            }

        }
    }
}
