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

        private CompanyPosition _position;

        [Dependency]
        public IEmployeContactDataProvider EmployeContactDataProvider { get; set; }

        [Dependency]
        public ICompanyPositionDataProvider CompanyPositionDataProvider { get; set; }

        [Dependency]
        public ICompanyDataProvider CompanyDataProvider { get; set; }

        [Dependency]
        public IFinancialCenterDataProvider FinancialCenterDataProvider { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public int PositionId { get; set; }

        public CompanyPosition Position
        {
            get
            {
                return _position ?? CompanyPositionDataProvider.Get(Id);
            }
            set
            {
                _position = value;
            }
        }

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

        public BudgetProjectStatus GetAllowedApproveStatus(int adminUnitId)
        {
            //Todo: remove this stub
            return BudgetProjectStatus.Accepted;

            var company = CompanyDataProvider.Get();

            if (company.Id == adminUnitId && company.DirectorId == Id)
            {
                return BudgetProjectStatus.Accepted;
            }

            var finCenter = FinancialCenterDataProvider.Get(adminUnitId);

            if (finCenter != null && finCenter.Id == adminUnitId && finCenter.DirectorId == Id)
            {
                return BudgetProjectStatus.IntermediatelyAccepted;
            }

            return BudgetProjectStatus.Waiting;
        }

        public bool CanFinallize(int adminUnitId)
        {
            //Todo: Implement checking for can finalize property
            return true;
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
            PositionId = Convert.ToInt32(record["PositionId"]);
            return this;
        }
    }
}