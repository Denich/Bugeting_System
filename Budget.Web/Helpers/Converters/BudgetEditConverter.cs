using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Web.Models;
using Budget.Web.Models.BudgetModels;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class BudgetEditConverter
    {
        #region edtiModel to obj

        public static BudgetCategory ToObj(this BudgetCategoryEditModel model, IBudgetDataManagement data)
        {
            var obj = data.BudgetCategories.GetTemplate();
            obj.InfoId = model.InfoId;
            obj.Value = model.NewValue;
            obj.Id = model.Id;
            obj.ComplexBudgetId = model.ComplexBudgetId;

            if (model.Targets != null)
            {
                obj.TargetBudgets = model.Targets.Select(t => t.ToObj(data));
            }

            return obj;
        }

        public static TargetBudget ToObj(this TargetBudgetEditModel model, IBudgetDataManagement data)
        {
            var obj = data.TargetBudgets.GetTemplate();
            obj.InfoId = model.InfoId;
            obj.Value = model.NewValue;
            obj.Id = model.Id;
            obj.BudgetCategoryId = model.BudgetCategoryId;

            if (model.Items != null)
            {
                obj.BudgetItems = model.Items.Select(i => i.ToObj(data));
            }

            return obj;
        }

        public static BudgetItem ToObj(this BudgetItemEditModel model, IBudgetDataManagement data)
        {
            var obj = data.BudgetItems.GetTemplate();
            obj.InfoId = model.InfoId;
            obj.Value = model.NewValue;
            obj.Id = model.Id;
            obj.TargetBudgetId = model.TargetBudgetId;
            return obj;
        }

        #endregion

        #region Budgets ToEditModel

        public static ComplexBudgetProjectEditModel ToEditModel(this YearComplexBudget obj)
        {
            var model =
                ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudget, ComplexBudgetProjectEditModel>()
                                   .Map(obj);

            model.Categories = obj.BudgetCategories.Select(c => c.ToEditModel()).ToList();
            model.BaseBudgetId = obj.Id;
            model.Period = obj.GetPeriodName();
            model.AdministrativeUnitName = obj.AdministrativeUnit.Name;

            return model;
        }

        public static ComplexBudgetProjectEditModel ToEditModel(this QuarterComplexBudget obj)
        {
            var model =
                ObjectMapperManager.DefaultInstance
                                   .GetMapper<QuarterComplexBudget, ComplexBudgetProjectEditModel>().Map(obj);

            model.Categories = obj.BudgetCategories.Select(c => c.ToEditModel()).ToList();
            model.BaseBudgetId = obj.Id;
            model.Period = obj.GetPeriodName();
            model.AdministrativeUnitName = obj.AdministrativeUnit.Name;

            return model;
        }

        public static ComplexBudgetProjectEditModel ToEditModel(this MonthComplexBudget obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudget, ComplexBudgetProjectEditModel>()
                                   .Map(obj);

            model.Categories = obj.BudgetCategories.Select(c => c.ToEditModel()).ToList();
            model.BaseBudgetId = obj.Id;
            model.Period = obj.GetPeriodName();
            model.AdministrativeUnitName = obj.AdministrativeUnit.Name;

            return model;
        }

        public static BudgetCategoryEditModel ToEditModel(this BudgetCategory obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategory, BudgetCategoryEditModel>()
                                           .Map(obj);
            
            model.Name = obj.Info.Name;
            model.IsEditable = !obj.TargetBudgets.Any();
            model.NewValue = model.Value;
            model.Targets = obj.TargetBudgets.Select(t => t.ToEditModel()).ToList();

            return model;
        }

        public static TargetBudgetEditModel ToEditModel(this TargetBudget obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<TargetBudget, TargetBudgetEditModel>().Map(obj);

            model.Name = obj.Info.Name;
            model.NewValue = model.Value;
            model.IsEditable = !obj.BudgetItems.Any();
            model.Items = obj.BudgetItems.Select(i => i.ToEditModel()).ToList();

            return model;
        }

        public static BudgetItemEditModel ToEditModel(this BudgetItem obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetItem, BudgetItemEditModel>().Map(obj);
            model.NewValue = model.Value;
            model.Name = obj.Info.Name;

            return model;
        }

        #endregion
    }
}