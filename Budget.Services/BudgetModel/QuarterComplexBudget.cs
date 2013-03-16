﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudget : ComplexBudget
    {
        public int QuarterNumber { get; set; }

        public int Year { get; set; }

        public static QuarterComplexBudget Create(IDataReader record)
        {
            return new QuarterComplexBudget
                {
                    Id = Convert.ToInt32(record["Id"]),
                    AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]),
                    Year = Convert.ToInt32(record["Year"]),
                    QuarterNumber = Convert.ToInt32(record["QuarterNumber"]),
                };
        }

        public SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("QuarterNumber", SqlHelper.GetSqlValue(QuarterNumber))
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
