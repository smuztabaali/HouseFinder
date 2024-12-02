using HouseFinderAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HouseFinderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var DataBaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (DataBaseCreator != null)
                {
                    if (!DataBaseCreator.CanConnect()) DataBaseCreator.Create();
                    if (!DataBaseCreator.HasTables()) DataBaseCreator.CreateTables();
                }
            }
            catch (Exception ex) { }
        }
        //public DbSet<ApplicationUser> ApplicationUsers {  get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
