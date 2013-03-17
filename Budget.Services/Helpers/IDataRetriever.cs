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
        string SelectProcedureName { get; }

        string SelectByIdProcedureName { get; }

        string UpdateProcedureName { get; }

        string DeleteByIdProcedureName { get; }

        string InsertProcedureName { get; }

        SqlParameter[] SqlParameters { get; }

        T Create(IDataRecord record);
    }
}
