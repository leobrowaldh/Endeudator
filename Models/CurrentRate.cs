using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeudator.Models
{
    internal class CurrentRate
    {
        public int id {  get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
