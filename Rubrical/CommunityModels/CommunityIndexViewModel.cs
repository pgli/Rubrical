// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="CommunityIndexViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Used to load Rubrics on the Community Rubrics page</summary>
// ***********************************************************************
using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.CommunityModels
{
    /// <summary>
    /// Class CommunityIndexViewModel.
    /// </summary>
    public class CommunityIndexViewModel
    {
        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>The rubrics.</value>
        public List<Rubric> Rubrics { get; set; }
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
        /// <summary>
        /// Gets or sets the filter subject identifier.
        /// </summary>
        /// <value>The filter subject identifier.</value>
        public int? FilterSubjectId { get; set; }
        /// <summary>
        /// Gets or sets the filter grade identifier.
        /// </summary>
        /// <value>The filter grade identifier.</value>
        public int? FilterGradeId { get; set; }
        /// <summary>
        /// Gets or sets the type of the sort.
        /// </summary>
        /// <value>The type of the sort.</value>
        public int SortType { get; set; }
    }
}
