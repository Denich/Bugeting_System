using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudgetProject : QuarterComplexBudget
    {
        private readonly BudgetProject _budgetProject = new BudgetProject();

        private QuarterComplexBudgetProject(BudgetProject budgetProject, QuarterComplexBudget quarterComplexBudget)
            : this(quarterComplexBudget)
        {
            _budgetProject = budgetProject;
        }

        public QuarterComplexBudgetProject(QuarterComplexBudget quarterComplexBudget)
            : base(quarterComplexBudget)
        {
        }

        public QuarterComplexBudgetProject()
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

        public new static QuarterComplexBudgetProject Create(IDataReader record)
        {
            return new QuarterComplexBudgetProject(BudgetProject.Create(record), QuarterComplexBudget.Create(record));
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