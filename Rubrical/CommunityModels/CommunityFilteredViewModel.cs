// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="CommunityFilteredViewModel.cs" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.CommunityModels
{
    /// <summary>
    /// Class CommunityFilteredViewModel.
    /// </summary>
    public class CommunityFilteredViewModel
    {
        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        public int? RubricId { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public Subject Subject { get; set; }
        /// <summary>
        /// Gets or sets the grade.
        /// </summary>
        /// <value>The grade.</value>
        public Grade Grade { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Gets or sets the total rating.
        /// </summary>
        /// <value>The total rating.</value>
        public int? TotalRating { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}
