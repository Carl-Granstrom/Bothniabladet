using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bothniabladet.Data
{
    // IdentityUser was changed to ApplicationUser because we want to add custom data to the user (owned images for example)
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<SectionEnum> Enums { get; set; }
        public DbSet<ImageKeyword> ImageKeywords { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<EditedImage> EditedImages { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ApplicationUser> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //storing the NewsSection enum as an integer
            modelBuilder.Entity<Image>()
                .Property(c => c.Section)
                .HasConversion<string>();

            //configuring Ownedproperty
            modelBuilder.Entity<Image>()
                .OwnsOne(s => s.ImageLicense);

            //configuring Ownedproperty
            modelBuilder.Entity<Image>()
                .OwnsOne(s => s.ImageMetaData);

            //configuring EditedImages, one way relationship
            modelBuilder.Entity<Image>()
                .HasMany(c => c.EditedImages)
                .WithOne(s => s.Image);

            //configure compound primary key for ImageKeyword
            modelBuilder.Entity<ImageKeyword>()
                .HasKey(x => new { x.ImageId, x.KeywordId });

            //configure the many to many relationship of images and User for the shoppingcart
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(iu => new { iu.ImageId, iu.UserId });

            //configure shoppingcart to shoppingcartImage
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(iu => iu.User)
                .WithMany(u => u.ShoppingCart)
                .HasForeignKey(iu => iu.UserId);

            //configure image to shoppingcartImage
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(si => si.Image)
                .WithMany(i => i.ShoppingCart)
                .HasForeignKey(si => si.ImageId);

            modelBuilder.Entity<UserDocuments>()
                .HasKey(iu => new { iu.SalesDocumentId, iu.UserId });

            modelBuilder.Entity<UserDocuments>()
                .HasOne(iu => iu.User)
                .WithMany(u => u.UserDocuments)
                .HasForeignKey(iu => iu.UserId);

            modelBuilder.Entity<UserDocuments>()
                .HasOne(si => si.SalesDocument)
                .WithMany(i => i.UserDocuments)
                .HasForeignKey(si => si.SalesDocumentId);

            //create a unique contraint on Keyword.Word
            //Commented this out because handling unique constraints on a many-many is hairy at best, and a disaster at worst.
            //modelBuilder.Entity<Keyword>()
            //    .HasIndex(k => k.Word)
            //    .IsUnique();

            //storing the enums in their own table
            modelBuilder.Entity<SectionEnum>()
                .ToTable("Enums")
                .HasKey(c => c.SectionEnumId);

            modelBuilder.Entity<SectionEnum>()
                .Property(s => s.Name)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

            //Create the enums in the database on startup
            modelBuilder.Entity<SectionEnum>().HasData(
                new SectionEnum
                {
                    SectionEnumId = 1,
                    Name = NewsSection.CULTURE
                },
                new SectionEnum
                {
                    SectionEnumId = 2,
                    Name = NewsSection.ECONOMY
                },
                new SectionEnum
                {
                    SectionEnumId = 3,
                    Name = NewsSection.NEWS
                },
                new SectionEnum
                {
                    SectionEnumId = 4,
                    Name = NewsSection.INTERNATIONAL
                },
                new SectionEnum
                {
                    SectionEnumId = 5,
                    Name = NewsSection.SPORTS
                }
                );
        }
    }
}

