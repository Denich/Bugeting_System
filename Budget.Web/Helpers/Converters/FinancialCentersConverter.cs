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
        public static FinancialCenterSelectModel ToSelectModel(this FinancialCenter obj, YearComplexBudgetProject companyYearBudget)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<FinancialCenter, FinancialCenterSelectModel>().Map(obj);

            var usedAdminUnitsIds = companyYearBudget.ChildBudgets.Select(b => b.AdministrativeUnitId).ToList();

            model.IsSeleceted = usedAdminUnitsIds.Contains(model.Id);

            return model;
        }
    }
}