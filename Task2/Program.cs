using System;
using System.Collections.Generic;

// abstract class to hold public abstracts
abstract class Expenses
{
    public abstract double MonthlyExpenses();
    public abstract double Purchasing();
    public abstract double Renting();
    public abstract double Vehicle();
    public abstract double Balance();
}

//class MonthlyExpenses to hold expenses and also a Get and Set Method
class MonthlyExpenses
{
    public double Income
    { get; set; }

    public double TaxDeduct
    { get; set; }

    public double Groceries
    { get; set; }

    public double LightsAndWater
    { get; set; }

    public double TravelCosts
    { get; set; }

    public double CellPhone
    { get; set; }

    public double Miscellaneous
    { get; set; }
}

//class Purchasing to hold Get and Set method when user chooses to buy a property
class Purchasing
{

    public double PurchasePrice
    { get; set; }

    public double Deposit
    { get; set; }

    public double IntRate
    { get; set; }

    public double RepayMonths
    { get; set; }

    public double RepaymentAmount
    { get; set; }
}

//Class renting to hold rentExpense get and set method
class Renting
{
    public double RentExpense
    { get; set; }
}

//Class vehicle to hold Get and Set methods used in Vehicle repayment
class Vehicle
{
    public String MakeAndModel
    { get; set; }

    public double VehiclePurchasePrice
    { get; set; }

    public double VehicleDeposit
    { get; set; }

    public double VehicleIntRate
    { get; set; }

    public double InsurancePremium
    { get; set; }

    public double MonthlyRepaymentIncInsurance
    { get; set; }
}

//Class Balance to hold Get and Set methods used to calculate remaining balances
class Balance
{
    public double avaliableMoney
    { get; set; }

    public double avaliableMoneyIncRent
    { get; set; }
}

//Declare Delegate
delegate string AlertMsg(String alert);

namespace Task2
{

    //Class delegate used for error Message
    class DelegateMsg
    {
        public static String ErrorMessage(String Message)
        {
            Message = "Your expenses have exceeded 75% of Your Income";
            return Message;
        }

        class Program
        {
            private static int exitCode;

