using IdentityModel;
using IdentityServer.DbContext;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
                return;

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Arpit",
                LastName = "Singh"
            };
            _userManager.CreateAsync(admin, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, SD.Admin).GetAwaiter().GetResult();
            var temp = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, admin.FirstName+" "+admin.LastName),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin)
            }).Result;

            ApplicationUser customer = new ApplicationUser()
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Customer",
                LastName = "Pandey"
            };
            _userManager.CreateAsync(customer, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customer, SD.Customer).GetAwaiter().GetResult();
            var temp1 = _userManager.AddClaimsAsync(customer, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customer.FirstName + " " + customer.LastName),
                new Claim(JwtClaimTypes.GivenName, customer.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customer.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer)
            }).Result;
        }
    }
}
