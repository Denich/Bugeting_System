using System.Collections.Generic;
using System.Linq;
using Budget.Services.BudgetServices.DataProviders;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategory
    {
        private IEnumerable<TargetBudget> _targetBudgets;
        
        private Employe _responsibleEmploye;

        private BudgetCategoryInfo _info;

        public BudgetCategory()
        {
            BudgetCategoryDataProvider = new TargetBudgetsDataProvider();

            EmployeDataProvider = new EmployeDataProvider();

            BudgetCategoryInfoDataProvider = new BudgetCategoryInfoDataProvider();
        }

        public ITargetBudgetsDataProvider BudgetCategoryDataProvider { get; set; }

        public IEmployeDataProvider EmployeDataProvider { get; set; }

        public IBudgetCategoryInfoDataProvider BudgetCategoryInfoDataProvider { get; set; }

        public int Id { get; set; }

        public double Value { get; set; }

        public int ComplexBudgetId { get; set; }

        public int InfoId { get; set; }

        public BudgetCategoryInfo Info
        {
            get
            {
                return _info ?? BudgetCategoryInfoDataProvider.GetBudgetCategoryInfoById(InfoId);
            }
            set
            {
                _info = value;

                InfoId = value.Id;
            }
        }

        public int ResponsibleEmployeId { get; set; }

        public Employe ResponsibleEmploye
        {
            get
            {
                return _responsibleEmploye ?? EmployeDataProvider.GetEmploye(ResponsibleEmployeId);
            }
            set
            {
                _responsibleEmploye = value;

                ResponsibleEmployeId = value.Id;
            }
        }

        public IEnumerable<TargetBudget> TargetBudgets
        {
            get
            {
                if (_targetBudgets != null)
                {
                    return _targetBudgets;
                }

                var targetBudgets = BudgetCategoryDataProvider.GetTargetBudgets();

                return targetBudgets == null ? null : targetBudgets.Where(t => t.BudgetCategoryId == Id);
            }
            set { _targetBudgets = value; }
        }
    }
}
