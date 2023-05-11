using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MonteNegRo.Models;
using System.Security.Cryptography.X509Certificates;

namespace MonteNegRo.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Benefit_Property> Benefits_Properties { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Photo_Property> Photos_Properties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<City_Property> City_Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Local> Locals { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Local_Benefit> Local_Benefits { get; set; }
        public DbSet<Local_City> Local_Cities { get; set; }
        public DbSet<Local_Neighborhood> Local_Neighborhoods { get; set; }
        public DbSet<Local_Property> Local_Properties { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<RealticAccount> RealticAccounts { get; set; }
        public DbSet<AdvertisementSetting> AdvertisementSettings { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Partner_City> Partner_Cities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PartnerPhone> PartnerPhones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benefit_Property>().HasOne(x => x.Benefit)
                .WithMany(x => x.Benefit_Properties)
                .HasForeignKey(x => x.Benefit_ID);
            modelBuilder.Entity<Benefit_Property>().HasOne(x => x.Property)
                .WithMany(x => x.Benefit_Properties)
                .HasForeignKey(x => x.Property_ID);


            modelBuilder.Entity<City_Property>().HasOne(x => x.City)
                .WithMany(x => x.City_Properties)
                .HasForeignKey(x => x.City_ID);
            modelBuilder.Entity<City_Property>().HasOne(x => x.Property)
                .WithMany(x => x.City_Properties)
                .HasForeignKey(x => x.Property_ID);


            modelBuilder.Entity<Photo_Property>().HasOne(x => x.Property)
                .WithMany(x => x.Photo_Properties)
                .HasForeignKey(x => x.Property_ID);
            modelBuilder.Entity<Photo_Property>().HasOne(x => x.Photo)
                .WithMany(x => x.Photo_Properties)
                .HasForeignKey(x => x.Photo_ID);


            modelBuilder.Entity<User>().HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserType_ID);

            modelBuilder.Entity<Neighborhood>().HasOne(x => x.City)
                .WithMany(x => x.Neighborhoods)
                .HasForeignKey(x => x.City_ID);

            modelBuilder.Entity<Local_Benefit>().HasOne(x => x.Benefit)
                .WithMany(x => x.Local_Benefits)
                .HasForeignKey(x => x.Benefit_ID);
            modelBuilder.Entity<Local_Benefit>().HasOne(x => x.Local)
                .WithMany(x => x.Local_Benefits)
                .HasForeignKey(x => x.Local_ID);

            modelBuilder.Entity<Local_City>().HasOne(x => x.Local)
                .WithMany(x => x.Local_Cities)
                .HasForeignKey(x => x.Local_ID);
            modelBuilder.Entity<Local_City>().HasOne(x => x.City)
                .WithMany(x => x.Local_Cities)
                .HasForeignKey(x => x.City_ID);

            modelBuilder.Entity<Local_Property>().HasOne(x => x.Property)
                .WithMany(x => x.Local_Properties)
                .HasForeignKey(x => x.Property_ID);
            modelBuilder.Entity<Local_Property>().HasOne(x => x.Local)
                .WithMany(x => x.Local_Properties)
                .HasForeignKey(x => x.Local_ID);

            modelBuilder.Entity<Local_Neighborhood>().HasOne(x => x.Local)
                .WithMany(x => x.Local_Neighborhoods)
                .HasForeignKey(x => x.Local_ID);
            modelBuilder.Entity<Local_Neighborhood>().HasOne(x => x.Neighborhood)
                .WithMany(x => x.Local_Neighborhoods)
                .HasForeignKey(x => x.Neighborhood_ID);
            modelBuilder.Entity<Property>().HasOne(x => x.Counterparty)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.Counterparty_ID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Partner_City>().HasOne(x => x.City)
                .WithMany(x => x.Partner_Cities)
                .HasForeignKey(x => x.City_ID);
            modelBuilder.Entity<Partner_City>().HasOne(x => x.Partner)
                .WithMany(x => x.Partner_Cities)
                .HasForeignKey(x => x.Partner_ID);

            modelBuilder.Entity<Notification>().HasOne(x => x.Property)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.Property_ID);

            modelBuilder.Entity<PartnerPhone>().HasOne(x => x.Partner)
                .WithMany(x => x.PartnerPhones)
                .HasForeignKey(x => x.Partner_ID);

            modelBuilder.Entity<AdvertisementSetting>().HasOne(x => x.Property)
                .WithMany(x => x.AdvertisementSettings)
                .HasForeignKey(x => x.Property_ID);

            //modelBuilder.Entity<RealticAccount_AdvertisementSetting>().HasOne(x => x.RealticAccount)
            //    .WithMany(x => x.RealticAccount_AdvertisementSettings)
            //    .HasForeignKey(x => x.RealticAccount_ID);
            //modelBuilder.Entity<RealticAccount_AdvertisementSetting>().HasOne(x => x.AdvertisementSetting)
            //    .WithMany(x => x.RealticAccount_AdvertisementSettings)
            //    .HasForeignKey(x => x.AdvertisementSetting_ID);

            modelBuilder.Entity<AdvertisementSetting>().HasOne(x => x.RealticAccountRent)
                .WithMany(x => x.AdvertisementSettingsRent)
                .HasForeignKey(x => x.RealticAccountRent_ID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<AdvertisementSetting>().HasOne(x => x.RealticAccountSale)
                .WithMany(x => x.AdvertisementSettingsSale)
                .HasForeignKey(x => x.RealticAccountSale_ID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Property>()
                .Property(c => c.RentStatus)
                .HasConversion<string>();
            modelBuilder.Entity<Property>()
                .Property(c => c.SaleStatus)
                .HasConversion<string>();
            modelBuilder.Entity<Property>()
                .Property(c => c.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Counterparty>()
                .Property(c => c.Type)
                .HasConversion<string>();
        }
    }
}
