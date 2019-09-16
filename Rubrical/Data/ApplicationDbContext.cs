using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rubrical.Models;

namespace Rubrical.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }  
        public DbSet<Column> Columns { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Privacy> Privacies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}