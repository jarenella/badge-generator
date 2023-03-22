// using System;
// using System.IO;
// using System.Collections.Generic;
using SkiaSharp;
using System.Net.Http;
using System.Threading.Tasks;

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
            
            //bitmap variables
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;
            int PHOTO_LEFT_X = 184;
            int PHOTO_TOP_Y = 215;
            int PHOTO_RIGHT_X = 486;
            int PHOTO_BOTTOM_Y = 517;
            int COMPANY_NAME_Y = 150;
            int EMPLOYEE_NAME_Y = 600;
            int EMPLOYEE_ID_Y = 730;

            using(HttpClient client = new HttpClient())
            {
                //init a new SKPaint object
                SKPaint paint = new SKPaint();
                paint.TextSize = 42.0f;
                paint.IsAntialias = true;
                paint.Color = SKColors.Black;
                paint.IsStroke = false;
                paint.TextAlign = SKTextAlign.Center;
                paint.Typeface = SKTypeface.FromFamilyName("Arial");

                //loop through employees and create badge for each
                for (int i = 0; i < employees.Count; i++)
                {
                    //get a stream data type of employee[i]'s photourl
                    SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));
                    SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

                    SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);
                    SKCanvas canvas = new SKCanvas(badge);
                    canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT)); //skrect params are x and y coords of image
                    canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));
                    paint.Typeface = SKTypeface.FromFamilyName("Arial"); //font was changed at end of last loop, so change it back to arial
                    paint.Color = SKColors.White; //the color was set to black at the end of the last loop, so set it to white again
                    canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);
                    paint.Color = SKColors.Black; //set the color to black for the next line of text
                    canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint);
                    paint.Typeface = SKTypeface.FromFamilyName("Courier New"); //set font to courier for next line
                    canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint);

                    //generate a badge with employee[i]'s info
                    SKImage finalImage = SKImage.FromBitmap(badge);
                    SKData data = finalImage.Encode();
                    string employeeIdString = employees[i].GetId().ToString();
                    data.SaveTo(File.OpenWrite($"data/employee{employeeIdString}Badge.png"));
                }
            }
        }
    }
}