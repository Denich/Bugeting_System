using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudgetProject : QuarterComplexBudget, IDataRetriever<QuarterComplexBudgetProject>
    {
        private readonly List<int> _quaretrNumbers = new List<int> {1, 2, 3, 4};

        private readonly BudgetProject _budgetProject = new BudgetProject();

        private IEnumerable<MonthComplexBudgetProject> _monthBudgets;

        [Dependency]
        public IMonthComplexBudgetProjectDataProvider MonthComplexBudgetProjectDataProvider { get; set; }

        [Dependency]
        public IQuarterComplexBudgetProjectDataProvider QuarterComplexBudgetProjectDataProvider { get; set; }

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

        private IEnumerable<QuarterComplexBudgetProject> _childBudgets;

        public IEnumerable<QuarterComplexBudgetProject> ChildBudgets
        {
            get
            {
                return _childBudgets ?? QuarterComplexBudgetProjectDataProvider.GetByMaster(Id);
            }
            set
            {
                _childBudgets = value;
            }
        }

        public IEnumerable<MonthComplexBudgetProject> MonthBudgets
        {
            get { return _monthBudgets ?? MonthComplexBudgetProjectDataProvider.GetChildForQuarterBudget(Id); }
            set { _monthBudgets = value; }
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

        public void GenerateMonthBudgets()
        {
            if (!_quaretrNumbers.Contains(QuarterNumber))
            {
                throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture,
                                                              "Quarter number '{0}' not supported", QuarterNumber));
            }

            var monthBudgets =  new List<MonthComplexBudgetProject>();

            for (int monthNumber = QuarterNumber * 3 - 2; monthNumber <= QuarterNumber * 3; monthNumber++)
            {
                monthBudgets.Add(GetMonthBudget(monthNumber));
            }

            MonthBudgets = monthBudgets;
        }

        public override void CalculateValues()
        {
            if (ChildBudgets != null && ChildBudgets.Any())
            {
                ChildBudgets.ForEach(b => b.CalculateValues());

                BudgetCategories = GetValuesSumFormCategories(BudgetCategories, ChildBudgets.Select(b => b.BudgetCategories));

                return;
            }

            if (MonthBudgets != null && MonthBudgets.Any())
            {
                MonthBudgets.ForEach(b => b.CalculateValues());
                BudgetCategories = GetValuesSumFormCategories(BudgetCategories, MonthBudgets.Select(b => b.BudgetCategories));
                return;
            }

            base.CalculateValues();
        }

        private MonthComplexBudgetProject GetMonthBudget(int monthNumber)
        {
            var monthBudget = MonthComplexBudgetProjectDataProvider.GetTemplate();

            monthBudget.Year = Year;
            monthBudget.Month = monthNumber;
            monthBudget.AdministrativeUnitId = AdministrativeUnitId;
            monthBudget.UpdatedPersonId = UpdatedPersonId;
            monthBudget.Status = Status;
            monthBudget.BudgetCategories = BudgetCategories != null
                                               ? BudgetCategories.Select(b => b.ClearValues())
                                               : null;
            return monthBudget;
        }
    }
}