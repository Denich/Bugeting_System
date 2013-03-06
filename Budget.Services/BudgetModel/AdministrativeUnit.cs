namespace Budget.Services.BudgetModel
{
    public abstract class AdministrativeUnit
    {
        protected AdministrativeUnit(string name)
            : this(-1, name)
        {
        }

        protected AdministrativeUnit(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Employe Director { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }
    }
}