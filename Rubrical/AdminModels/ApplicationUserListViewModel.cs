// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="ApplicationUserListViewModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Holds a list of ApplicationUserViewModels, created to add subject information to them.</summary>
// ***********************************************************************
using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.AdminModels
{
    /// <summary>
    /// Class ApplicationUserListViewModel.
    /// </summary>
    public class ApplicationUserListViewModel
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public List<ApplicationUserViewModel> Users { get; set; }
        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>The subjects.</value>
        public List<Subject> Subjects { get; set; }
    }
}
