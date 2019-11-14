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
            var subject4 = new Subject { SubjectName = "Computer Science", SubjectDescription = "The study of computer science" };
            var subject5 = new Subject { SubjectName = "Mathematics", SubjectDescription = "The study of mathematics" };
            var subject6 = new Subject { SubjectName = "Geography", SubjectDescription = "The study of geography" };
            var subject7 = new Subject { SubjectName = "History", SubjectDescription = "The study of history" };
            var subject8 = new Subject { SubjectName = "Art", SubjectDescription = "The study of art" };
            var subject9 = new Subject { SubjectName = "Music", SubjectDescription = "The study of music" };
            var subject10 = new Subject { SubjectName = "Gym", SubjectDescription = "The study of gym" };
            var subject11 = new Subject { SubjectName = "Drama", SubjectDescription = "The study of drama" };
            var subject12 = new Subject { SubjectName = "English", SubjectDescription = "The study of English" };
            var subject13 = new Subject { SubjectName = "French", SubjectDescription = "The study of French" };

            var grade1 = new Grade { Number = 1, GradeDescription = "Grade 1 - elementary school." };
            var grade2 = new Grade { Number = 2, GradeDescription = "Grade 2 - elementary school." };
            var grade3 = new Grade { Number = 3, GradeDescription = "Grade 3 - elementary school." };
            var grade4 = new Grade { Number = 4, GradeDescription = "Grade 4 - elementary school." };
            var grade5 = new Grade { Number = 5, GradeDescription = "Grade 5 - elementary school." };
            var grade6 = new Grade { Number = 6, GradeDescription = "Grade 6 - middle school." };
            var grade7 = new Grade { Number = 7, GradeDescription = "Grade 7 - middle school." };
            var grade8 = new Grade { Number = 8, GradeDescription = "Grade 8 - middle school." };
            var grade9 = new Grade { Number = 9, GradeDescription = "Grade 9 - highschool." };
            var grade10 = new Grade { Number = 10, GradeDescription = "Grade 10 - highschool." };
            var grade11 = new Grade { Number = 11, GradeDescription = "Grade 11 - highschool." };
            var grade12 = new Grade { Number = 12, GradeDescription = "Grade 12 - highschool." };
            var gradeExtra1 = new Grade { GradeName = "University", GradeDescription = "A university, such as McMaster." };
            var gradeExtra2 = new Grade { GradeName = "College", GradeDescription = "A college, such as Mohawk." };
            var gradeExtra3 = new Grade { GradeName = "Other" };
            #endregion

            if (!(db.Subjects.Where(x => x.SubjectName == subject1.SubjectName).Any()))
            {
                db.Subjects.Add(subject1);
                db.Subjects.Add(subject2);
                db.Subjects.Add(subject3);
                db.Subjects.Add(subject4);
                db.Subjects.Add(subject5);
                db.Subjects.Add(subject6);
                db.Subjects.Add(subject7);
                db.Subjects.Add(subject8);
                db.Subjects.Add(subject9);
                db.Subjects.Add(subject10);
                db.Subjects.Add(subject11);
                db.Subjects.Add(subject12);
                db.Subjects.Add(subject13);

                db.Grades.Add(grade1);
                db.Grades.Add(grade2);
                db.Grades.Add(grade3);
                db.Grades.Add(grade4);
                db.Grades.Add(grade5);
                db.Grades.Add(grade6);
                db.Grades.Add(grade7);
                db.Grades.Add(grade8);
                db.Grades.Add(grade9);
                db.Grades.Add(grade10);
                db.Grades.Add(grade11);
                db.Grades.Add(grade12);
                db.Grades.Add(gradeExtra1);
                db.Grades.Add(gradeExtra2);
                db.Grades.Add(gradeExtra3);

                db.SaveChanges();
            }
        }
    }
}