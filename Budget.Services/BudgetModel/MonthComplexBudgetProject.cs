using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudgetProject : MonthComplexBudget, IBudgetProject
    {
        public MonthComplexBudgetProject(MonthComplexBudget monthComplexBudget) : base(monthComplexBudget)
        {
        }

        public IEmployeDataProvider EmployeDataProvider { get; set; }

        public MonthComplexBudgetProject()
        {
            EmployeDataProvider = new EmployeDataProvider();
        }

        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

        private Employe _updatedPerson;

        public int UpdatedPersonId { get; set; }

        public Employe UpdatedPerson
        {
            get { return _updatedPerson ?? EmployeDataProvider.GetEmploye(UpdatedPersonId); }
            set
            {
                _updatedPerson = value;

                UpdatedPersonId = value.Id;
            }
        }

        public bool IsAccepted { get; set; }

        public new static MonthComplexBudgetProject Create(IDataReader record)
        {
            return new MonthComplexBudgetProject(MonthComplexBudget.Create(record))
            {
                Revision = Convert.ToInt32(record["Revision"]),
                RevisionDate = Convert.ToDateTime(record["RevisionDate"]),
                UpdatedPersonId = Convert.ToInt32(record["UpdatedPersonId"]),
                IsAccepted = Convert.ToBoolean(record["IsAccepted"]),
                Month = Convert.ToInt32(record["Month"]),
            };
        }

        public override SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("Revision", SqlHelper.GetSqlValue(Revision)),
                        new SqlParameter("RevisionDate", SqlHelper.GetSqlValue(RevisionDate)),
                        new SqlParameter("UpdatedPersonId", SqlHelper.GetSqlValue(UpdatedPersonId)),
                        new SqlParameter("IsAccepted", SqlHelper.GetSqlValue(IsAccepted)),
                    };

                sqlParams.AddRange(base.SqlParameters);

                return sqlParams.ToArray();
            }
        }
    }
}