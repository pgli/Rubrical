// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="ApplicationUser.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Custom user class, inherited from the ASP IdentityUser system</summary>
// ***********************************************************************
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rubrical.Models
{
    /// <summary>
    /// Class ApplicationUser.
    /// Implements the <see cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        /// <remarks>The Id property is initialized to form a new GUID string value.</remarks>
        public ApplicationUser()
        {
            Rubrics = new List<Rubric>();
            Ratings = new List<Rating>();
        }

        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>The rubrics.</value>
        public virtual List<Rubric> Rubrics { get; set; }

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>The ratings.</value>
        public virtual List<Rating> Ratings { get; set; }
    }
}
