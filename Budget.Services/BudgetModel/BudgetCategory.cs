using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategory : IDataRetriever<BudgetCategory>
    {
        public BudgetCategory()
        {
            ComplexBudgetId = -1;
        }

        private IEnumerable<TargetBudget> _targetBudgets;

        private Employe _responsibleEmploye;

        private BudgetCategoryInfo _info;

        [Dependency]
        public ITargetBudgetDataProvider TargetBudgetDataProvider { get; set; }

        [Dependency]
        public IEmployeDataProvider EmployeDataProvider { get; set; }

        [Dependency]
        public IBudgetCategoryInfoDataProvider BudgetCategoryInfoDataProvider { get; set; }

        public int Id { get; set; }

        public double Value { get; set; }

        public int ComplexBudgetId { get; set; }

        public int InfoId { get; set; }

        public BudgetCategoryInfo Info
        {
            get { return _info ?? BudgetCategoryInfoDataProvider.Get(InfoId); }
            set
            {
                _info = value;

                InfoId = value.Id;
            }
        }

        public int ResponsibleEmployeId { get; set; }

        public Employe ResponsibleEmploye
        {
            get { return _responsibleEmploye ?? EmployeDataProvider.Get(ResponsibleEmployeId); }
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
                return _targetBudgets ?? TargetBudgetDataProvider.GetForCategory(Id);
            }
            set { _targetBudgets = value; }
        }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("InfoId", SqlHelper.GetSqlValue(InfoId)),
                        new SqlParameter("Value", SqlHelper.GetSqlValue(Value)),
                        new SqlParameter("ResponsibleEmployeeId", SqlHelper.GetSqlValue(ResponsibleEmployeId)),
                        new SqlParameter("ComplexBudgetId", SqlHelper.GetSqlValue(ComplexBudgetId)),
                    };
            }
        }

        public ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters;
                sqlParams.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public BudgetCategory Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            InfoId = Convert.ToInt32(record["InfoId"]);
            Value = Convert.ToDouble(record["Value"]);
            ResponsibleEmployeId = Convert.ToInt32(record["ResponsibleEmployeeId"]);
            ComplexBudgetId = Convert.ToInt32(record["ComplexBudgetId"]);
            return this;
        }

        public BudgetCategory ClearValues()
        {
            var clearedCategory = this;
            
            clearedCategory.Value = 0;
            clearedCategory.ComplexBudgetId = 0;

            if (clearedCategory.TargetBudgets != null)
            {
                clearedCategory.TargetBudgets = clearedCategory.TargetBudgets.Select(b => b.ClearValues());
            }

            return clearedCategory;
        }

        public void Calulate()
        {
            if (TargetBudgets == null || !TargetBudgets.Any())
            {
                return;
            }

            TargetBudgets.ForEach(t => t.Calculate());

            Value = TargetBudgets.Sum(t => t.Value);
        }
    }
}
