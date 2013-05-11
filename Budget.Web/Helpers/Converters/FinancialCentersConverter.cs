using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class FinancialCentersConverter
    {
        public static FinancialCenterSelectModel ToModel(this FinancialCenter obj, int year)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<FinancialCenter, FinancialCenterSelectModel>().Map(obj);
        }
    }
}