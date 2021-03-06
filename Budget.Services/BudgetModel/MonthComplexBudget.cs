﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudget : ComplexBudget, IDataRetriever<MonthComplexBudget>
    {
        private static readonly string[] Months =
            {
                "Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень", "Вересень",
                "Жовтень", "Листопад",
                "Грудень"
            };

        private IEnumerable<MonthComplexBudget> _childBudgets;

        public MonthComplexBudget()
        {
            QuarterBudgetID = -1;
        }

        [Dependency]
        public IMonthComplexBudgetDataProvider MonthComplexBudgetDataProvider { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int QuarterBudgetID { get; set; }

        public virtual ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("MasterBudgetID", SqlHelper.GetSqlValue(MasterBudgetID)),
                        new SqlParameter("QuarterBudgetID", SqlHelper.GetSqlValue(QuarterBudgetID)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("Month", SqlHelper.GetSqlValue(Month)),
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

        public IEnumerable<MonthComplexBudget> ChildBudgets
        {
            get
            {
                return _childBudgets ?? MonthComplexBudgetDataProvider.GetByMaster(Id);
            }
            set
            {
                _childBudgets = value;
            }
        
        }

        public virtual MonthComplexBudget Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]);
            MasterBudgetID = Convert.ToInt32(record["MasterBudgetID"]);
            QuarterBudgetID = Convert.ToInt32(record["QuarterBudgetID"]);
            Year = Convert.ToInt32(record["Year"]);
            Month = Convert.ToInt32(record["Month"]);
            IsFinal = Convert.ToBoolean(record["IsFinal"]);
            return this;
        }

        public override void CalculateValues()
        {
            if (ChildBudgets.Any())
            {
                //ChildBudgets = ChildBudgets.Select(c => { c.CalculateValues(); return c; });

                BudgetCategories = GetValuesSumFormCategories(BudgetCategories, ChildBudgets.SelectMany(b => b.BudgetCategories));

                return;
            }

            base.CalculateValues();
        }

        public override string GetPeriodName()
        {
            return Months[Month - 1] + " " + Year + " рік";
        }

        public override string GetShortPeriodName()
        {
            return Months[Month - 1];
        }
    }
}