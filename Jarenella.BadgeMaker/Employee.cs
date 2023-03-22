namespace Jarenella.BadgeMaker
{
    class Employee
    {
        private string FirstName;
        private string LastName;
        private int Id;
        private string PhotoUrl;
        private string CompanyName;
        public Employee(string firstName, string lastName, int id, string photoUrl, string companyName)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            PhotoUrl = photoUrl;
            CompanyName = companyName;
        }
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
        public int GetId()
        {
            return Id;
        }
        public string GetPhotoUrl() {
            return PhotoUrl;
        }
        public string GetCompanyName() {
            return CompanyName;
        }
    }
}