            static void Main(string[] args)
            {

                //classes are brought into the main
                MonthlyExpenses myExpenses = new MonthlyExpenses();
                Purchasing myProperty = new Purchasing();
                Renting myRent = new Renting();
                Vehicle myVehicle = new Vehicle();
                Balance myBalance = new Balance();

                //Delegate class declared in main
                AlertMsg DelMsg = new AlertMsg(ErrorMessage);

                //Generic collection to store the expenses 
                List<double> ExpenseList = new List<double>
            {
                myExpenses.Income,
                myExpenses.TaxDeduct,
                myExpenses.Groceries,
                myExpenses.LightsAndWater,
                myExpenses.TravelCosts,
                myExpenses.CellPhone,
                myExpenses.Miscellaneous,
                myProperty.RepaymentAmount,
                myRent.RentExpense,
                myVehicle.MonthlyRepaymentIncInsurance
            };
                //'Reverse' used to display by Descending order
                ExpenseList.Sort();
                ExpenseList.Reverse();

                //declared Sum for Array
                double sum = 0;
                int i;

                Console.WriteLine("Good Day\n");

                //Try and catch for exception in the case a value is incorretly inputted
                try
                {
                    //code to ask user for monthly income
                    Console.Write("Enter your monthly income (Before Deductions) -- ");
                    myExpenses.Income = Double.Parse(Console.ReadLine());

                    //code to ask user for Tax decution
                    Console.Write("Enter your tax deductions -- ");
                    myExpenses.TaxDeduct = double.Parse(Console.ReadLine());

                    //code to ask user for grocery expense
                    Console.Write("Enter your grocery expenses -- ");
                    myExpenses.Groceries = double.Parse(Console.ReadLine());

                    //code to ask user for lights and water
                    Console.Write("Enter your Lights & Water expenses -- ");
                    myExpenses.LightsAndWater = double.Parse(Console.ReadLine());

                    //code to ask user for travel cost
                    Console.Write("Enter your Travel costs -- ");
                    myExpenses.TravelCosts = double.Parse(Console.ReadLine());

                    //code to ask user for cell phone expense
                    Console.Write("Enter your Cell Phone and Telephone expenses -- ");
                    myExpenses.CellPhone = double.Parse(Console.ReadLine());

                    //code to ask user for all Miscellaneous expenses
                    Console.Write("Enter all Miscellaneous expenses -- ");
                    myExpenses.Miscellaneous = double.Parse(Console.ReadLine());

                    Console.Write("\n");
                }

                //Catch with error message
                catch (Exception)
                {
                    Console.WriteLine("Please Enter A Number!!!");
                    Environment.Exit(exitCode);
                }

                //Code that askes user if they will purchase or rent a property 
                Console.Write("Will you be Purchasing or Renting a property? ");
                String choice = Console.ReadLine();
                string upperChoice = choice.ToUpper();

                //Gross income calculation
                double grossIncome = myExpenses.Income - myExpenses.TaxDeduct - myExpenses.Groceries - myExpenses.LightsAndWater - myExpenses.TravelCosts - myExpenses.CellPhone - myExpenses.Miscellaneous;

                //If statement that prompts different outputs depending on what user inputs 
                if (upperChoice == "PURCHASING")
                {
                    //Try and catch for exception in the case a value is incorretly inputted
                    try
                    {

                        Console.Write("\n");

                        //code that asks the user for the property price
                        Console.Write("Please enter Purchase Price of the Property -- ");
                        myProperty.PurchasePrice = double.Parse(Console.ReadLine());

                        //code that asks the user for the deposit amount
                        Console.Write("Please enter the Deposit Amount -- ");
                        myProperty.Deposit = double.Parse(Console.ReadLine());

                        //code that asks the user for the interest rate
                        Console.Write("Please enter the Interest rate -- ");
                        myProperty.IntRate = double.Parse(Console.ReadLine());

                        //code that asks the user for the repayment period 
                        Console.Write("Please enter the Repayment Period in Months (between 240 - 360) -- ");
                        myProperty.RepayMonths = double.Parse(Console.ReadLine());
                    }

                    //Catch with error message
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a value!!!");
                        Environment.Exit(exitCode);
                    }

                    //Repayment amount calculation using Simple Interest
                    double paymentExlDepo = myProperty.PurchasePrice - myProperty.Deposit;
                    double totalCost = paymentExlDepo * (1 + (myProperty.IntRate / 100) * (myProperty.RepayMonths / 12));
                    myProperty.RepaymentAmount = totalCost / myProperty.RepayMonths;

                    //Remaning money calculation
                    myBalance.avaliableMoney = grossIncome - myProperty.RepaymentAmount;

                    //If statement that determines if the users home loan will be approved 
                    if (myProperty.RepaymentAmount > grossIncome / 3)
                    {
                        Console.WriteLine("\nYour Home Loan Approval is Unlinkey. Reason - Monthly Repayment is more than a third of your Gross Income");
                        Console.WriteLine("Your Gross Income Excluding home loan is -- {0}", Math.Round(grossIncome, 2));
                    }
                    else
                    {
                        //Adds all these values to the array
                        //To add all values to an array including monthly repayement for purchase property and excluding rent expense
                        double[] allMonthlyExpenses = { myExpenses.TaxDeduct - myExpenses.Groceries, myExpenses.LightsAndWater, myExpenses.TravelCosts, myExpenses.CellPhone, myExpenses.Miscellaneous, myProperty.RepaymentAmount };

                        for (i = 0; i < allMonthlyExpenses.Length; i++)
                        {
                            sum += allMonthlyExpenses[i];
                        }

                        //outputs remaining money 
                        Console.WriteLine("\nYour Monthly repayments is -- {0}", Math.Round(myProperty.RepaymentAmount, 2));
                        Console.WriteLine("Your Avaliable balance is -- {0}", Math.Round(myBalance.avaliableMoney, 2));
                    }
                }
                //if statement that displays when user chooses renting 
                if (upperChoice == "RENTING")
                {
                    //Try and catch for exception in the case a value is incorretly inputted
                    try
                    {
                        Console.Write("Please enter Rent expense -- ");
                        myRent.RentExpense = double.Parse(Console.ReadLine());
                    }
                    //Catch with error message
                    catch (Exception)
                    {
                        Console.WriteLine("Please Enter A Value");
                        Environment.Exit(exitCode);
                    }

                    // avaliable money calculation
                    myBalance.avaliableMoneyIncRent = grossIncome - myRent.RentExpense;

                    //Array to hold values including rent expense excluding purchase property calculations
                    double[] allMonthlyExpenses = { myExpenses.TaxDeduct - myExpenses.Groceries, myExpenses.LightsAndWater, myExpenses.TravelCosts, myExpenses.CellPhone, myExpenses.Miscellaneous, myRent.RentExpense };

                    for (i = 0; i < allMonthlyExpenses.Length; i++)
                    {
                        sum += allMonthlyExpenses[i];
                    }

                    //Avaliable money output rounded to 2 decimal places
                    Console.WriteLine("\nYour Avaliable balance is -- {0}", Math.Round(myBalance.avaliableMoneyIncRent, 2));
                }

