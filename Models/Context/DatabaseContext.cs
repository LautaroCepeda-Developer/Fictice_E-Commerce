using Microsoft.EntityFrameworkCore;

namespace Models.Context
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<CompanyData> CompanyData { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPersonalInformation> UserPersonalInformation { get; set; }
        public DbSet<UserBankAccount> UserBankAccounts { get; set; }
        public DbSet<BannedUser> BannedUsers { get; set; }
        public DbSet<BannedBankAccount> BannedBankAccounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductSale> ProductsSales { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             Most of the information in the database is financial,
             and the data must always remain.
             Deletion of information that requires cascade deletion will be configured manually.
            */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.Entity<User>()
                .HasOne(u=>u.PersonalInformation)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
