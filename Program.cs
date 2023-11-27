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
            dataAccess.UpdateInterests();
            RunMenu(dataAccess);
        }

        private static void RunMenu(DataAccess dataAccess)
        {
            ConsoleCompanionHelper cc = new();
            int selectedOption = -1;
            do
            {
                selectedOption = cc.CreateMenu(new List<string> { "PAY off some of the debt", "REPORT print", "ADD DEBT to the debt" },
            "Menu options, select with enter, ESC to Exit", ConsoleColor.Green, true);

                switch (selectedOption)
                {
                    case 0:
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
                            break;
                        }
                    case 1:
                        {
                            PrintReport();
                            break;
                        }
                    case 2:
                        {
                            double amount = cc.AskForDouble("How is the new debt?", "wrong input");
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
                            break;
                        }
                }
            }
            while (selectedOption != -1);
        }
    }
    }

    //chatgpt, using Windows Task Scheduler
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        if (args.Length > 0 && args[0] == "scheduled")
    //        {
    //            // This branch is executed when the application is triggered by the Task Scheduler
    //            RunScheduledUpdate();
    //        }
    //        else
    //        {
    //            // This branch is executed when the application is manually run in the console
    //            Console.WriteLine("Manual run - Press Enter to exit.");
    //            Console.ReadLine();
    //        }
    //    }

    //    static void RunScheduledUpdate()
    //    {
    //        // Implement the logic for automatic updates here
    //        Console.WriteLine("Running scheduled update...");
    //        ApplyInterest();
    //    }

    //    static void ApplyInterest()
    //    {
    //        // Your existing logic for applying interest
    //        // ...

    //        Console.WriteLine("Interest applied!");
    //    }
    //}
}