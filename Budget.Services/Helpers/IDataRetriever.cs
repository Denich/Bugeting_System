using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;

namespace Budget.Services.Helpers
{
    public interface IDataRetriever<out T>
    {
        ICollection<SqlParameter> InsertSqlParameters { get; }

        ICollection<SqlParameter> UpdateSqlParameters { get; }

        T Setup(IDataRecord record);
    }
}
