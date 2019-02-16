namespace KrsApiIntegration
{
    public class Employer
    {
        private string firstName;
        private string lastName;

        public string FirstName { get => this.firstName; set => this.firstName = value; }
        public string LastName { get => this.lastName; set => this.lastName = value; }
        public string FullName { get; set; }
    }
}