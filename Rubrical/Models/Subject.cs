// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Subject.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Subject object, with a name and description. Used for Rubric categorization.</summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Subject.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        /// <value>The name of the subject.</value>
        [Required]
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the subject description.
        /// </summary>
        /// <value>The subject description.</value>
        public string SubjectDescription { get; set; }
    }
}