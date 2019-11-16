// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="SetRatingModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A model used in determining the nature of a user's vote onto a rubric.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    /// <summary>
    /// Class SetRatingModel.
    /// </summary>
    public class SetRatingModel
    {
        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        public int RubricId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SetRatingModel"/> is value.
        /// </summary>
        /// <value><c>true</c> if value; otherwise, <c>false</c>.</value>
        public bool Value { get; set; }
    }
}
