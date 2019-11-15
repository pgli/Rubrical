// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RubricIndexViewModel.cs" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
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
