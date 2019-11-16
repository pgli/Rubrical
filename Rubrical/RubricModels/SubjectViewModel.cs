// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="SubjectViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Used when storing Subject metadata.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class SubjectViewModel.
    /// </summary>
    public class SubjectViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        /// <value>The name of the subject.</value>
        public string SubjectName { get; set; }
        /// <summary>
        /// Gets or sets the subject description.
        /// </summary>
        /// <value>The subject description.</value>
        public string SubjectDescription { get; set; }

    }
}
