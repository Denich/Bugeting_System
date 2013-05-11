using System.Collections.Generic;
using System.Linq;
using Budget.Web.Helpers.Converters;

namespace Budget.Web.Models.BudgetModels
{
    public class BudgetItemCollection : List<BudgetProjectItemViewModel>
    {
        public double GetValue(int budgetItemInfoId)
        {
            var result = this.SingleOrDefault(b => b.InfoId == budgetItemInfoId);
            
            return result == null ? 0 : result.Value;
        }
    }
}