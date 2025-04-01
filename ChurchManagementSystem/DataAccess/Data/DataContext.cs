using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
                .OnDelete(DeleteBehavior.Restrict);  // ✅ Prevents cascade delete issue


            #region Dataseeding
            // Static GUIDs
            string adminRoleId = "a1b2c3d4-e5f6-4g7h-8i9j-k0l1m2n3o4p5";
            string adminUserId = "12345678-90ab-cdef-ghij-klmnopqrstuv";
            // Seed Role
            var adminRole = new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            builder.Entity<IdentityRole>().HasData(adminRole);
            
            #endregion

        }

    }
}
