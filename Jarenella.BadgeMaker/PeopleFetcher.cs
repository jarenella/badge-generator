using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Jarenella.BadgeMaker
{
    class PeopleFetcher
    {
        //getemployees method
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>(); //starting with an empty list
            while (true)
            {
                Console.WriteLine("Please enter a first name: ");
                // taking a name from the console and assigning it to a variable
                string firstName = Console.ReadLine() ?? ""; //?? operator so if input is null, value of empty string is assigned
                if (firstName == "")
                {
                    break;
                }
                Console.WriteLine("Please enter a last name: ");
                string lastName = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter an employee ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? ""); //parse string to int
                Console.WriteLine("Please enter a photo URL: ");
                string photoUrl = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter a company name: ");
                string companyName = Console.ReadLine() ?? "";
                //creates a new employee instance
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl, companyName);
                //add the employee to the list of employees
                employees.Add(currentEmployee);
            }
            //return a list of strings
            return employees;
        }

        //getfromapi method
        async public static Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>(); //create empty employee list

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);
                //Console.WriteLine(json.SelectToken("results[0].name.first"));
                for (int i=0; i < 10; i++)
                {
                    //for each loop make a new employee and add them to a list
                    //then return that list
                    JToken person = json.SelectToken($"results[{i}]");
                    // Console.WriteLine(person.SelectToken("name").SelectToken("first"));
                    string firstName = person.SelectToken("name").SelectToken("first")?.ToObject<string>();
                    string lastName = person.SelectToken("name").SelectToken("last")?.ToObject<string>();
                    string id = person.SelectToken("id").SelectToken("value")?.ToObject<string>();
                    string idNoDash = id.Remove(3, 1).Remove(5, 1); //remove dashes from IDs
                    int idInt = int.Parse(idNoDash);
                    string photoUrl = person.SelectToken("picture").SelectToken("thumbnail")?.ToObject<string>();
                    Employee currentEmployee = new Employee(firstName, lastName, idInt, photoUrl, "");
                    employees.Add(currentEmployee);
                }
            }

            return employees; //return list of employees
        }
    }
}