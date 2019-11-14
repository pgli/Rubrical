using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.CommunityModels
{
    public class CommunityFilteredViewModel
    {
        public int? RubricId { get; set; }
        public Subject Subject { get; set; }
        public Grade Grade { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? TotalRating { get; set; }
        public string UserName { get; set; }
    }
}
