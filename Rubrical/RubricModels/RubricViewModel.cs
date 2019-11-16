// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RubricViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Viewmodel used when populating Subjects and Grades in the Create Rubric form.</summary>
// ***********************************************************************
using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class RubricViewModel.
    /// </summary>
    public class RubricViewModel
    {
        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>The subjects.</value>
        public List<Subject> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the grades.
        /// </summary>
        /// <value>The grades.</value>
        public List<Grade> Grades { get; set; }
    }
}
