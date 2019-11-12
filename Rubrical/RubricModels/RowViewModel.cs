using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    public class RowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cell> Cells { get; set; }
    }
}
