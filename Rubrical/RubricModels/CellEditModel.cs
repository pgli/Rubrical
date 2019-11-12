using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    public class CellEditModel
    {
        public int RubricId { get; set; }
        public int RowId { get; set; }
        public int CellId { get; set; }
        public string Text { get; set; }
    }
}
