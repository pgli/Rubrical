// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Rubric.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Rubric object, which holds all of a rubric's metadata used throughout the site.</summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Rubric.
    /// </summary>
    public class Rubric
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rubric"/> class.
        /// </summary>
        public Rubric()
        {
            Rows = new List<Row>();
            Ratings = new List<Rating>();
            DateCreated = DateTime.Now;
            TotalRating = 0;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        [Required]
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [JsonIgnore]
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        [Required]
        public int GradeId { get; set; }

        /// <summary>
        /// Gets or sets the grade.
        /// </summary>
        /// <value>The grade.</value>
        [JsonIgnore]
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the total rating.
        /// </summary>
        /// <value>The total rating.</value>
        public int? TotalRating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the application user identifier.
        /// </summary>
        /// <value>The application user identifier.</value>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        /// <value>The created by user.</value>
        [JsonIgnore]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        [JsonIgnore]
        public virtual List<Row> Rows { get; set; }

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>The ratings.</value>
        [JsonIgnore]
        public virtual List<Rating> Ratings { get; set; }
    }
}