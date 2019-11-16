// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Grade.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Grade object, containing a grade's name and/or number. Also allows a description. Used for Rubric categorization.</summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Grade.
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int? Number { get; set; }

        /// <summary>
        /// The grade name
        /// </summary>
        private string _gradeName;
        /// <summary>
        /// Gets or sets the name of the grade.
        /// </summary>
        /// <value>The name of the grade.</value>
        public string GradeName
        {
            get
            {
                if (this.Number == null || this.Number == 0)
                {
                    return _gradeName;
                }
                else
                {
                    return string.Format("Grade {0}", this.Number);
                }

            }
            set { _gradeName = value; }
        }

        /// <summary>
        /// Gets or sets the grade description.
        /// </summary>
        /// <value>The grade description.</value>
        public string GradeDescription { get; set; }
    }
}