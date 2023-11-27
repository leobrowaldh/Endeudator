using Endeudator.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Endeudator.Data
{
    internal class DataAccess
    {
        

        public void PayDebt(decimal amount, DateTime date)
        {
            using (Context context = new Context())
            {
                Debt debt = RecalculateDebt(-amount, date, context);

                Movement movement = new Movement();
                movement.Amount = -amount;
                movement.Date = date;
                movement.Category = "payment";
                movement.Debt = debt;
                context.Movements.Add(movement);
                context.SaveChanges();
            }
        }

        public void NewDebt(decimal amount, DateTime date)
        {
            using (Context context = new Context())
            {
                Debt debt = RecalculateDebt(amount, date, context);

                Movement movement = new Movement();
                movement.Amount = amount;
                movement.Date = date;
                movement.Category = "new debt";
                movement.Debt = debt;
                context.Movements.Add(movement);
                context.SaveChanges();
            }
        }

        private static Debt RecalculateDebt(decimal amount, DateTime date, Context context)
        {
            Debt debt = new Debt();
            Debt? previousDebtEvent = context.Debts
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

            debt.TotalSEK = (previousDebtEvent?.TotalSEK ?? 0) + amount;

            debt.Date = date;
            return debt;
        }

        private void PrintReport()
        {

        }
    }
}
