using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.CommunityModels
{
    public class CommunityIndexViewModel
    {
        public List<Rubric> Rubrics { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Grade> Grades { get; set; }
        public int? FilterSubjectId { get; set; }
        public int? FilterGradeId { get; set; }
        public int SortType { get; set; }
    }
}
