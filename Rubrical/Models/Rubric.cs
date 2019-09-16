﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrical.Models
{
    public class Rubric
    {
        public Rubric()
        {
            Columns = new List<Column>();
            Ratings = new List<Rating>();
            DateCreated = DateTime.Now;
            TotalRating = 0;
            OwnerId = CreatedByUser.Id;
        }

        [Key]
        public int Id { get; set; }

        public int OwnerId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [Required]
        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        [Required]
        public int PrivacyId { get; set; }

        [ForeignKey("PrivacyId")]
        public virtual Privacy Privacy { get; set; }

        public DateTime DateCreated { get; set; }

        public int? TotalRating { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        [Required]
        public virtual List<Column> Columns { get; set; }

        [Required]
        public virtual List<Rating> Ratings { get; set; }
    }
}