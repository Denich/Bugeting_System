﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class YearComplexBudget : ComplexBudget, IDataRetriever<YearComplexBudget>
    {
        private IEnumerable<YearComplexBudget> _childBudgets;

        [Dependency]
        public IYearComplexBudgetDataProvider YearComplexBudgetDataProvider { get; set; }

        public int Year { get; set; }

        public virtual ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("MasterBudgetID", SqlHelper.GetSqlValue(MasterBudgetID)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("IsFinal", SqlHelper.GetSqlValue(IsFinal))
                    };
            }
        }

        public virtual ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters.ToList();
                sqlParams.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public YearComplexBudget Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]);
            MasterBudgetID = Convert.ToInt32(record["MasterBudgetID"]);
            Year = Convert.ToInt32(record["Year"]);
            IsFinal = Convert.ToBoolean(record["IsFinal"]);
            return this;
        }

        public IEnumerable<YearComplexBudget> ChildBudgets
        {
            get
            {
                return _childBudgets ?? YearComplexBudgetDataProvider.GetByMaster(Id);
            }
            set
            {
                _childBudgets = value;
            }
        }

        public override string GetPeriodName()
        {
            return Year + " рік";
        }

        public override string GetShortPeriodName()
        {
            return Year + "р.";
        }
    }
}