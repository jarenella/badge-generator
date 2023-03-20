// using System;
// using System.IO;
// using System.Collections.Generic;

namespace Jarenella.BadgeMaker{
    class Util
    {
        //printEmployees method
        public static void PrintEmployees(List<Employee> employees)
        {
            for (int i =0; i < employees.Count; i++)
            {
                string template = "{0, -10}\t{1, -20}\t{2}";
                Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
        }

        //makeCSV method to save employee data
        public static void MakeCSV(List<Employee> employees)
        {
            if (!Directory.Exists("data")) //make a data folder if it doesn't exist
            {
                Directory.CreateDirectory("data");
            }
        }
    }
}