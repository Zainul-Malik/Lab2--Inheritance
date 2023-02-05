using Employees.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    /// <summary>
    /// CPRG211 - Lab Inheritance
    /// </summary>
    /// <remarks>Author: </remarks>
    /// <remarks>Date: </remarks>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create list that holds Employee instances
            List<Employee> employees = new List<Employee>();

            // Must be a relative path
            string path = "employees.txt";

            // Converts lines in file into an array of strings
            string[] lines = File.ReadAllLines(path);

            // Loop through each line
            foreach (string line in lines)
            {
                // Split line into parts or cells.
                string[] cells = line.Split(':');

                // The first 3 cells are the ID, name, and address.
                string id = cells[0];
                string name = cells[1];
                string address = cells[2];
				
				// TODO: Get remaining employee info from cells

                // Extract the first digit of the ID.
                string firstDigit = id.Substring(0, 1);

                // Convert first digit from string to int.
                int firstDigitInt = int.Parse(firstDigit);

                // Check range of first digit
                if (firstDigitInt >= 0 && firstDigitInt <= 4)
                {
                    // Salaried
                    string salary = cells[7];

                    // Convert salary from string to double.
                    double salaryDouble = double.Parse(salary);

                    // Create Salaried instance
                    Salaried salaried = new Salaried(id, name, address, salaryDouble);

                    // Add to list of employees.
                    employees.Add(salaried);
                }
                else if (firstDigitInt >= 5 && firstDigitInt <= 7)
                {
                    // Wage
                    string rate = cells[7];
                    string hours = cells[8];

                    // Convert rate and hours from string to double
                    double rateDouble = double.Parse(rate);
                    double hoursDouble = double.Parse(hours);

                    // Create Wages instance
                    Waged wages = new Waged(id,name, address, rateDouble, hoursDouble);

                    // Add to list of employees.
                    employees.Add(wages);
                }
                else if (firstDigitInt >= 8 && firstDigitInt <= 9)
                {
                    // Part time
                    string rate = cells[7];
                    string hours = cells[8];

                    // Convert rate and hours from string to double
                    double rateDouble = double.Parse(rate);
                    double hoursDouble = double.Parse(hours);

                    // Create PartTime instance
                    PartTime partTime = new PartTime(id, name, address, rateDouble, hoursDouble);

                    // Add to list of employees
                    employees.Add(partTime);

                }
            }
            /*if (firstDigit == "0" || firstDigit == "1" || firstDigit == "2" || firstDigit == "3" || firstDigit == "4")
               {

               }*/

            /**
             * TODO:
             *  - Determine average weekly pay of all employees.
             *  - Determine highest paid waged employee.
             *  - Determine lowest paid salaried employee.
             *  - Determine percentage of employees that are salaried, waged, and part-time.
             */

            double weeklyPaySum = 0;


            // It's okay to use loop through employees multiple times.
            foreach (Employee employee in employees)
            {
                double weeklyPay = employee.CalcWeeklyPay();

                weeklyPaySum += weeklyPay;
            }
            double averageWeeklyPay = weeklyPaySum / employees.Count;

            Console.WriteLine("Average weekly pay: $" + (Math.Round(averageWeeklyPay,2)) + "\n");



            //Highest paid waged employee
            Waged highestPaidWaged = null;

            foreach (Employee employee in employees)
            {
                if (employee is Waged)
                {
                    Waged waged = (Waged)employee;

                    if (highestPaidWaged == null || waged.CalcWeeklyPay() > highestPaidWaged.CalcWeeklyPay())
                    {
                        //Assigns current item to found
                        highestPaidWaged=waged;
                    }


                }
  
            }
            Console.WriteLine("Highest waged employee: " + highestPaidWaged.Name + "\n" + "Highest waged pay: $"+ highestPaidWaged.CalcWeeklyPay() + "\n");



            //Lowest paid salaried employee
            Salaried lowestPaidSalaried = null;

            foreach (Employee employee in employees)
            {
                if (employee is Salaried)
                {
                    Salaried salaried = (Salaried)employee;

                    if (lowestPaidSalaried == null || salaried.CalcWeeklyPay() < lowestPaidSalaried.CalcWeeklyPay())
                    {
                        lowestPaidSalaried=salaried;
                    }
                }
            }
            Console.WriteLine("Lowest salaried employee: " + lowestPaidSalaried.Name + "\n" + "Lowest salaried pay: $" + lowestPaidSalaried.CalcWeeklyPay() + "\n");

             

            //Employee percentages

            //Employee count
            int salariedEmployees = 0;
            int wagedEmployees = 0;
            int partTimeEmployees = 0;

            //Employee percentages
            double wagedPercentage = 0;
            double salariedPercentage = 0;
            double partTimePercentage = 0;
            foreach (Employee employee in employees)
            {
                if (employee is Salaried)
                { 
                    //Adds a salaried employee to the salariedEmployees variable
                    salariedEmployees++;
                }
                else if (employee is Waged)
                {
                    //Adds a waged employee to the wagedEmployees variable
                    wagedEmployees++;
                }
                else
                {
                    //Adds a part time employee to the partTimeEmployees variable
                    partTimeEmployees++;
                }

            }

            salariedPercentage += (salariedEmployees / (double)employees.Count()) * 100;
            wagedPercentage += (wagedEmployees / (double)employees.Count()) * 100;
            partTimePercentage += (partTimeEmployees / (double)employees.Count()) * 100;

            Console.WriteLine("Salaried: " + salariedEmployees + "/" + (double)employees.Count() + " (" + Math.Round(salariedPercentage,2) + "%)");
            Console.WriteLine("Waged: " + wagedEmployees + "/" + (double)employees.Count() + " (" + Math.Round(wagedPercentage, 2) + "%)");
            Console.WriteLine("Part Time: " + partTimeEmployees + "/" + (double)employees.Count() + " (" + Math.Round(partTimePercentage, 2) + "%)" + "\n");
            Console.ReadLine();
        }
    }
            
}
