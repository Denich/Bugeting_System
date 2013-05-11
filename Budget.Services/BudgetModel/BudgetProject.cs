using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class BudgetProject
    {
        [Dependency]
        public IEmployeDataProvider EmployeDataProvider { get; set; }

        private Employe _updatedPerson;

        public int UpdatedPersonId { get; set; }

        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

        public Employe UpdatedPerson
        {
            get { return _updatedPerson ?? EmployeDataProvider.Get(UpdatedPersonId); }
            set
            {
                _updatedPerson = value;

                UpdatedPersonId = value.Id;
            }
        }

        public bool IsAccepted
        {
            get { return Status == BudgetProjectStatus.Accepted; }
        }

        public BudgetProjectStatus Status { get; set; }

        public string Comment { get; set; }

        public BudgetProject Setup(IDataRecord record)
        {
            Revision = Convert.ToInt32(record["Revision"]);
            RevisionDate = Convert.ToDateTime(record["RevisionDate"]);
            UpdatedPersonId = Convert.ToInt32(record["UpdatePersonId"]);
            Status = (BudgetProjectStatus) Convert.ToInt32(record["Status"]);
            Comment = Convert.ToString(record["Comment"]);
            return this;
        }

        public SqlParameter[] SqlParameters
        {
            get
            {

                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("Revision", SqlHelper.GetSqlValue(Revision)),
                        new SqlParameter("RevisionDate", SqlHelper.GetSqlValue(RevisionDate)),
                        new SqlParameter("UpdatePersonId", SqlHelper.GetSqlValue(UpdatedPersonId)),
                        new SqlParameter("Status", SqlHelper.GetSqlValue(Status)),
                        new SqlParameter("Comment", SqlHelper.GetSqlValue(Comment))
                    };

                return sqlParams.ToArray();
            }
        }
    }
}
