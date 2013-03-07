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

        public IEnumerable<TargetBudgetInfo> TargetBudgetInfos
        {
            get
            {
                return _targetBudgetInfos ?? TargetBudgetInfoDataProvider.GetTargetBudgetInfos().Where(t => t.BudgetCategoryId == Id);
            }
            set { _targetBudgetInfos = value; }
        }

        public bool IsDeleted { get; set; }
    }
}
