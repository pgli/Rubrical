using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rubrical.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Rubrics = new List<Rubric>();
            Ratings = new List<Rating>();
        }

        public virtual List<Rubric> Rubrics { get; set; }

        public virtual List<Rating> Ratings { get; set; }
    }
}
