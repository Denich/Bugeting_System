using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudgetProject : MonthComplexBudget, IDataRetriever<MonthComplexBudgetProject>
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

        public new MonthComplexBudgetProject Setup(IDataRecord record)
        {
            _budgetProject.Setup(record); 
            base.Setup(record);
            return this;
        }
    }
}