using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudgetProject : QuarterComplexBudget, IDataRetriever<QuarterComplexBudgetProject>
    {
        private readonly BudgetProject _budgetProject = new BudgetProject();

        [Dependency]
        public IMonthComplexBudgetProjectDataProvider MonthComplexBudgetProjectDataProvider { get; set; }

        public int UpdatedPersonId
        {
            get { return _budgetProject.UpdatedPersonId; }
            set { _budgetProject.UpdatedPersonId = value; }
        }

        public int Revision
        {
            get { return _budgetProject.Revision; }
            set { _budgetProject.Revision = value; }
        }

        public DateTime RevisionDate
        {
            get { return _budgetProject.RevisionDate; }
            set { _budgetProject.RevisionDate = value; }
        }

        public Employe UpdatedPerson
        {
            get { return _budgetProject.UpdatedPerson; }
            set { _budgetProject.UpdatedPerson = value; }
        }

        public bool IsAccepted
        {
            get { return _budgetProject.IsAccepted; }
            set { _budgetProject.IsAccepted = value; }
        }

        public bool IsRejected
        {
            get { return _budgetProject.IsRejected; }
            set { _budgetProject.IsRejected = value; }
        }

        public bool HasMonthBudgets
        {
            get
            {
                return
                    MonthComplexBudgetProjectDataProvider.GetBudgetProjects(Year, QuarterNumber * 3, AdministrativeUnitId).Any(c => c.IsAccepted) &&
                    MonthComplexBudgetProjectDataProvider.GetBudgetProjects(Year, QuarterNumber * 3 - 1, AdministrativeUnitId).Any(c => c.IsAccepted) &&
                    MonthComplexBudgetProjectDataProvider.GetBudgetProjects(Year, QuarterNumber * 3 - 2, AdministrativeUnitId).Any(c => c.IsAccepted);
            }
        }

        public new QuarterComplexBudgetProject Setup(IDataRecord record)
        {
            _budgetProject.Setup(record);
            base.Setup(record);
            return this;
        }

        public override ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>(_budgetProject.SqlParameters);

                sqlParams.AddRange(base.InsertSqlParameters);

                return sqlParams.ToArray();
            }
        }

        public override ICollection<SqlParameter> UpdateSqlParameters
        {
            get { return InsertSqlParameters; }
        }
    }
}