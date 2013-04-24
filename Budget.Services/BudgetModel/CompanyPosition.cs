using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class CompanyPosition : IDataRetriever<CompanyPosition>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool CanApproveBudget { get; set; }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("CanApproveBudget", SqlHelper.GetSqlValue(CanApproveBudget)),
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

        public CompanyPosition Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            CanApproveBudget = Convert.ToBoolean(record["CanApproveBudget"]);
            return this;
        }
    }
}