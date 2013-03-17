using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class BudgetProject
    {
        public IEmployeDataProvider EmployeDataProvider { get; set; }

        public BudgetProject()
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

        public static BudgetProject Create(IDataReader record)
        {
            return new BudgetProject
                {
                    Revision = Convert.ToInt32(record["Revision"]),
                    RevisionDate = Convert.ToDateTime(record["RevisionDate"]),
                    UpdatedPersonId = Convert.ToInt32(record["UpdatedPersonId"]),
                    IsAccepted = Convert.ToBoolean(record["IsAccepted"]),
                };
        }

        public SqlParameter[] SqlParameters
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

                return sqlParams.ToArray();
            }
        }
    }
}
