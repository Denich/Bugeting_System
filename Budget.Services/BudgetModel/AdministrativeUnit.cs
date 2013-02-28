namespace Budget.Services.BudgetModel
{
    public abstract class AdministrativeUnit
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Employe Director { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }
    }
}