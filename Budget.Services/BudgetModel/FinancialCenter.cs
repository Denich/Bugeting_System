using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class FinancialCenter : AdministrativeUnit, IDataRetriever<FinancialCenter>
    {
        public int CompanyId { get; set; }

        public FinancialCenterType Type { get; set; }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Adress", SqlHelper.GetSqlValue(Adress)),
                        new SqlParameter("Phone", SqlHelper.GetSqlValue(Phone)),
                        new SqlParameter("Description", SqlHelper.GetSqlValue(Description)),
                        new SqlParameter("DirectorId", SqlHelper.GetSqlValue(DirectorId)),
                        new SqlParameter("Type", SqlHelper.GetSqlValue(Type)),
                        new SqlParameter("CompanyId", SqlHelper.GetSqlValue(CompanyId)),
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

        public FinancialCenter Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            Type = (FinancialCenterType) Convert.ToInt32(record["Type"]);
            CompanyId = Convert.ToInt32(record["CompanyId"]);
            Adress = Convert.ToString(record["Adress"]);
            Phone = Convert.ToString(record["Phone"]);
            Description = Convert.ToString(record["Description"]);
            DirectorId = Convert.ToInt32(record["DirectorId"]);
            return this;
        }
    }
}