using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models.BudgetModels;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class BudgetConverter
    {
        public static YearComplexBudgetModel ToModel(this YearComplexBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudget, YearComplexBudgetModel>().Map(obj);
        }

        public static YearComplexBudget ToObj(this YearComplexBudgetModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetModel, YearComplexBudget>().Map(obj);
        }
    }
}