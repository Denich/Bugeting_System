using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudget : ComplexBudget
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public MonthComplexBudget()
        {
        }

        public MonthComplexBudget(int id, int administrativeUnitId, int year, int month) : base(id, administrativeUnitId)
        {
            Year = year;
            Month = month;
        }

        public MonthComplexBudget(MonthComplexBudget monthComplexBudget)
            : this(monthComplexBudget.Id, monthComplexBudget.AdministrativeUnitId, monthComplexBudget.Year, monthComplexBudget.Month)
        {
            
        }

        public static MonthComplexBudget Create(IDataReader record)
        {
            return new MonthComplexBudget
            {
                Id = Convert.ToInt32(record["Id"]),
                AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]),
                Year = Convert.ToInt32(record["Year"]),
                Month = Convert.ToInt32(record["Month"]),
            };
        }

        public virtual SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("Month", SqlHelper.GetSqlValue(Month))
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