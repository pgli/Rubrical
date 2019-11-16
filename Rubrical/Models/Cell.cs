// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Cell.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Cell object, holding a cell's text and affiliations.</summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Cell.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>The row identifier.</value>
        [Required]
        public int RowId { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        [ForeignKey("RowId")]
        [JsonIgnore]
        public virtual Row Row { get; set; }
    }
}
