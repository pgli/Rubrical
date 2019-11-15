// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Subject.cs" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
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