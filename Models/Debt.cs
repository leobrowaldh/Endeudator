using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeudator.Models
{
    internal class Debt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSEK { get; set; }
    }
}
