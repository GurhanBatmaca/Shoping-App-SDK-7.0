using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace shopapp.webui.Identity
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Seed(builder);
        }

        private static void Seed(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData
            (
                new ApplicationUser() 
                {
                    UserName ="Admin2",
                    NormalizedUserName="ADMİN2",
                    FirstName ="Admin2",
                    LastName ="Admin2",
                    Email="admin2@shopapp.com",
                    NormalizedEmail="ADMİN2@SHOPAPP.COM",
                    PasswordHash="Shopapp_123",
                    EmailConfirmed=true,
                    SecurityStamp="abc123",
                    ConcurrencyStamp="123abc"
                }
            );
        }
    }
}

