// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RubricIndexViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Viewmodel used to load in rubrics under My Rubrics.</summary>
// ***********************************************************************
using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class RubricIndexViewModel.
    /// </summary>
    public class RubricIndexViewModel
    {
        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>The rubrics.</value>
        public List<Rubric> Rubrics { get; set; }
    }
}
