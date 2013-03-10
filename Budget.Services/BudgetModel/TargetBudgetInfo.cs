using System;
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

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public IEnumerable<BudgetItemInfo> BudgetItemInfos
        {
            get
            {
                if (_budgetItemInfos != null)
                {
                    return _budgetItemInfos;
                }

                var budgetItems = BudgetItemInfoDataProvider.GetBudgetItemInfos();

                return budgetItems == null ? null : budgetItems.Where(t => t.TargetBudgetId == Id);
            }
            set { _budgetItemInfos = value; }
        }
    }
}