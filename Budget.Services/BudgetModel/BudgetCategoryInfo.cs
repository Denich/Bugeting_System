using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using System.Linq;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategoryInfo : IDataRetriever<BudgetCategoryInfo>
    {
        private IEnumerable<TargetBudgetInfo> _targetBudgetInfos;

        [Dependency]
        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        [Dependency]
        public IYearComplexBudgetProjectDataProvider YearComplexBudgetProjectDataProvider { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public IEnumerable<TargetBudgetInfo> TargetBudgetInfos
        {
            get
            {
                if (_targetBudgetInfos != null)
                {
                    return _targetBudgetInfos;
                }

                var targetBudgets = TargetBudgetInfoDataProvider.GetAll();

                return targetBudgets == null ? null : targetBudgets.Where(t => t.BudgetCategoryId == Id);
            }
            set { _targetBudgetInfos = value; }
        }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Description", SqlHelper.GetSqlValue(Description)),
                        new SqlParameter("IsDeleted", SqlHelper.GetSqlValue(IsDeleted)),
                        new SqlParameter("DateAdded", SqlHelper.GetSqlValue(DateAdded)),
                        new SqlParameter("Source", SqlHelper.GetSqlValue(Source))
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

        public BudgetCategoryInfo Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            IsDeleted = Convert.ToBoolean(record["IsDeleted"]);
            Description = Convert.ToString(record["Description"]);
            DateAdded = Convert.ToDateTime(record["DateAdded"]);
            Source = Convert.ToString(record["Source"]);
            return this;
        }
    }
}
