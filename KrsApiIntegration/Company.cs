namespace KrsApiIntegration
{
    using System.Collections.Generic;

    public class Company
    {
        private string name;

        private string krs;

        private IList<Employer> employers;

        public string Krs { get => krs; set => krs = value; }
        public string Name { get => name; set => name = value; }
        public IList<Employer> Employers { get => employers; set => employers = value; }
    }
}