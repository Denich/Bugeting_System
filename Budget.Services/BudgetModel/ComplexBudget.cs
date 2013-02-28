using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class ComplexBudget
    {
        public AdministrativeUnit Unit { get; set; }

        public BudgetType Type { get; set; }

        public ICollection<BudgetCategory> Categories { get; set; }
    }
}
