using Endeudator.Data;
using ConsoleCompanion;
using System.IdentityModel.Tokens.Jwt;

namespace Endeudator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            RunMenu(dataAccess);
        }

        private static void RunMenu(DataAccess dataAccess)
        {
            ConsoleCompanionHelper cc = new();
            int selectedOption = -1;
            do
            {
                selectedOption = cc.CreateMenu(new List<string> { "UPDATE Interests", "PAY off some of the debt", "REPORT print", "ADD DEBT to the debt", "CHANGE INTEREST rate" , "START a new Debt Register"},
            "Menu options, select with enter, ESC to Exit", ConsoleColor.Green, true);

                switch (selectedOption)
                {
                    case 0:
                        {
                            dataAccess.UpdateInterests(DateTime.Now);
                            break;
                        }
                    case 1:
                        {
                            PayMenuSelection(dataAccess, cc);
                            break;
                        }
                    case 2:
                        {
                            //PrintReportMenuSelection();
                            break;
                        }
                    case 32:
                        {
                            AddDebtMenuSelection(dataAccess, cc);
                            break;
                        }
                    case 4:
                        {
                            ChangeInterestMenu(dataAccess, cc);
                            break;
                        }
                    case 5:
                        {

                            break;
                        }
                }
            }
            while (selectedOption != -1);
        }

        private static void AddDebtMenuSelection(DataAccess dataAccess, ConsoleCompanionHelper cc)
        {
            double amount = cc.AskForDouble("How much is the new debt?", "wrong input");
            string stringDate = cc.AskForString("When was this debt taken? yyyy-mm-dd HH:mm , \n type \"NOW\" if it starts now.");
            if (stringDate.ToUpper() == "NOW")
            {
                DateTime date = DateTime.Now;
                dataAccess.NewDebt((decimal)amount, date);
                Console.WriteLine("\n--New debt set--\n");
            }
            else if (DateTime.TryParse(stringDate, out DateTime parsedDate))
            {
                dataAccess.NewDebt((decimal)amount, parsedDate);
                Console.WriteLine("\n--New debt set--\n");
            }
            else { Console.WriteLine("wrong input"); }
        }

        private static void PayMenuSelection(DataAccess dataAccess, ConsoleCompanionHelper cc)
        {
            double amount = cc.AskForDouble("How much are you Paying?", "wrong input");
            string stringDate = cc.AskForString("When did this payment occur? yyyy-mm-dd HH:mm , \n type \"NOW\" if paying now.");
            if (stringDate.ToUpper() == "NOW")
            {
                DateTime date = DateTime.Now;
                dataAccess.PayDebt((decimal)amount, date);
                Console.WriteLine("\n--Payment made--\n");
            }
            else if (DateTime.TryParse(stringDate, out DateTime parsedDate))
            {
                dataAccess.PayDebt((decimal)amount, parsedDate);
                Console.WriteLine("\n--Payment made--\n");
            }
            else { Console.WriteLine("wrong input"); }
        }

        private static void ChangeInterestMenu(DataAccess dataAccess, ConsoleCompanionHelper cc)
        {
            double newInterest = cc.AskForDouble("What is the new interest rate you want to set?", "wrong input");
            string stringDate = cc.AskForString("When should the new interest start to apply? yyyy-mm-dd HH:mm , \n type \"NOW\" if now.");
            if (stringDate.ToUpper() == "NOW")
            {
                DateTime date = DateTime.Now;
                dataAccess.ChangeInterestRate((decimal)newInterest, date);
                Console.WriteLine("\n--Interest updated--\n");
            }
            else if (DateTime.TryParse(stringDate, out DateTime parsedDate))
            {
                dataAccess.ChangeInterestRate((decimal)newInterest, parsedDate);
                Console.WriteLine("\n--Interest updated--\n");
            }
            else { Console.WriteLine("wrong input"); }
        }
    }

}