
namespace Jarenella.BadgeMaker
{
    class Program
    {
        //main method
        async static Task Main(string[] args)
        {
            // List<Employee> employees = PeopleFetcher.GetEmployees();
            List<Employee> employees = await PeopleFetcher.GetFromApi();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
        }
    }
}