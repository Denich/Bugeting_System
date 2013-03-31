using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudget : ComplexBudget, IDataRetriever<MonthComplexBudget>
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public virtual ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("Month", SqlHelper.GetSqlValue(Month))
                    };
            }
        }

        public virtual ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters;
                InsertSqlParameters.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public virtual MonthComplexBudget Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]);
            Year = Convert.ToInt32(record["Year"]);
            Month = Convert.ToInt32(record["Month"]);
            return this;
        }
    }
}