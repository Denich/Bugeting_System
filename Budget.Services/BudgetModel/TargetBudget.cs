using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class TargetBudget : IDataRetriever<TargetBudget>
    {
        private TargetBudgetInfo _info;

        private IEnumerable<BudgetItem> _budgetItems;

        [Dependency]
        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        [Dependency]
        public IBudgetItemDataProvider BudgetItemDataProvider { get; set; }

        public int Id { get; set; }

        public int InfoId { get; set; }

        public TargetBudgetInfo Info
        {
            get
            {
                return _info ?? TargetBudgetInfoDataProvider.Get(InfoId);
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

                var budgetItems = BudgetItemDataProvider.GetAll();

                return budgetItems == null ? null : budgetItems.Where(t => t.TargetBudgetId == Id);
            }
            set { _budgetItems = value; }
        }

        public ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("InfoId", SqlHelper.GetSqlValue(InfoId)),
                        new SqlParameter("Value", SqlHelper.GetSqlValue(Value)),
                        new SqlParameter("BudgetCategoryId", SqlHelper.GetSqlValue(BudgetCategoryId)),
                    };
            }
        }

        public ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters;
                InsertSqlParameters.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public TargetBudget Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            InfoId = Convert.ToInt32(record["InfoId"]);
            Value = Convert.ToDouble(record["Value"]);
            BudgetCategoryId = Convert.ToInt32(record["BudgetCategoryId"]);
            return this;
        }
    }
}
