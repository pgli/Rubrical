// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RatingViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Viewmodel used when accepting a vote and totalling a rubric's rating.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class RatingViewModel.
    /// </summary>
    public class RatingViewModel
    {
        /// <summary>
        /// Gets or sets the vote.
        /// </summary>
        /// <value>The vote.</value>
        public int Vote { get; set; }
        /// <summary>
        /// Gets or sets the total rating.
        /// </summary>
        /// <value>The total rating.</value>
        public int TotalRating { get; set; }
    }
}
