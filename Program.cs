using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CalculatorLibrary;


namespace CalculatorProgram
{
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Calculator calculator = new Calculator();
        ArrayList information = new ArrayList();

        int counter = 0;  
        //Title
        System.Console.WriteLine("Console calculator in c# \r");
        System.Console.WriteLine("--------------------------\n");

    while(!endApp)
    {
        counter++;
        //Declare variables and set to empty. 
        string? numInput1 = "";
        string? numInput2 = "";
        double result = 0;


        System.Console.WriteLine("Type in a number then press ENTER: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while(!double.TryParse(numInput1, out cleanNum1))
        {
            System.Console.WriteLine("This is not a valid input. please try again");
            numInput1 = Console.ReadLine();
        }

        System.Console.WriteLine("Type in a second number then press ENTER: ");
        numInput2 = Console.ReadLine();

        double cleanNum2 = 0;
        while(!double.TryParse(numInput2, out cleanNum2))
        {
            System.Console.WriteLine("This is not a valid input. please try again");
            numInput2 = Console.ReadLine();
        }

        //Ask user to choose a operator
        System.Console.WriteLine("Choose an operator from the following list: ");
        System.Console.WriteLine("\ta - Add");
        System.Console.WriteLine("\ts - Subtract");
        System.Console.WriteLine("\tm - Multiply");
        System.Console.WriteLine("\td - Divide");
        Console.WriteLine("Your option please? : ");

        string? op = Console.ReadLine();

        //Validate input is not null and matches the pattern
        if(op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
        {
            System.Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                information.Add(result);

                if(double.IsNaN(result))
                {
                    System.Console.WriteLine("This operation will result in a mathematical ERROR!");
                }
                else
                {
                    System.Console.WriteLine("Your result: {0:0.##}\n", result);
                }
            }
                catch (Exception e)
                {
                    System.Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            System.Console.WriteLine("--------------------------------\n");

            //Ask user if they would like to view or remove results from the arraylist or exit the application
            System.Console.WriteLine("Would you like to delete the results stored in the databse?");
            System.Console.WriteLine("Type: Y - yes || N - no");

            string answer = Console.ReadLine().ToUpper();
            try{ 
            if(answer == "Y")
            {
                calculator.PrintResults();
                System.Console.WriteLine("Now deleting results....");
                calculator.DeleteResults();
                System.Console.WriteLine("Deletion Successful!");
            }
            else if(answer == "N")
            {
                System.Console.WriteLine("Would you like to preview the results in the database and perform new calculations?");
                System.Console.WriteLine("Type: Y - yes || N - no");
                if(Console.ReadLine().ToUpper() == "Y")
                        {
                            calculator.PrintResults();
                            extraFeatures(calculator, information);
                        }
                    }
            }
            catch (InvalidOperationException e)
            {
                System.Console.WriteLine("" + e);
            }
          

            System.Console.WriteLine("Press 'n' and enter to close to app or press any other key and enter to continue");

            if(Console.ReadLine() == "n")
            {
                endApp = true;
                System.Console.WriteLine($"The amount of times the calculator was used was {counter}");
            }
            System.Console.WriteLine("\n");
    }
        calculator.Finish();
        return;

            static void extraFeatures(Calculator calculator, ArrayList information)
            {
                System.Console.WriteLine("Choose an operator from the following list to perform: ");
                System.Console.WriteLine("\tm - Power^10");
                System.Console.WriteLine("\tq - SquareRoot");

                string? opTwo = Console.ReadLine();
                
                if (opTwo.ToLower() == "m")
                {
                    System.Console.WriteLine("Now choose which previous result you would like to perform these calculations on? ");
                    calculator.PrintResults();

                    string userInputOne = Console.ReadLine();
                    int userInputTwo;

                    while (!int.TryParse(userInputOne, out userInputTwo))
                    {
                        System.Console.WriteLine("Please enter a number!");
                        userInputOne = Console.ReadLine();
                    }
                    userInputTwo = userInputTwo - 1;
                    int powerValue = Convert.ToInt32(information[userInputTwo]);
                    calculator.PowerOperation(powerValue);
                }
                else if (opTwo.ToLower() == "q")
                {
                    System.Console.WriteLine("Now choose which previous result you would like to perform these calculations on? ");
                    calculator.PrintResults();

                    string userInputThree = Console.ReadLine();
                    int userInputFour;

                    while (!int.TryParse(userInputThree, out userInputFour))
                    {
                        System.Console.WriteLine("Please enter a number!");
                        userInputThree = Console.ReadLine();
                    }
                    userInputFour = userInputFour - 1;
                    int sqrValue = Convert.ToInt32(information[userInputFour]);
                    calculator.SquareOperation(sqrValue);
                }
            }
        }
    }
}



