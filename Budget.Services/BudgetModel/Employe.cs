using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public class Employe : IDataRetriever<Employe>
    {
        private EmployeContact _contact;

        [Dependency]
        public IEmployeContactDataProvider EmployeContactDataProvider { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public string Position { get; set; }

        public EmployeContact Contact
        {
            get
            {
                return _contact ?? EmployeContactDataProvider.Get(Id);
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

        public ICollection<SqlParameter> InsertSqlParameters {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("SecondName", SqlHelper.GetSqlValue(SecondName)),
                        new SqlParameter("MiddleName", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Position", SqlHelper.GetSqlValue(Position)),
                    };
            }
        }
        public ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters;
                InsertSqlParameters.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public Employe Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            SecondName = Convert.ToString(record["SecondName"]);
            MiddleName = Convert.ToString(record["MiddleName"]);
            Position = Convert.ToString(record["Position"]);
            return this;
        }
    }
}