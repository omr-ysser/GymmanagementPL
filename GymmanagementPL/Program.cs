using GymmanagmentDAL.Entities;
using GymmanagmentDAL.Entities.Data.Contexts;
using GymmanagmentDAL.Repositories.Classes;
using Microsoft.EntityFrameworkCore;

namespace GymmanagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer("Server=.;Database=GymManagemnet;Trusted_Connection=True;TrustServerCertificate=True;");
                // options.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["DefaultConnection"]);
               // options.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]);
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<GenericRepository<Member>,GenericRepository<Member>>();
            //builder.Services.AddScoped<GenericRepository<Trainer>, GenericRepository<Trainer>>();
            //builder.Services.AddScoped<GenericRepository<Plan>, GenericRepository<Plan>>();
            builder.Services.AddScoped(typeof(GenericRepository<>), typeof(GenericRepository<>));
            var app = builder.Build();

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
}
