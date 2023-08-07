using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shoppite.Core.Model;

#nullable disable

namespace Shoppite.Infrastructure.Data
{
    public partial class Shoppite_masterContext : DbContext
    {
        public Shoppite_masterContext()
        {
        }

        public Shoppite_masterContext(DbContextOptions<Shoppite_masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdsDetail> AdsDetails { get; set; }
        public virtual DbSet<AdsPageName> AdsPageNames { get; set; }
        public virtual DbSet<AdsPlacement> AdsPlacements { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AttributesSetup> AttributesSetups { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }
        public virtual DbSet<CategoryOne> CategoryOnes { get; set; }
        public virtual DbSet<CategoryThree> CategoryThrees { get; set; }
        public virtual DbSet<CategoryTwo> CategoryTwos { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomerWishlist> CustomerWishlists { get; set; }
        public virtual DbSet<DonationMaster> DonationMasters { get; set; }
        public virtual DbSet<DonationReceived> DonationReceiveds { get; set; }
        public virtual DbSet<EmailSetup> EmailSetups { get; set; }
        public virtual DbSet<Logo> Logos { get; set; }
        public virtual DbSet<MainPageCategory> MainPageCategories { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<NewsLetter> NewsLetters { get; set; }
        public virtual DbSet<OrderBasic> OrderBasics { get; set; }
        public virtual DbSet<OrganizationCategory> OrganizationCategory { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDisbursement> OrderDisbursements { get; set; }
        public virtual DbSet<OrderMaster> OrderMasters { get; set; }
        public virtual DbSet<OrderShipping> OrderShippings { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OrderVariation> OrderVariations { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Organization_Aggregator_Control> Organization_Aggregator_Controls { get; set; }
        public virtual DbSet<PageCategory> PageCategories { get; set; }
        public virtual DbSet<PageCategoryDetail> PageCategoryDetails { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductBasic> ProductBasics { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<ProductRecentlyViewed> ProductRecentlyVieweds { get; set; }
        public virtual DbSet<ProductSeo> ProductSeos { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SocialMedium> SocialMedia { get; set; }
        public virtual DbSet<SpecificationSetup> SpecificationSetups { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Notifications_Token> NotificationsTokens { get; set; }
        public virtual DbSet<UserActivation> UserActivations { get; set; }
        public virtual DbSet<UsersMembership> UsersMemberships { get; set; }
        public virtual DbSet<UsersProfile> UsersProfiles { get; set; }
        public virtual DbSet<VendorMembershipPackage> VendorMembershipPackages { get; set; }
        public virtual DbSet<WebsiteSetup> WebsiteSetups { get; set; }
        public virtual DbSet<WebsiteSetupScript> WebsiteSetupScripts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=103.150.186.216;Database=Shoppite_master;User Id=sa;Password=Z8Lix[jg3K@R74;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdsDetail>(entity =>
            {
                entity.HasKey(e => e.AdId);

                entity.ToTable("Ads_Detail");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AdsPageName>(entity =>
            {
                entity.HasKey(e => e.AdsPageId);

                entity.ToTable("Ads_PageName");

                entity.Property(e => e.PageName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<AdsPlacement>(entity =>
            {
                entity.ToTable("Ads_Placement");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.PlacementName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.Name, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId, "IX_RoleId");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AttributesSetup>(entity =>
            {
                entity.HasKey(e => e.AttributeId)
                    .HasName("PK_Attributes");

                entity.ToTable("Attributes_Setup");

                entity.Property(e => e.AttributeDescription).HasMaxLength(500);

                entity.Property(e => e.AttributeName).HasMaxLength(200);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandImage).HasMaxLength(1000);

                entity.Property(e => e.BrandName).HasMaxLength(200);

                entity.Property(e => e.BrandUrlpath)
                    .HasMaxLength(500)
                    .HasColumnName("BrandURLPath");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("category_master");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Banner).HasMaxLength(1000);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(200)
                    .HasColumnName("category_name");

                entity.Property(e => e.Icon).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(50)
                    .HasColumnName("SEO_Description");

                entity.Property(e => e.SeoKeyword)
                    .HasMaxLength(1000)
                    .HasColumnName("SEO_Keyword");

                entity.Property(e => e.SeoPageName)
                    .HasMaxLength(100)
                    .HasColumnName("SEO_PageName");

                entity.Property(e => e.SeoTitle)
                    .HasMaxLength(100)
                    .HasColumnName("SEO_Title");

                entity.Property(e => e.Urlpath)
                    .HasMaxLength(1000)
                    .HasColumnName("URLPath");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CategoryOne>(entity =>
            {
                entity.ToTable("Category_One");

                entity.Property(e => e.Banner).HasMaxLength(500);

                entity.Property(e => e.CategoryName).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.IsFeatured).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UrlPath).HasMaxLength(300);
            });

            modelBuilder.Entity<CategoryThree>(entity =>
            {
                entity.ToTable("Category_Three");

                entity.Property(e => e.Banner).HasMaxLength(500);

                entity.Property(e => e.CategoryThreeName).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.IsFeatured).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UrlPath).HasMaxLength(300);
            });

            modelBuilder.Entity<CategoryTwo>(entity =>
            {
                entity.ToTable("Category_Two");

                entity.Property(e => e.Banner).HasMaxLength(500);

                entity.Property(e => e.CategoryTwoName).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.IsFeatured).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UrlPath).HasMaxLength(300);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(2000);

                entity.Property(e => e.InsertBy).HasMaxLength(50);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InsertDate).HasColumnType("date");

                entity.Property(e => e.Isbase).HasColumnName("ISBase");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerWishlist>(entity =>
            {
                entity.HasKey(e => e.WishlistId);

                entity.ToTable("Customer_Wishlist");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<DonationMaster>(entity =>
            {
                entity.HasKey(e => e.RequestFundId);

                entity.ToTable("Donation_Master");

                entity.Property(e => e.AdminRemarks).HasMaxLength(1000);

                entity.Property(e => e.Administrativefee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.PaypalId)
                    .HasMaxLength(256)
                    .HasColumnName("PaypalID");

                entity.Property(e => e.RequestFundGuid)
                    .HasColumnName("RequestFundGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Title).HasMaxLength(1000);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<DonationReceived>(entity =>
            {
                entity.ToTable("Donation_Received");

                entity.Property(e => e.AdministrativeAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AdministrativeFeesPer).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaypalId).HasMaxLength(256);

                entity.Property(e => e.RequestFundGuid).HasColumnName("RequestFundGUID");
            });

            modelBuilder.Entity<EmailSetup>(entity =>
            {
                entity.HasKey(e => e.EmailSettingId);

                entity.ToTable("Email_Setup");

                entity.Property(e => e.Bcc)
                    .HasMaxLength(256)
                    .HasColumnName("BCC");

                entity.Property(e => e.EmailFrom).HasMaxLength(256);

                entity.Property(e => e.Host).HasMaxLength(500);

                entity.Property(e => e.Password).HasMaxLength(300);

                entity.Property(e => e.Smtpport).HasColumnName("SMTPPort");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Logo>(entity =>
            {
                entity.ToTable("Logo");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Favicon).HasMaxLength(1000);

                entity.Property(e => e.FooterLogo).HasMaxLength(1000);

                entity.Property(e => e.Keyword).HasMaxLength(1000);

                entity.Property(e => e.Logo1)
                    .HasMaxLength(1000)
                    .HasColumnName("Logo");

                entity.Property(e => e.Logoheight).HasColumnName("logoheight");

                entity.Property(e => e.Logowidth).HasColumnName("logowidth");

                entity.Property(e => e.SupportContact).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(1000);

                entity.Property(e => e.WebsiteName).HasMaxLength(50);
            });

            modelBuilder.Entity<MainPageCategory>(entity =>
            {
                entity.ToTable("MainPageCategory");

                entity.Property(e => e.MainPageCategory1)
                    .HasMaxLength(50)
                    .HasColumnName("MainPageCategory");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MesageId);

                entity.Property(e => e.Attachment).HasMaxLength(3000);

                entity.Property(e => e.ChatId)
                    .HasColumnName("ChatID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Message1)
                    .HasMaxLength(4000)
                    .HasColumnName("Message");

                entity.Property(e => e.Recieveddate)
                    .HasColumnType("datetime")
                    .HasColumnName("recieveddate");

                entity.Property(e => e.Recipient)
                    .HasMaxLength(256)
                    .HasColumnName("recipient");

                entity.Property(e => e.Senddate)
                    .HasColumnType("datetime")
                    .HasColumnName("senddate");

                entity.Property(e => e.Sender)
                    .HasMaxLength(256)
                    .HasColumnName("sender");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<NewsLetter>(entity =>
            {
                entity.ToTable("NewsLetter");

                entity.Property(e => e.NewsletterId).HasColumnName("NewsletterID");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderBasic>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Order_Basic");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.Commission).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Currencyid).HasColumnName("currencyid");

                entity.Property(e => e.DeliveryFees).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Donation).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.LastOrderStatus).HasMaxLength(50);

                entity.Property(e => e.OrderGuid)
                    .HasColumnName("OrderGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.OrderVariation).HasMaxLength(2000);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMode).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(50)
                    .HasColumnName("ReferenceID");

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order_Detail");

                entity.Property(e => e.DeliveryFees).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderGuid).HasColumnName("OrderGUID");

                entity.Property(e => e.TotalOrderAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<OrderDisbursement>(entity =>
            {
                entity.HasKey(e => e.OrderEarningId);

                entity.ToTable("Order_Disbursement");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisburseDate).HasColumnType("datetime");

                entity.Property(e => e.DisbursementId).HasMaxLength(500);

                entity.Property(e => e.DisbursementMode).HasMaxLength(50);

                entity.Property(e => e.InsertBy).HasMaxLength(256);
            });

            modelBuilder.Entity<OrderMaster>(entity =>
            {
                entity.HasKey(e => e.OrderMasterId);
                entity.Property(e => e.OrderGuid);

                entity.ToTable("Order_Master");

                entity.Property(e => e.OrderGuid)
                    .HasColumnName("OrderGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.InsertDate).HasColumnType("date");

                entity.Property(e => e.OrderKeyStatus).HasMaxLength(20);
            });

            modelBuilder.Entity<OrderShipping>(entity =>
            {
                entity.HasKey(e => e.ShippingId);

                entity.ToTable("Order_Shipping");

                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Contactnumber).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.OrderGuid).HasColumnName("OrderGUID");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.Zipcode).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("Order_Status");

                entity.Property(e => e.Insertby).HasMaxLength(256);

                entity.Property(e => e.OrderStatus1)
                    .HasMaxLength(50)
                    .HasColumnName("OrderStatus");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderVariation>(entity =>
            {
                entity.ToTable("Order_Variation");

                entity.Property(e => e.OrderGuid).HasColumnName("OrderGUID");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("organization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.OrgAddress)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("org_address");

                entity.Property(e => e.OrgDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("org_description");

                entity.Property(e => e.OrgLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("org_logo");

                entity.Property(e => e.OrgName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("org_name");

                entity.Property(e => e.Pincode).HasColumnName("pincode");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.VAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("v_account_number");

                entity.Property(e => e.VBankBranchName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("v_bank_branch_name");

                entity.Property(e => e.VBankName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("v_bank_name");

                entity.Property(e => e.VEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("v_email");

                entity.Property(e => e.VIdProof)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("v_id_proof");

                entity.Property(e => e.VIfscCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("v_ifsc_code");

                entity.Property(e => e.VMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("v_mobile");

                entity.Property(e => e.VPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("v_phone");

                entity.Property(e => e.VUpiId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("v_upi_id");

                entity.Property(e => e.VenderName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("vender_name");
            });

            modelBuilder.Entity<Organization_Aggregator_Control>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Organization_Aggregator_Control");
            });
            modelBuilder.Entity<OrganizationCategory>(entity =>
            {
                entity.HasKey(e => e.Org_CategoryId);

                entity.ToTable("OrganizationCategory");
            });
            modelBuilder.Entity<PageCategory>(entity =>
            {
                entity.ToTable("PageCategory");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.IsUrl).HasColumnName("IsURL");

                entity.Property(e => e.PageCategory1)
                    .HasMaxLength(50)
                    .HasColumnName("PageCategory");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<PageCategoryDetail>(entity =>
            {
                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("Product_Attribute");

                entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductBasic>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("Product_Basic");

                entity.Property(e => e.CoverImage).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductStartDate).HasColumnType("datetime");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.ShortDescription).HasMaxLength(500);

                entity.Property(e => e.Sku)
                    .HasMaxLength(200)
                    .HasColumnName("SKU");

                entity.Property(e => e.Urlpath)
                    .HasMaxLength(500)
                    .HasColumnName("URLPath");
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.ToTable("Product_Brands");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("Product_Category");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.MainCatId).HasColumnName("MainCat_Id");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("Product_Discount");

                entity.Property(e => e.DiscountEndDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountOffer).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountStartDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountType).HasMaxLength(20);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(100)
                    .HasColumnName("ModuleID");

                entity.Property(e => e.ModuleType).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.ProductImagesId);

                entity.ToTable("Product_Images");

                entity.Property(e => e.AltText).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.ToTable("Product_Inventory");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.ToTable("Product_Price");

                entity.Property(e => e.DeliveryFees).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");
            });

            modelBuilder.Entity<ProductRecentlyViewed>(entity =>
            {
                entity.HasKey(e => e.RecentlyViewId);

                entity.ToTable("Product_Recently_Viewed");

                entity.Property(e => e.Insertdate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(100)
                    .HasColumnName("IP");
            });

            modelBuilder.Entity<ProductSeo>(entity =>
            {
                entity.HasKey(e => e.Seoid);

                entity.ToTable("Product_SEO");

                entity.Property(e => e.Seoid).HasColumnName("SEOId");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(500)
                    .HasColumnName("SEO_Keywords");

                entity.Property(e => e.SeoMetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("SEO_MetaTItle");

                entity.Property(e => e.SeoMetadescription)
                    .HasMaxLength(1000)
                    .HasColumnName("SEO_Metadescription");

                entity.Property(e => e.SeoPageName)
                    .HasMaxLength(500)
                    .HasColumnName("SEO_PageName");
            });

            modelBuilder.Entity<ProductSpecification>(entity =>
            {
                entity.ToTable("Product_Specification");

                entity.Property(e => e.ControlType).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.Insertdate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("Product_Status");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(e => e.ProductTagsId);

                entity.ToTable("Product_Tags");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ProductGuid).HasColumnName("ProductGUID");

                entity.Property(e => e.ProductTags).HasMaxLength(3000);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Comments).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Module).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<SocialMedium>(entity =>
            {
                entity.HasKey(e => e.SocialMediaId);

                entity.Property(e => e.Icon).HasMaxLength(4000);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<SpecificationSetup>(entity =>
            {
                entity.HasKey(e => e.SpecificationId);

                entity.ToTable("Specification_Setup");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SpecificationName).HasMaxLength(200);

                entity.Property(e => e.SpecificiatoinDescription).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.CssClass).HasMaxLength(1000);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Status1)
                    .HasMaxLength(50)
                    .HasColumnName("Status");

                entity.Property(e => e.Urlpath)
                    .HasMaxLength(100)
                    .HasColumnName("URLPath");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UserActivation>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserActivation");

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            modelBuilder.Entity<UsersMembership>(entity =>
            {
                entity.HasKey(e => e.MembershipId);

                entity.ToTable("Users_Membership");

                entity.Property(e => e.CancelStatus).HasMaxLength(50);

                entity.Property(e => e.Cancellationdate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MembershipFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MembershipStatus).HasMaxLength(50);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMode).HasMaxLength(50);

                entity.Property(e => e.PaymentStatus).HasMaxLength(50);

                entity.Property(e => e.ReferenceId).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsersProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.ToTable("Users_Profile");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.AdminStatus).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ContactNumber).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CoverImage).HasMaxLength(200);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(100)
                    .HasColumnName("latitude");

                entity.Property(e => e.Logo).HasMaxLength(200);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(100)
                    .HasColumnName("longitude");

                entity.Property(e => e.PaypalId).HasMaxLength(256);

                entity.Property(e => e.ProfileGuid)
                    .HasColumnName("ProfileGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ShopDescription).HasMaxLength(2000);

                entity.Property(e => e.ShopName).HasMaxLength(200);

                entity.Property(e => e.ShopUrlpath)
                    .HasMaxLength(500)
                    .HasColumnName("ShopURLPath");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.Zip).HasMaxLength(50);
            });

            modelBuilder.Entity<VendorMembershipPackage>(entity =>
            {
                entity.HasKey(e => e.Membershipid)
                    .HasName("PK_Vendor_Membership");

                entity.ToTable("Vendor_Membership_Package");

                entity.Property(e => e.Fees).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Membershiptype).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Unit).HasMaxLength(50);
            });

            modelBuilder.Entity<WebsiteSetup>(entity =>
            {
                entity.ToTable("Website_Setup");

                entity.Property(e => e.DeductionType).HasMaxLength(10);

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.ItemDescription).HasMaxLength(500);

                entity.Property(e => e.ItemKey).HasMaxLength(50);

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.ItemValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<WebsiteSetupScript>(entity =>
            {
                entity.HasKey(e => e.Scriptid);

                entity.ToTable("Website_Setup_Script");

                entity.Property(e => e.Scriptname).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(30);
            });

            modelBuilder.Entity<Notifications_Token>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Notifications_Token");
            });
            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Notifications");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
