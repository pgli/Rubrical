// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="Row.cs" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>A Row object, which contains its affiliated rubric and a list of cells.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    /// <summary>
    /// Class Row.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        public Row()
        {
            Cells = new List<Cell>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the cells.
        /// </summary>
        /// <value>The cells.</value>
        public virtual List<Cell> Cells { get; set; }

        /// <summary>
        /// Gets or sets the rubric identifier.
        /// </summary>
        /// <value>The rubric identifier.</value>
        [Required]
        public int RubricId { get; set; }

        /// <summary>
        /// Gets or sets the rubric.
        /// </summary>
        /// <value>The rubric.</value>
        [ForeignKey("RubricId")]
        public virtual Rubric Rubric { get; set; }
    }
}
