using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class YearComplexBudget : ComplexBudget
    {
        public YearComplexBudget()
        {
        }

        public YearComplexBudget(YearComplexBudget yearComplexBudget)
            : this(yearComplexBudget.Id, yearComplexBudget.AdministrativeUnitId, yearComplexBudget.Year)
        {
        }

        public YearComplexBudget(int id, int administrativeUnitId, int year)
            : base(id, administrativeUnitId)
        {
            Year = year;
        }

        public int Year { get; set; }

        public static YearComplexBudget Create(IDataReader record)
        {
            return new YearComplexBudget
                {
                    Id = Convert.ToInt32(record["Id"]),
                    AdministrativeUnitId = Convert.ToInt32(record["AdministrativeUnitId"]),
                    Year = Convert.ToInt32(record["Year"]),
                };
        }

        public virtual SqlParameter[] SqlParameters
        {
            get
            {
                var sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter("AdministrativeUnitId", SqlHelper.GetSqlValue(AdministrativeUnitId)),
                        new SqlParameter("Year", SqlHelper.GetSqlValue(Year)),
                    };

                //note: it budget category info center is new, its id = 0
                if (Id != 0)
                {
                    sqlParams.Add(new SqlParameter("Id", Id));
                }

                return sqlParams.ToArray();
            }
        }
    }
}