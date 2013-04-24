using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class BudgetItemInfo : IDataRetriever<BudgetItemInfo>
    {
        [Dependency]
        public IYearComplexBudgetProjectDataProvider YearComplexBudgetProjectDataProvider { get; set; }

        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Description", SqlHelper.GetSqlValue(Description)),
                        new SqlParameter("IsDeleted", SqlHelper.GetSqlValue(IsDeleted)),
                        new SqlParameter("TargetBudgetId", SqlHelper.GetSqlValue(TargetBudgetId)),
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

        public BudgetItemInfo Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            TargetBudgetId = Convert.ToInt32(record["TargetBudgetId"]);
            IsDeleted = Convert.ToBoolean(record["IsDeleted"]);
            Description = Convert.ToString(record["Description"]);
            DateAdded = Convert.ToDateTime(record["DateAdded"]);
            Source = Convert.ToString(record["Source"]);
            return this;
        }

        public bool IsUsedInBudgetProject(int year, int adminUnitId)
        {
            return YearComplexBudgetProjectDataProvider.GetAll()
                                                       .Any(
                                                           c =>
                                                           c.Year == year && c.AdministrativeUnitId == adminUnitId &&
                                                           c.IsAccepted &&
                                                           c.BudgetCategories != null &&
                                                           c.BudgetCategories.Any(
                                                               b =>
                                                               b.TargetBudgets != null &&
                                                               b.TargetBudgets.Any(
                                                                   t =>
                                                                   t.BudgetItems != null &&
                                                                   t.BudgetItems.Any(i => i.InfoId == Id))));
        }
    }
}
