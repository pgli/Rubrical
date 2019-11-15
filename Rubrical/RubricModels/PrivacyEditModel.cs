// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="PrivacyEditModel.cs" company="Rubrical">
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
    /// Class PrivacyEditModel.
    /// </summary>
    public class PrivacyEditModel
    {
        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        public int RubricId { get; set; }
        /// <summary>
        /// Gets or sets the selected privacy.
        /// </summary>
        /// <value>The selected privacy.</value>
        public int SelectedPrivacy { get; set; }
    }
}
