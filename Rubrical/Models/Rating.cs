// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Rating.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Rating object, used for the upvote and downvote system when viewing a Rubric.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Rating.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        [Required]
        public int RubricId { get; set; }

        /// <summary>
        /// Gets or sets the rubric.
        /// </summary>
        /// <value>The rubric.</value>
        [ForeignKey("RubricId")]
        public virtual Rubric Rubric { get; set; }

        /// <summary>
        /// Gets or sets the application user identifier.
        /// </summary>
        /// <value>The application user identifier.</value>
        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the rated by user.
        /// </summary>
        /// <value>The rated by user.</value>
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser RatedByUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rating"/> is value.
        /// </summary>
        /// <value><c>true</c> if value; otherwise, <c>false</c>.</value>
        [Required]
        public bool Value { get; set; }
    }
}
