// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RubricDeleteViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Viewmodel used when deleting a Rubric.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class RubricDeleteViewModel.
    /// </summary>
    public class RubricDeleteViewModel
    {
        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        public int RubricId { get; set; }
    }
}
