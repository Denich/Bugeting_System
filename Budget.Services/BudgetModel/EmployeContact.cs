﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class EmployeContact : IDataRetriever<EmployeContact>
    {
        public int EmployeId { get; set; }

        public string WorkPhone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("EmployeId", SqlHelper.GetSqlValue(EmployeId)),
                        new SqlParameter("WorkPhone", SqlHelper.GetSqlValue(WorkPhone)),
                        new SqlParameter("MobilePhone", SqlHelper.GetSqlValue(MobilePhone)),
                        new SqlParameter("Email", SqlHelper.GetSqlValue(Email)),
                        new SqlParameter("Skype", SqlHelper.GetSqlValue(Skype)),
                    };
            }
        }

        public ICollection<SqlParameter> UpdateSqlParameters
        {
            get { return InsertSqlParameters; }
        }

        public EmployeContact Setup(IDataRecord record)
        {
            EmployeId = Convert.ToInt32(record["EmployeId"]);
            WorkPhone = Convert.ToString(record["WorkPhone"]);
            MobilePhone = Convert.ToString(record["MobilePhone"]);
            Email = Convert.ToString(record["Email"]);
            Skype = Convert.ToString(record["Skype"]);

            return this;
        }

        public virtual SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("EmployeId", SqlHelper.GetSqlValue(EmployeId)),
                        new SqlParameter("WorkPhone", SqlHelper.GetSqlValue(WorkPhone)),
                        new SqlParameter("MobilePhone", SqlHelper.GetSqlValue(MobilePhone)),
                        new SqlParameter("Email", SqlHelper.GetSqlValue(Email)),
                        new SqlParameter("Skype", SqlHelper.GetSqlValue(Skype)),
                    };

                return sqlParams.ToArray();
            }
        }
    }
}