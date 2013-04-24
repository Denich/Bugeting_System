using System.Collections.Generic;

namespace Budget.Web.Models
{
    public class EditBudgetItemsModel
    {
        public int Year { get; set; }

        public IList<BudgetCategoryInfoSelectModel> Categories { get; set; }
    }
}