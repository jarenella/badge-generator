
namespace Jarenella.BadgeMaker
{
    class Program
    {
        //main method
        static void Main(string[] args)
        {
            List<Employee> employees = GetEmployees();
            PrintEmployees(employees);
            
        }

        //getEmployees method
        static List<Employee> GetEmployees()
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
                //creates a new employee instance
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                //add the employee to the list of employees
                employees.Add(currentEmployee);
            }
            //return a list of strings
            return employees;
        }

        //printEmployees method
        static void PrintEmployees(List<Employee> employees)
        {
            for (int i =0; i < employees.Count; i++)
            {
                string template = "{0, -10}\t{1, -20}\t{2}";
                Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
        }
    }
}