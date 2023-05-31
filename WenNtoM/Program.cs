using WenNtoM.Models;

namespace WenNtoM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // statt using() Datenbank Kontext Hinzufügen
            // An Zentraler stelle wird Datenbank Kontext hinzugefügt
            // Jeder Controller kann diesen Datenbank Konext verwenden
            // Dependency Injection
            builder.Services.AddDbContext<CS2023KursContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}