using System;
using System.Collections.Generic;
using Budget.Services.BudgetServices.DataProviders;
using System.Linq;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategoryInfo
    {
        private IEnumerable<TargetBudgetInfo> _targetBudgetInfos;

        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        public BudgetCategoryInfo()
        {
            TargetBudgetInfoDataProvider = new TargetBudgetInfoDataProvider();//todo: change for DI
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public IEnumerable<TargetBudgetInfo> TargetBudgetInfos
        {
            get
            {
                if (_targetBudgetInfos != null)
                {
                    return _targetBudgetInfos;
                }
                
                var targetBudgets = TargetBudgetInfoDataProvider.GetTargetBudgetInfos();

                return targetBudgets == null ? null : targetBudgets.Where(t => t.BudgetCategoryId == Id);
            }
            set { _targetBudgetInfos = value; }
        }
    }
}
