using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class YearComplexBudgetProject : YearComplexBudget, IDataRetriever<YearComplexBudgetProject>
    {
        private readonly BudgetProject _budgetProject = new BudgetProject();

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

        public override ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>(_budgetProject.SqlParameters);

                sqlParams.AddRange(base.InsertSqlParameters);

                return sqlParams;
            }
        }

        public override ICollection<SqlParameter> UpdateSqlParameters
        {
            get { return InsertSqlParameters; }
        }

        public new YearComplexBudgetProject Setup(IDataRecord record)
        {
            _budgetProject.Setup(record);
            base.Setup(record);
            return this;
        }
    }
}