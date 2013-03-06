using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class CompanyInfo : AdministrativeUnit
    {
        public CompanyInfo(string name): base(name)
        {
        }

        public CompanyInfo(int id, string name) : base(id, name)
        {
        }

        public int AccountNumber { get; set; }

        public int Edrpou { get; set; }

        public IEnumerable<FinancialCenter> FinancialCenters { get; set; }
    }
}