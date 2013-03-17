using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class BudgetItem
    {
        private BudgetItemInfo _info;

        [Dependency]
        public IBudgetItemInfoDataProvider BudgetItemInfoDataProvider { get; set; }

        public BudgetItem()
        {
            BudgetItemInfoDataProvider = new BudgetItemInfoDataProvider();
        }

        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public int InfoId { get; set; }

        public double Value { get; set; }

        public BudgetItemInfo Info
        {
            get { return _info ?? BudgetItemInfoDataProvider.GetBudgetItemInfoById(InfoId); }
            set
            {
                _info = value;
                InfoId = value.Id;
            }
        }

        public static BudgetItem Create(IDataReader record)
        {
            return new BudgetItem
            {
                Id = Convert.ToInt32(record["Id"]),
                InfoId = Convert.ToInt32(record["InfoId"]),
                Value = Convert.ToDouble(record["Value"]),
                TargetBudgetId = Convert.ToInt32(record["TargetBudgetId"]),
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
                        new SqlParameter("TargetBudgetId", SqlHelper.GetSqlValue(TargetBudgetId)),
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
