using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubrical.Data;
using Rubrical.Models;
using System;
using System.Linq;

namespace Rubrical
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "453363031628-ap1sbc5rq1o2r20a82fqvahhfvgj52nf.apps.googleusercontent.com";
                    options.ClientSecret = "x0Qm8gKX0gn0QrzRle2UBmr8";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                SeedData(serviceProvider);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void SeedData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            #region Mock Data
            var subject1 = new Subject { SubjectName = "Chemistry", SubjectDescription = "The study of chemistry" };
            var subject2 = new Subject { SubjectName = "Biology", SubjectDescription = "The study of biology" };
            var subject3 = new Subject { SubjectName = "Physics", SubjectDescription = "The study of physics" };

            var grade1 = new Grade { GradeName = "Grade 9 Applied", GradeDescription = "The applied level of grade 9." };
            var grade2 = new Grade { GradeName = "Grade 10 Academic", GradeDescription = "The academic level of grade 9." };
            var grade3 = new Grade { GradeName = "Grade 3" };
            #endregion

            if (!(db.Subjects.Where(x => x.SubjectName == subject1.SubjectName).Any()))
            {
                db.Subjects.Add(subject1);
                db.Subjects.Add(subject2);
                db.Subjects.Add(subject3);

                db.Grades.Add(grade1);
                db.Grades.Add(grade2);
                db.Grades.Add(grade3);

                db.SaveChanges();
            }
        }
    }
}