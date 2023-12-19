using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeudator.Models
{
    internal class Movement
    {
        public int Id { get; set; }
        public int DebtId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        private string _category;
        public string Category 
        {
            get => _category;
            set
            {
                if (Enum.TryParse<enCategory>(value, out _))
                {
                    _category = value;
                }
            }
        }
        public Debt Debt { get; set; }
    }

    enum enCategory { Payment, NewDebt}
}
