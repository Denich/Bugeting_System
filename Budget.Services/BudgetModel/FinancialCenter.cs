namespace Budget.Services.BudgetModel
{
    public class FinancialCenter : AdministrativeUnit
    {
        public FinancialCenter(string name, FinancialCenterType type)
            : base(name)
        {
            Type = type;

            CompanyId = -1;
        }

        public FinancialCenter(int id, string name, FinancialCenterType type)
            : base(id, name)
        {
            Type = type;

            CompanyId = -1;
        }

        public int CompanyId { get; set; }

        public FinancialCenterType Type { get; set; }
    }
}