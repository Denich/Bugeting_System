using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class ComplexBudget
    {
        public AdministrativeUnit Unit { get; set; }

        public IEnumerable<BudgetCategory> Categories { get; set; }
    }
}
