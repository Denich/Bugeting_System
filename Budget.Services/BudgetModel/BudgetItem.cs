using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class BudgetItem : IDataRetriever<BudgetItem>
    {
        private BudgetItemInfo _info;

        [Dependency]
        public IBudgetItemInfoDataProvider BudgetItemInfoDataProvider { get; set; }

        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public int InfoId { get; set; }

        public double Value { get; set; }

        public BudgetItemInfo Info
        {
            get { return _info ?? BudgetItemInfoDataProvider.Get(InfoId); }
            set
            {
                _info = value;
                InfoId = value.Id;
            }
        }

        public ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("InfoId", SqlHelper.GetSqlValue(InfoId)),
                        new SqlParameter("Value", SqlHelper.GetSqlValue(Value)),
                        new SqlParameter("TargetBudgetId", SqlHelper.GetSqlValue(TargetBudgetId)),
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

        public BudgetItem Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            InfoId = Convert.ToInt32(record["InfoId"]);
            Value = Convert.ToDouble(record["Value"]);
            TargetBudgetId = Convert.ToInt32(record["TargetBudgetId"]);
            return this;
        }

        public BudgetItem ClearValues()
        {
            var clearedItem = this;

            clearedItem.Value = 0;
            clearedItem.TargetBudgetId = 0;

            return clearedItem;
        }
    }
}
