using EFAssignment1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFAssignment1.Startup))]
namespace EFAssignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = "admin@school.com",
                    Email = "admin@school.com",
                    BirthDate = System.DateTime.Now
                };
                string password = "Password1";
                IdentityResult user = userManager.Create(appUser, password);
                if(user.Succeeded)
                {
                    IdentityResult result = userManager.AddToRole(appUser.Id, "Admin");
                }
            }

            if(!roleManager.RoleExists("Teacher"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }

            if(!roleManager.RoleExists("Supervisor"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);
            }

        }

    }
}
