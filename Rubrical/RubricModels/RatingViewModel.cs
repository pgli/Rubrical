// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="RatingViewModel.cs" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
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
