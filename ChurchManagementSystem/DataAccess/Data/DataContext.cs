using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicaitonUsers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<DonationType> DonationTypes { get; set; }
        public DbSet<DepositTB> DepositTBs { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<SystemRout> SystemRouts { get; set; }
        public DbSet<RoutPermission> RoutPermissions { get; set; }
        public DbSet<ChurchSetting> ChurchSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Fix Role Foreign Key Issue
            builder.Entity<ApplicationUser>()
                .HasOne<IdentityRole>(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete issue

            #region Data Seeding
            // Static GUIDs
            string adminRoleId = "a1b2c3d4-e5f6-4g7h-8i9j-k0l1m2n3o4p5"; // Predefined Admin Role ID
            string adminUserId = "12345678-90ab-cdef-ghij-klmnopqrstuv"; // Predefined Admin User ID

            // Seed Role
            var adminRole = new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            builder.Entity<IdentityRole>().HasData(adminRole);

            // Seed Admin User
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@newlightmbc.com",
                NormalizedUserName = "ADMIN@newlightmbc.com".ToUpper(),
                Email = "admin@newlightmbc.com",
                NormalizedEmail = "ADMIN@newlightmbc.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@12345"), // Default password
                SecurityStamp = new DateTime(2025, 1, 1).ToString(),
                RoleId = adminRoleId // Assign Admin Role
            };

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Seed User Role Relationship
            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);

            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
