using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        public int? Number { get; set; }

        private string _gradeName;
        public string GradeName
        {
            get
            {
                if (this.Number == null || this.Number == 0)
                {
                    return _gradeName;
                }
                else
                {
                    return string.Format("Grade {0}", this.Number);
                }

            }
            set { _gradeName = value; }
        }

        public string GradeDescription { get; set; }
    }
}