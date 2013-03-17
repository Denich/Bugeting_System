using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class TargetBudget
    {
        private TargetBudgetInfo _info;

        private IEnumerable<BudgetItem> _budgetItems;

        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        public IBudgetItemDataProvider BudgetItemDataProvider { get; set; }

        public TargetBudget()
        {
            TargetBudgetInfoDataProvider = new TargetBudgetInfoDataProvider();

            BudgetItemDataProvider = new BudgetItemDataProvider();
        }

        public int Id { get; set; }

        public int InfoId { get; set; }

        public TargetBudgetInfo Info
        {
            get
            {
                return _info ?? TargetBudgetInfoDataProvider.GetTargetBudgetInfoById(InfoId);
            }
            set
            {
                _info = value;

                InfoId = value.Id;
            }
        }

        public double Value { get; set; }

        public int BudgetCategoryId { get; set; }

        public IEnumerable<BudgetItem> BudgetItems
        {
            get
            {
                if (_budgetItems != null)
                {
                    return _budgetItems;
                }

                var budgetItems = BudgetItemDataProvider.GetBudgetItems();

                return budgetItems == null ? null : budgetItems.Where(t => t.TargetBudgetId == Id);
            }
            set { _budgetItems = value; }
        }

        public static TargetBudget Create(IDataReader record)
        {
            return new TargetBudget
            {
                Id = Convert.ToInt32(record["Id"]),
                InfoId = Convert.ToInt32(record["InfoId"]),
                Value = Convert.ToDouble(record["Value"]),
                BudgetCategoryId = Convert.ToInt32(record["BudgetCategoryId"]),
            };
        }

        public virtual SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("InfoId", SqlHelper.GetSqlValue(InfoId)),
                        new SqlParameter("Value", SqlHelper.GetSqlValue(Value)),
                        new SqlParameter("BudgetCategoryId", SqlHelper.GetSqlValue(BudgetCategoryId)),
                    };

                //note: it budget category info center is new, its id = 0
                if (Id != 0)
                {
                    sqlParams.Add(new SqlParameter("Id", Id));
                }

                return sqlParams.ToArray();
            }
        }
    }
}
