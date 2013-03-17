using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudgetProject : MonthComplexBudget
    {
        private readonly BudgetProject _budgetProject = new BudgetProject();

        private MonthComplexBudgetProject(BudgetProject budgetProject, MonthComplexBudget monthComplexBudget)
            : this(monthComplexBudget)
        {
            _budgetProject = budgetProject;
        }

        public MonthComplexBudgetProject(MonthComplexBudget monthComplexBudget)
            : base(monthComplexBudget)
        {
        }

        public MonthComplexBudgetProject()
        {
        }

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

        public new static MonthComplexBudgetProject Create(IDataReader record)
        {
            return new MonthComplexBudgetProject(BudgetProject.Create(record), MonthComplexBudget.Create(record));
        }

        public override SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>(_budgetProject.SqlParameters);

                sqlParams.AddRange(base.SqlParameters);

                return sqlParams.ToArray();
            }
        }
    }
}