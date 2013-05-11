using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudgetProject : MonthComplexBudget, IDataRetriever<MonthComplexBudgetProject>
    {
        private IEnumerable<MonthComplexBudgetProject> _childBudgets;

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
        }

        public BudgetProjectStatus Status
        {
            get { return _budgetProject.Status; }
            set { _budgetProject.Status = value; }
        }

        public string Comment
        {
            get { return _budgetProject.Comment; }
            set { _budgetProject.Comment = value; }
        }

        public IEnumerable<MonthComplexBudgetProject> ChildBudgets
        {
            get
            {
                return _childBudgets ?? MonthComplexBudgetProjectDataProvider.GetByMaster(Id);
            }
            set
            {
                _childBudgets = value;
            }
        }

        public override void CalculateValues()
        {
            if (ChildBudgets != null && ChildBudgets.Any())
            {
                ChildBudgets.ForEach(b => b.CalculateValues());

                BudgetCategories = GetValuesSumFormCategories(BudgetCategories, ChildBudgets.Select(b => b.BudgetCategories));

                return;
            }

            base.CalculateValues();
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