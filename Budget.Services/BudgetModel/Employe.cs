using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class Employe : IDataRetriever<Employe>
    {
        private EmployeContact _contact;
        private readonly string _selectProcedureName;
        private readonly string _selectByIdProcedureName;
        private readonly string _updateProcedureName;
        private readonly string _deleteByIdProcedureName;
        private readonly string _insertProcedureName;

        [Dependency]
        public IEmployeContactDataProvider EmployeContactDataProvider { get; set; }

        public Employe()
        {
            _selectProcedureName = "usp_EmployeSelect";
            _selectByIdProcedureName = "usp_EmployeSelect";
            _updateProcedureName = "usp_EmployeUpdate";
            _deleteByIdProcedureName = "usp_EmployeDelete";
            _insertProcedureName = "usp_EmployeInsert";
            EmployeContactDataProvider = new EmployeContactDataProvider();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public string Position { get; set; }

        public EmployeContact Contact
        {
            get
            {
                return _contact ?? EmployeContactDataProvider.GetEmployeContactById(Id);
            }
            set
            {
                _contact = value;
            }
        }

        public string FullName {
            get
            {
                var fullName = string.Empty;
                
                if (Name != null)
                {
                    fullName += " "+ Name;
                }

                if (SecondName != null)
                {
                    fullName += " " + SecondName;
                }

                if (MiddleName != null)
                {
                    fullName += " " + MiddleName;
                }

                return fullName;
            }
        }

        public Employe Create(IDataRecord record)
        {
            return new Employe()
            {
                Id = Convert.ToInt32(record["Id"]),
                Name = Convert.ToString(record["Name"]),
                SecondName = Convert.ToString(record["SecondName"]),
                MiddleName = Convert.ToString(record["MiddleName"]),
                Position = Convert.ToString(record["Position"]),
            };
        }

        public void FillData(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            SecondName = Convert.ToString(record["SecondName"]);
            MiddleName = Convert.ToString(record["MiddleName"]);
            Position = Convert.ToString(record["Position"]);
        }

        public virtual SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("SecondName", SqlHelper.GetSqlValue(SecondName)),
                        new SqlParameter("MiddleName", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Position", SqlHelper.GetSqlValue(Position)),
                    };

                //note: it budget category info center is new, its id = 0
                if (Id != 0)
                {
                    sqlParams.Add(new SqlParameter("Id", Id));
                }

                return sqlParams.ToArray();
            }
        }

        public string SelectProcedureName
        {
            get { return _selectProcedureName; }
        }

        public string SelectByIdProcedureName
        {
            get { return _selectByIdProcedureName; }
        }

        public string UpdateProcedureName
        {
            get { return _updateProcedureName; }
        }

        public string DeleteByIdProcedureName
        {
            get { return _deleteByIdProcedureName; }
        }

        public string InsertProcedureName
        {
            get { return _insertProcedureName; }
        }
    }
}