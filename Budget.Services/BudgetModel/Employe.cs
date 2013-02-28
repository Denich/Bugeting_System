namespace Budget.Services.BudgetModel
{
    public class Employe
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MidleName { get; set; }

        public CompanyPosition Position { get; set; }

        public EmployeContacts Contact { get; set; }
    }
}