                //User will be asked if they will be purchasing a car or not
                Console.Write("\nWill be purchasing a Car? Please enter 'Yes' or 'No' -- ");
                choice = Console.ReadLine();
                upperChoice = choice.ToUpper();

                //if statement that will output if user chooses yes
                if (upperChoice == "YES")
                {
                    Console.Write("\nPlease enter the Make and Model of the vehicle -- ");
                    myVehicle.MakeAndModel = Console.ReadLine();

                    //Try and catch for exception in the case a value is incorretly inputted
                    try
                    {
                        Console.Write("Please enter the Vehicle purchase price -- ");
                        myVehicle.VehiclePurchasePrice = double.Parse(Console.ReadLine());

                        Console.Write("Please enter the Total Deposit Amount -- ");
                        myVehicle.VehicleDeposit = double.Parse(Console.ReadLine());

                        Console.Write("Please enter the Interest rate -- ");
                        myVehicle.VehicleIntRate = double.Parse(Console.ReadLine());

                        Console.Write("Please enter the Estimated insurance premium -- ");
                        myVehicle.InsurancePremium = double.Parse(Console.ReadLine());


                    }
                    //Catch with error message
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter a value!!!");
                        Environment.Exit(exitCode);
                    }
                }

                else
                {
                    //output message to show Avaliable balance 
                    Console.WriteLine("Your Avaliable balance at the end is {0}", Math.Round(myBalance.avaliableMoneyIncRent + myBalance.avaliableMoney, 2));
                }

                    //Calculations for monthly repayment
                    double totalPayment = (myVehicle.VehiclePurchasePrice - myVehicle.VehicleDeposit) * (1 + (myVehicle.VehicleIntRate / 100) * 5);
                    double totalMonthlyRepayment = totalPayment / 60;
                    myVehicle.MonthlyRepaymentIncInsurance = totalMonthlyRepayment + myVehicle.InsurancePremium;

                    Console.Write("\nYour Monthly Repayment including the Insurance Premium on a " + myVehicle.MakeAndModel + " is -- {0}", myVehicle.MonthlyRepaymentIncInsurance);
                    Console.Write("\nYour Total Remaining balance in the end is -- {0}", Math.Round(myBalance.avaliableMoneyIncRent + myBalance.avaliableMoney - myVehicle.MonthlyRepaymentIncInsurance, 2));

                //calculation for delegate
                double delValue = myBalance.avaliableMoneyIncRent + myBalance.avaliableMoney + myVehicle.MonthlyRepaymentIncInsurance;

                //loop for delegate
                if (delValue > 0.75)
                {
                    Console.WriteLine("\n" + DelMsg);
                }

                //values used for bubble sort. bubble sort is used to display values in decending order
                double[] arr = {    myExpenses.Groceries,
                                    myExpenses.LightsAndWater,
                                    myExpenses.TravelCosts,
                                    myExpenses.CellPhone,
                                    myExpenses.Miscellaneous,
                                    myProperty.RepaymentAmount,
                                    myRent.RentExpense,
                                    myVehicle.MonthlyRepaymentIncInsurance
                            };

                double temp;
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    for (i = 0; i < arr.Length - 1 - j; i++)
                    {
                        if (arr[i] < arr[i + 1])
                        {
                            temp = arr[i + 1];
                            arr[i + 1] = arr[i];
                            arr[i] = temp;
                        }
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine("-----Expenses listed in Decending order-----");
                foreach (int p in arr)
                    Console.WriteLine("\t\t " + p);
                    Console.Read();

            }
        }
    }
}

/*References
C# Tutorial (2021). C# delegate tutorial. [Online] From: https://www.completecsharptutorial.com/basic/c-delegate-tutorial-with-easy-example.php
W3 Schools (2021) C# Exceptions. [Online] From: https://www.w3schools.com/cs/cs_exceptions.asp
Lionsure (2020) Bubble sort algorithm. [Online] From: http://www.liangshunet.com/en/202007/799693701.htm
TutorialsTeach (2021) Generic & Non-Generic collections. [Online] From: https://www.tutorialsteacher.com/csharp/csharp-collection
*/