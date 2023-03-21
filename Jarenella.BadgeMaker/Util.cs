// using System;
// using System.IO;
// using System.Collections.Generic;
using SkiaSharp;

namespace Jarenella.BadgeMaker
{
    class Util
    {
        //printEmployees method
        public static void PrintEmployees(List<Employee> employees)
        {
            for (int i=0; i < employees.Count; i++)
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
            //create new SteamWriter file but only have it be active while we need it / are using it
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                file.WriteLine("ID,Name,PhotoUrl");
                for (int i = 0; i < employees.Count; i++)
                {
                    string template = "{0},{1},{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
                }
            }
        }

        //MakeBadges method to create badges with employee data
        async public static Task MakeBadges(List<Employee> employees)
        {
            // SKImage newIMage = SKImage.FromEncodedData(File.OpenRead("badge.png")); //creates image from path
            // SKData data = newIMage.Encode(); //encodes image data
            // data.SaveTo(File.OpenWrite("data/employeeBadge.png")); //use file.openwrite method to turn path into stream (saveto method requires a stream) to tell program where to save new file

            // for (int i=0; i < employees.Count; i++)
            // {
            //     //generate a badge with employee[i]'s info
            // }
        }
    }
}