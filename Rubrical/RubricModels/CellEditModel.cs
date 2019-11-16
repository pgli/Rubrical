// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="CellEditModel.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Viewmodel used when editing a cell's text.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    /// <summary>
    /// Class CellEditModel.
    /// </summary>
    public class CellEditModel
    {
        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        public int RubricId { get; set; }
        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>The row identifier.</value>
        public int RowId { get; set; }
        /// <summary>
        /// Gets or sets the cell identifier.
        /// </summary>
        /// <value>The cell identifier.</value>
        public int CellId { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
    }
}
