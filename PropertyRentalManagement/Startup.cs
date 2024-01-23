using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace PropertyRentalManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

 public void ConfigureServices(IServiceCollection services)
        {
            var connection =
           Configuration.GetConnectionString("FinalProjectDb");
            services.AddDbContext<Models.FinalProjectDbContext>(options
           => options.UseSqlServer(connection));
            services.AddControllersWithViews();
        }
 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
 app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
          
 app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
     name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
 });
        }
    }
}
