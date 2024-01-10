using Endeudator.Models;
using System.Security.Cryptography.X509Certificates;
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
                movement.Category = "Payment";
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
                movement.Category = "NewDebt";
                movement.Debt = debt;
                context.Movements.Add(movement);
                context.SaveChanges();
            }
        }

        public void UpdateInterests(DateTime date)
        {
            bool thereIsAnotherInterestToApply = true;
            do
            {
                using (Context context = new Context())
                {
                    //get last interest date: 
                    Interest? lastInterestEntry = context.Interests
                        .OrderByDescending(x => x.Date)
                        .FirstOrDefault();
                    if (lastInterestEntry == null)
                    {
                        Console.WriteLine("No interest entry in database");
                        throw new Exception("No interest entry in database");
                    }

                    DateTime lastInterestDate = lastInterestEntry.Date;
                    DateTime nextInterestDate = GetNextInterestDate(lastInterestDate);

                    if (nextInterestDate > date)
                    {
                        thereIsAnotherInterestToApply = false;
                        break; //this means we are up to date with the interests, nothing else to apply
                    }
                    else if (nextInterestDate <= date)
                    {
                        //Apply the interest
                        decimal interestRate = FetchCorrespondingRate(nextInterestDate);
                        decimal dailyInterestRate = interestRate / (decimal)30; //A month will have 30 days for the purpose of this application.
                        TimeSpan interestInterval = nextInterestDate - lastInterestDate;
                        int daysToApplyInterest = interestInterval.Days;

                        Debt? lastTotalDebt = context.Debts
                            .Where(x => x.Date == lastInterestDate)
                            .FirstOrDefault();

                        decimal amount = (lastTotalDebt?.TotalSEK ?? 0)*(1 + dailyInterestRate * daysToApplyInterest);

                        AddInterest(amount, nextInterestDate, interestRate);
                    }
                }
            }
            while (thereIsAnotherInterestToApply);
            
        }

        private static DateTime GetNextInterestDate(DateTime lastInterestDate)
        {
            DateTime nextInterestDate = DateTime.Now.AddDays(1);
            if (lastInterestDate.Day >= 25)
            {
                nextInterestDate = lastInterestDate.AddMonths(1);
                nextInterestDate = new DateTime(nextInterestDate.Year, nextInterestDate.Month, 25);
            }
            else if (lastInterestDate.Day < 25)
            {
                nextInterestDate = lastInterestDate;
                nextInterestDate = new DateTime(nextInterestDate.Year, nextInterestDate.Month, 25);
            }

            return nextInterestDate;
        }

        public void AddInterest(decimal amount, DateTime date, decimal interestRate)
        {
            using (Context context = new Context())
            {
                Debt debt = RecalculateDebt(amount, date, context);
                Interest interest = new Interest();

                interest.Amount = amount;
                interest.Date = date;
                interest.InterestRate = interestRate;
                interest.Debt = debt;
                context.Interests.Add(interest);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Create the initial state of the register
        /// </summary>
        public void AddInitialState(DateTime date, decimal initialDebt, decimal interestRate)
        {
            using (Context context = new Context())
            {

            }
        }
        
        public decimal FetchCorrespondingRate(DateTime date)
        {
            using (Context context = new Context())
            {
                CurrentRate? rate = context.CurrentRates
                    .Where(x => x.Date <= date)
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault();
                return rate.Rate;
            }
        }

        /// <summary>
        /// Adds the given amount to the total debt
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="context"></param>
        /// <returns></returns>
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

        public void PrintReport()
        {

        }

        public void ChangeInterestRate(decimal newInterestRate, DateTime startingAt)
        {
            using (Context context = new Context())
            {
                CurrentRate newRate = new CurrentRate() { Rate = newInterestRate, Date = startingAt };
                context.CurrentRates.Add(newRate);
                context.SaveChanges();
            }
        }

        
    }
}
