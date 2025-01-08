using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsleUI
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine($"Welcome to our Garage!{Environment.NewLine}Please choose an option:{Environment.NewLine}1. Insert a car "
                              + $"{Environment.NewLine}2. Print license numbers{Environment.NewLine}3. Change vehicle status"
                              + $"{Environment.NewLine}4. Inflate wheels{Environment.NewLine}5. Refuel{Environment.NewLine}6. Recharge"
                              + $"{Environment.NewLine}7. Print vehicle details"); 
            string customerChoice = Console.ReadLine();
            while(!validateNumber(customerChoice))
            {
                Console.WriteLine("Invalid choice please try again.");
                customerChoice = Console.ReadLine();
            }

            
            switch(StringToChoice(customerChoice))
            {
                case eChoice.Insert:


                    break;
                case eChoice.PrintLicenses:
                    break;
                case eChoice.ChangeStatus:
                    break;
                case eChoice.Inflate:
                    break;
                case eChoice.Refuel:
                    break;
                case eChoice.Recharge:
                    break;
                case eChoice.PrintDetails:
                    break;
            }
        }

        private static bool validateNumber(string i_ChosenNumber)
        {
            bool isValidated = false;
            if(int.TryParse(i_ChosenNumber, out int result))
            {
                if(result < 8 && result > 0)
                {
                    isValidated = true;
                }
            }

            return isValidated;
        }

        private static eChoice? StringToChoice(string i_CustomerChoice)
        {
            var choicesMap = new Dictionary<string, eChoice>
                                 {
                                     { "1", eChoice.Insert },
                                     { "2", eChoice.PrintLicenses },
                                     { "3", eChoice.ChangeStatus },
                                     { "4", eChoice.Inflate },
                                     { "5", eChoice.Refuel },
                                     { "6", eChoice.Recharge },
                                     { "7", eChoice.PrintDetails }
                                 };

            return choicesMap.TryGetValue(i_CustomerChoice, out var choice) ? choice : (eChoice?)null;
        }
    }
}

