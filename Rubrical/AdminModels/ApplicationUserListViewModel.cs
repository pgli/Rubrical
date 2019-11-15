using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.AdminModels
{
    public class ApplicationUserListViewModel
    {
        public List<ApplicationUserViewModel> Users { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
