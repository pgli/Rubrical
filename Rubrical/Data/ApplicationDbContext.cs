// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="ApplicationDbContext.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Our database context, used to communicate db changes.</summary>
// ***********************************************************************
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rubrical.Models;

namespace Rubrical.Data
{
    /// <summary>
    /// Class ApplicationDbContext.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Rubrical.Models.ApplicationUser}" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Rubrical.Models.ApplicationUser}" />
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        /// <value>The application users.</value>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>The rubrics.</value>
        public DbSet<Rubric> Rubrics { get; set; }
        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>The subjects.</value>
        public DbSet<Subject> Subjects { get; set; }
        /// <summary>
        /// Gets or sets the grades.
        /// </summary>
        /// <value>The grades.</value>
        public DbSet<Grade> Grades { get; set; }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public DbSet<Row> Rows { get; set; }
        /// <summary>
        /// Gets or sets the cells.
        /// </summary>
        /// <value>The cells.</value>
        public DbSet<Cell> Cells { get; set; }
        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>The ratings.</value>
        public DbSet<Rating> Ratings { get; set; }
    }
}