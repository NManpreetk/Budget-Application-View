using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BudgetApplicationView.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreatedHouseHolds = new HashSet<HouseHolds>();
            Transactions = new HashSet<Transactions>();
            HouseHolds = new HashSet<HouseHolds>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [InverseProperty("User")]
        public virtual ICollection<HouseHolds> CreatedHouseHolds { get; set; }
        [InverseProperty("HouseHoldUser")]
        public virtual ICollection<HouseHolds> HouseHolds { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.ApplicationUserHouseHolds> ApplicationUserHouseHolds { get; set; }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.HouseHolds> HouseHolds { get; set; }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.HouseHoldInvites> HouseHoldInvites { get; set; }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.Categories> Categories { get; set; }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.Accounts> Accounts { get; set; }

        public System.Data.Entity.DbSet<BudgetApplicationView.Models.Transactions> Transactions { get; set; }
    }
}