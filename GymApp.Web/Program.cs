using GymApp.BLL;
using GymApp.BLL.Contracts;
using GymApp.BLL.Services;
using GymApp.DAl.Context;
using GymApp.DAl.Contracts;
using GymApp.DAl.DataSeeding;
using GymApp.DAl.Models;
using GymApp.DAl.Repositories;
using GymApp.Web.Extensions;
using GymManagement.BLL.Services.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IAttachmentService, AttachmentService>();
        builder.Services.AddScoped<ISessionService, SessionService>();
        builder.Services.AddScoped<ISessionRepository, SessionRepository>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        //builder.Services.AddScoped<IPlansRepository, PlansRepository>();
        builder.Services.AddScoped<IPlansService, PlansService>();
        builder.Services.AddScoped<IMemberService, MemberService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        //builder.Services.AddScoped<GymDbContext>();
        //builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));
        builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
        builder.Services.AddScoped<ITrainerService, TrainerService>();

        builder.Services.AddDbContext<GymDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
        {
            Options.Lockout.MaxFailedAccessAttempts = 5;
            Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(2);
            Options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<GymDbContext>();


        builder.Services.ConfigureApplicationCookie(cfg =>
        {
            cfg.AccessDeniedPath = "/Account/AccessDenied";
            cfg.LogoutPath = "/Account/Login";
        });
        
        var app = builder.Build();

        await app.MigrateAndSeedAsync();

        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}