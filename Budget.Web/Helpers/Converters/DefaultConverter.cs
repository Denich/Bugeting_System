using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class DefaultConverter
    {
        public static FinancialCenterModel ToModel(this FinancialCenter obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<FinancialCenter, FinancialCenterModel>().Map(obj);

            model.DirectorName = obj.Director != null ? obj.Director.FullName : null;

            return model;
        }

        public static FinancialCenter ToObj(this FinancialCenterModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<FinancialCenterModel, FinancialCenter>().Map(obj);
        }

        public static AdministrativeUnitModel ToModel(this AdministrativeUnit obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<AdministrativeUnit, AdministrativeUnitModel>().Map(obj);

            model.DirectorName = obj.Director != null ? obj.Director.FullName : null;

            return model;
        }

        public static AdministrativeUnit ToObj(this AdministrativeUnitModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<AdministrativeUnitModel, AdministrativeUnit>().Map(obj);
        }
    }
}