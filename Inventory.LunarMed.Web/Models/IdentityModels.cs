using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Data.Entities.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Inventory.LunarMed.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public string User { get; private set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region Entities

        public DbSet<Client> Clients { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Order> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<UnitSize> UnitSizes { get; set; }

        #endregion

        /// <summary>
        /// Set the User property 
        /// </summary>
        private void CreateAuditLogs()
        {
            if (string.IsNullOrEmpty(this.User))
            {
                this.User = "Admin";
            }

            this.SetAuditFields(this.User);
        }

        /// <summary>
        /// Set values for audit fields for added or modified entities.
        /// </summary>
        private void SetAuditFields(string user)
        {
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            // Iterate over all entities in the context that are new or modified.
            // If the entity derives from ModelBase, set the values for audit fields.
            foreach (var entry in entries)
            {
                var model = entry.Entity as BaseModel;
                if (model != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        model.DateCreated = DateTime.Now;
                        model.UserCreated = user;
                    }
                    // Always set the Modified fields.
                    model.DateModified = DateTime.Now;
                    model.UserModified = user;
                }
            }
        }

        /// <summary>
        /// Save changes to the model.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override int SaveChanges()
        {
            var autoDetectChanges = Configuration.AutoDetectChangesEnabled;

            try
            {
                Configuration.AutoDetectChangesEnabled = false;
                ChangeTracker.DetectChanges();

                // Set values for audit fields for added or modified entities.
                this.CreateAuditLogs();

                var errors = GetValidationErrors().ToList();
                if (errors.Any())
                {
                    throw new DbEntityValidationException("Validation errors found during save.", errors);
                }

                ChangeTracker.DetectChanges();
                Configuration.ValidateOnSaveEnabled = false;
                return base.SaveChanges();
            }
            finally
            {
                Configuration.AutoDetectChangesEnabled = autoDetectChanges;
            }
        }

        /// <summary>
        /// Configure the model.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder instance to add the configuration to.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Change Database Table Names to Singular Form
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}