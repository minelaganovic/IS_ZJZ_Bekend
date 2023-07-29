using IS_ZJZ_B.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Context
{
    public class AppDbContext: DbContext
    {
        //konstruktor
        public AppDbContext(DbContextOptions <AppDbContext> options ): base (options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<healthcards> Healthcards { get; set; }
        public DbSet<AdministrativeWorker> Administrativeworkers { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Guidance> guidances { get; set; }
        public DbSet<ExpenseTravel>travelExpenses { get; set; }

        //ADMINISTRATOR
        public DbSet<HealthCenterEmployee> HealthCenterEmployees { get; set; }

        //modelBuilder je klasa koja pomaže za konekciju sa entitijem u .net
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<AdministrativeWorker>().ToTable("administrativeworkers");
            modelBuilder.Entity<Admin>().ToTable("admins");

            //ADMINISTRATOR
            modelBuilder.Entity<HealthCenterEmployee>().ToTable("healthcenteremployee");

            //Korisnik
            modelBuilder.Entity<healthcards>().ToTable("healthcards");
            modelBuilder.Entity<Request>().ToTable("requests");
            modelBuilder.Entity<RequestType>().ToTable("requesttype");

            //Administrativni radnik
            modelBuilder.Entity<Guidance>().ToTable("gcard");
            modelBuilder.Entity<ExpenseTravel>().ToTable("expensess");

        }
    }
}
