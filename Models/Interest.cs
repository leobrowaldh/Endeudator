using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeudator.Models
{
    internal class Interest
    {
        public int Id { get; set; }
        public int DebtId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public Debt Debt { get; set; }
    }
}
