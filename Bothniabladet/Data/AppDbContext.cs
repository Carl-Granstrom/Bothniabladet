using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bothniabladet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        //not sure how to handle the two-way relationships here but atm there is no jointable, the Invoices hold an FK to the Client
        //I'm not sure how it will work querying for either of these, but you surely want to be able to query for both specific invoices
        //and then navigate to the clint they belong to as well as finding all invoices that belong to a particular client. 
        //So, navigation has to work both ways.
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //storing the NewsSection enum as an integer
            modelBuilder.Entity<Image>()
                .Property(c => c.Section)
                .HasConversion<string>();

            modelBuilder.Entity<Image>()
                .HasOne(c => c.SectionRelation)
                .WithMany()
                .HasForeignKey(c => c.Section);

            //configuring Ownedproperty
            modelBuilder.Entity<Image>()
                .OwnsOne(s => s.ImageLicense)
                .ToTable("ImageLicense");

            //configuring Ownedproperty
            modelBuilder.Entity<Image>()
                .OwnsOne(s => s.ImageMetaData)
                .ToTable("ImageMetaData");

            //storing the enums in their own table
            modelBuilder.Entity<SectionEnum>()
                .ToTable("Enums");

            modelBuilder.Entity<SectionEnum>()
                .Property(s => s.Name)
                .HasConversion<string>();

            modelBuilder.Entity<SectionEnum>()
                .HasKey(s => s.Name);

            base.OnModelCreating(modelBuilder);

            //Create the enums in the database on startup
            modelBuilder.Entity<SectionEnum>().HasData(
                new SectionEnum
                {
                    Name = NewsSection.CULTURE
                },
                new SectionEnum
                {
                    Name = NewsSection.ECONOMY
                },
                new SectionEnum
                {
                    Name = NewsSection.NEWS
                },
                new SectionEnum
                {
                    Name = NewsSection.SPORTS
                }
                );
        }
    }
}

