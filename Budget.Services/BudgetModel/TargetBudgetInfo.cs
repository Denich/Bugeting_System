using System.Collections.Generic;
using Budget.Services.BudgetServices.DataProviders;
using System.Linq;

namespace Budget.Services.BudgetModel
{
    public class TargetBudgetInfo
    {
        private IEnumerable<BudgetItemInfo> _budgetItemInfos;

        public IBudgetItemInfoDataProvider BudgetItemInfoDataProvider { get; set; }
        
        public TargetBudgetInfo()
        {
            BudgetItemInfoDataProvider = new BudgetItemInfoDataProvider();
        }

        public int Id { get; set; }

        public int BudgetCategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<BudgetItemInfo> BudgetItemInfos
        {
            get
            {
                return _budgetItemInfos ?? BudgetItemInfoDataProvider.GetBudgetItemInfos().Where(b => b.TargetBudgetId == Id);
            }
            set { _budgetItemInfos = value; }
        }

        public bool IsDeleted { get; set; }
    }
}