using Microsoft.EntityFrameworkCore;
using NLTDSimpleInventory.BusinessLayer.Interfaces;
using NLTDSimpleInventory.BusinessLayer.Services;
using NLTDSimpleInventory.DataLayer.Models;

namespace NLTDSimpleInventory.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); 


            builder.Services.AddDbContext<SimpleInventoryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IItemService, ItemService>();

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages(); 

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Item}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
