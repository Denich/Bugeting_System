using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class CompanyInfo : AdministrativeUnit
    {
        public int AccountNumber { get; set; }

        public int Edrpou { get; set; }

        public ICollection<FinancialCenter> FinancialCenters { get; set; }
    }
}