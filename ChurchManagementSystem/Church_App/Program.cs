﻿using DataAccess.Data;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

// Add bold report services
Bold.Licensing.BoldLicenseProvider.RegisterLicense("Y6LiubUOovhwkp6Tx51f35QqKEWgI47X8rMfB3QkgQQ=");
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddMvcOptions(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "This field is required.");
});
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PermissionHelper>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IDonationTypeService, DonationTypeService>();
builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ISystemRoutService, SystemRoutService>();
builder.Services.AddScoped<IPagePermissionService, PagePermissionService>();
builder.Services.AddScoped<IChurchSettingService, ChurchSettingService>();


var app = builder.Build();


// Seed admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedAdminUserAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers(); // User For Bold Report Controller
app.UseAuthorization();
app.UseSession(); // 🔥 Add this here
app.MapRazorPages();

app.Run();
