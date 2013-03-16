using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudgetProject : QuarterComplexBudget, IBudgetProject
    {
        public QuarterComplexBudgetProject(QuarterComplexBudget quarterComplexBudget)
            : base(quarterComplexBudget)
        {
            EmployeDataProvider = new EmployeDataProvider();
        }

        public IEmployeDataProvider EmployeDataProvider { get; set; }

        public QuarterComplexBudgetProject()
        {
            EmployeDataProvider = new EmployeDataProvider();
        }

        private Employe _updatedPerson;

        public int UpdatedPersonId { get; set; }

        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

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

        public new static QuarterComplexBudgetProject Create(IDataReader record)
        {
            return new QuarterComplexBudgetProject(QuarterComplexBudget.Create(record))
                {
                    Revision = Convert.ToInt32(record["Revision"]),
                    RevisionDate = Convert.ToDateTime(record["RevisionDate"]),
                    UpdatedPersonId = Convert.ToInt32(record["UpdatedPersonId"]),
                    IsAccepted = Convert.ToBoolean(record["IsAccepted"]),
                    QuarterNumber = Convert.ToInt32(record["QuarterNumber"]),
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