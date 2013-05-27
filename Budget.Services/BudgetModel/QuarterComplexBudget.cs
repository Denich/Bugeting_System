using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudget : ComplexBudget, IDataRetriever<QuarterComplexBudget>
    {
        private IEnumerable<QuarterComplexBudget> _childBudgets;

        public QuarterComplexBudget()
        {
            YearBudgetID = -1;
        }

        [Dependency]
        public IQuarterComplexBudgetDataProvider QuarterComplexBudgetDataProvider { get; set; }

        public int QuarterNumber { get; set; }

        public int Year { get; set; }

        public int YearBudgetID { get; set; }

        public virtual ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("MasterBudgetID", SqlHelper.GetSqlValue(MasterBudgetID)),
                        new SqlParameter("YearBudgetID", SqlHelper.GetSqlValue(YearBudgetID)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                        new SqlParameter("QuarterNumber", SqlHelper.GetSqlValue(QuarterNumber)),
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

        public QuarterComplexBudget Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]);
            MasterBudgetID = Convert.ToInt32(record["MasterBudgetID"]);
            YearBudgetID = Convert.ToInt32(record["YearBudgetID"]);
            Year = Convert.ToInt32(record["Year"]);
            QuarterNumber = Convert.ToInt32(record["QuarterNumber"]);
            IsFinal = Convert.ToBoolean(record["IsFinal"]);
            return this;
        }

        public IEnumerable<QuarterComplexBudget> ChildBudgets
        {
            get
            {
                return _childBudgets ?? QuarterComplexBudgetDataProvider.GetByMaster(Id);
            }
            set
            {
                _childBudgets = value;
            }
        }

        public override string GetPeriodName()
        {
            return QuarterNumber + "-й квартал " + Year + " рік";
        }

        public override string GetShortPeriodName()
        {
            return QuarterNumber + "-й квартал";
        }
    }
}
