using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models;
using Budget.Web.Models.BudgetModels;
using EmitMapper;
using MoreLinq;

namespace Budget.Web.Helpers.Converters
{
    public static class BudgetConverter
    {
        #region group ComplexBudgetProjects

        #region ProjectItemViews

        public static BudgetProjectItemViewModel ToProjectViewModel(this BudgetCategory obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategory, BudgetProjectItemViewModel>().Map(obj);

            model.Name = obj.Info.Name;

            model.ChildItems = obj.TargetBudgets != null ? obj.TargetBudgets.Select(b => b.ToProjectViewModel()) : null;

            return model;
        }

        public static BudgetProjectItemViewModel ToProjectViewModel(this TargetBudget obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<TargetBudget, BudgetProjectItemViewModel>().Map(obj);

            model.Name = obj.Info.Name;

            model.ChildItems = obj.BudgetItems != null ? obj.BudgetItems.Select(b => b.ToProjectViewModel()) : null;

            return model;
        }

        public static BudgetProjectItemViewModel ToProjectViewModel(this BudgetItem obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetItem, BudgetProjectItemViewModel>().Map(obj);

            model.Name = obj.Info.Name;

            return model;
        }


        public static IList<string> ToValuesModel(this ComplexBudget obj, IEnumerable<BudgetCategory> infosBudgetCategories)
        {
            var values = new List<string>();

            foreach (var category in infosBudgetCategories)
            {
                var foundCategory = obj.FindCategoryByInfoId(category.InfoId);
                values.Add(foundCategory == null ? "-" : foundCategory.Value.ToString(CultureInfo.InvariantCulture));

                foreach (var target in category.TargetBudgets)
                {
                    var foundTarget = obj.FindTargetBudgetByInfoId(target.InfoId);
                    values.Add(foundTarget == null ? "-" : foundTarget.Value.ToString(CultureInfo.InvariantCulture));
                    
                    foreach (var item in target.BudgetItems)
                    {
                        var foundItem = obj.FindBudgetItemByInfoId(item.InfoId);
                        values.Add(foundItem == null ? "-" : foundItem.Value.ToString(CultureInfo.InvariantCulture));    
                    }
                }
            }

            return values;
        }

        #endregion

        #region Convert ParentBudgets

        public static ParentComplexBudgetViewModel ToAdminParentViewModel(this YearComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, ParentComplexBudgetViewModel>().Map(obj);

            model = obj.BaseToParentViewModel(model);
            model.DerivedBudgets = obj.ChildBudgets.Select(b => b.ToViewModel(obj.BudgetCategories));

            model.DerivedBudgets = model.DerivedBudgets.Select(c => { c.Caption = c.AdministrativeUnitName; return c; }).ToList();

            model.Caption = model.AdministrativeUnitName;

            return model;
        }

        public static ParentComplexBudgetViewModel ToAdminParentViewModel(this QuarterComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, ParentComplexBudgetViewModel>().Map(obj);

            model = obj.BaseToParentViewModel(model);
            model.DerivedBudgets = obj.ChildBudgets.Select(b => b.ToViewModel(obj.BudgetCategories));

            model.DerivedBudgets = model.DerivedBudgets.Select(c => { c.Caption = c.AdministrativeUnitName; return c; }).ToList();

            model.Caption = model.AdministrativeUnitName;

            return model;
        }

        public static ParentComplexBudgetViewModel ToAdminParentViewModel(this MonthComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudgetProject, ParentComplexBudgetViewModel>().Map(obj);

            model = obj.BaseToParentViewModel(model);
            model.DerivedBudgets = obj.ChildBudgets.Select(b => b.ToViewModel(obj.BudgetCategories));

            model.DerivedBudgets = model.DerivedBudgets.Select(c => { c.Caption = c.AdministrativeUnitName; return c; }).ToList();

            model.Caption = model.AdministrativeUnitName;

            return model;
        }

        public static ParentComplexBudgetViewModel ToPeriodParentViewModel(this YearComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, ParentComplexBudgetViewModel>().Map(obj);

            model = obj.BaseToParentViewModel(model);
            model.DerivedBudgets = obj.QuarterBudgets.Select(b => b.ToViewModel(obj.BudgetCategories));

            model.DerivedBudgets = model.DerivedBudgets.Select(c => { c.Caption = c.Period; return c; }).ToList();
            model.Caption = model.Period;

            return model;
        }

        public static ParentComplexBudgetViewModel ToPeriodParentViewModel(this QuarterComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, ParentComplexBudgetViewModel>().Map(obj);

            model = obj.BaseToParentViewModel(model);
            model.DerivedBudgets = obj.MonthBudgets.Select(b => b.ToViewModel(obj.BudgetCategories));

            model.DerivedBudgets = model.DerivedBudgets.Select(c => { c.Caption = c.Period; return c; }).ToList();
            model.Caption = model.Period;

            return model;
        }

        public static ParentComplexBudgetViewModel BaseToParentViewModel(this ComplexBudget obj, ParentComplexBudgetViewModel baseModel)
        {
            baseModel.AdministrativeUnitName = obj.AdministrativeUnit.Name;
            baseModel.Period = obj.GetPeriodName();
            baseModel.BudgetItemValues = obj.ToValuesModel(obj.BudgetCategories);
            baseModel.BudgetItems = obj.BudgetCategories.Select(b => b.ToProjectViewModel());
            
            return baseModel;
        }
        #endregion

        #region Convert ComplexBudgets to use items values
        public static CommonComplexBudgetViewModel ToViewModel(this YearComplexBudgetProject obj, IEnumerable<BudgetCategory> parentBudgetCategories)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, CommonComplexBudgetViewModel>().Map(obj);
            return obj.BaseToViewModel(model, parentBudgetCategories);
        }
        
        public static CommonComplexBudgetViewModel ToViewModel(this QuarterComplexBudgetProject obj, IEnumerable<BudgetCategory> parentBudgetCategories)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, CommonComplexBudgetViewModel>().Map(obj);
            return obj.BaseToViewModel(model, parentBudgetCategories);
        }

        public static CommonComplexBudgetViewModel ToViewModel(this MonthComplexBudgetProject obj, IEnumerable<BudgetCategory> parentBudgetCategories)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudgetProject, CommonComplexBudgetViewModel>().Map(obj);
            return obj.BaseToViewModel(model, parentBudgetCategories);
        }

        private static CommonComplexBudgetViewModel BaseToViewModel(this ComplexBudget obj, CommonComplexBudgetViewModel baseModel, IEnumerable<BudgetCategory> parentBudgetCategories)
        {
            baseModel.AdministrativeUnitName = obj.AdministrativeUnit.Name;
            baseModel.Period = obj.GetShortPeriodName();
            baseModel.BudgetItemValues = obj.ToValuesModel(parentBudgetCategories);
            return baseModel;
        }
        #endregion

        public static YearComplexBudgetListViewModel ToListModel(this YearComplexBudgetProject obj)
        {
            if (obj == null)
            {
                return null;
            }
            
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, YearComplexBudgetListViewModel>().Map(obj);
            
            model.Period = obj.GetPeriodName();
            return model;
        }

        public static QuarterComplexBudgetListViewModel ToListModel(this QuarterComplexBudgetProject obj)
        {
            if (obj == null)
            {
                return null;
            }
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, QuarterComplexBudgetListViewModel>().Map(obj);
            model.Period = obj.GetPeriodName();
            return model;
        }

        public static MonthComplexBudgetListViewModel ToListModel(this MonthComplexBudgetProject obj)
        {
            if (obj == null)
            {
                return null;
            }
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudgetProject, MonthComplexBudgetListViewModel>().Map(obj);
            model.Period = obj.GetPeriodName();
            return model;
        }

        #endregion

        public static YearComplexBudgetResultListViewModel ToListModel(this KeyValuePair<YearComplexBudget, YearComplexBudgetProject> obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudget, YearComplexBudgetResultListViewModel>().Map(obj.Key);

            model.ResultBudgetId = obj.Key.Id;
            model.ProjectBudgetId = obj.Value.Id;
            model.TotalCostsPlan = obj.Value.TotalCosts;
            model.TotalIncomePlan = obj.Value.TotalIncome;
            model.Period = obj.Value.GetPeriodName();

            model.ProjectItemsCount = obj.Value.BudgetCategories.Count();

            model.ProcessedItemsCount = obj.Key.BudgetCategories.Count(b => b.Value != 0);

            return model;
        }

        public static QuarterComplexBudgetResultListViewModel ToListModel(this KeyValuePair<QuarterComplexBudget, QuarterComplexBudgetProject> obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudget, QuarterComplexBudgetResultListViewModel>().Map(obj.Key);

            model.ResultBudgetId = obj.Key.Id;
            model.ProjectBudgetId = obj.Value.Id;
            model.TotalCostsPlan = obj.Value.TotalCosts;
            model.TotalIncomePlan = obj.Value.TotalIncome;
            model.Period = obj.Value.GetPeriodName();

            model.ProjectItemsCount = obj.Value.BudgetCategories.Count();

            model.ProcessedItemsCount = obj.Key.BudgetCategories.Count(b => b.Value != 0);

            return model;
        }

        public static MonthComplexBudgetResultListViewModel ToListModel(this KeyValuePair<MonthComplexBudget, MonthComplexBudgetProject> obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudget, MonthComplexBudgetResultListViewModel>().Map(obj.Key);

            model.ResultBudgetId = obj.Key.Id;
            model.ProjectBudgetId = obj.Value.Id;
            model.TotalCostsPlan = obj.Value.TotalCosts;
            model.TotalIncomePlan = obj.Value.TotalIncome;
            model.Period = obj.Value.GetPeriodName();

            model.ProjectItemsCount = obj.Value.BudgetCategories.Count();

            model.ProcessedItemsCount = obj.Key.BudgetCategories.Count(b => b.Value != 0);

            return model;
        }

        public static YearUnapproveBudgetModel ToListModel(this UnapproveYearBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<UnapproveYearBudget, YearUnapproveBudgetModel>().Map(obj);
        }

        public static QuarterUnapproveBudgetModel ToListModel(this UnapproveQuarterBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<UnapproveQuarterBudget, QuarterUnapproveBudgetModel>().Map(obj);
        }

        public static MonthUnapproveBudgetModel ToListModel(this UnapproveMonthBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<UnapproveMonthBudget, MonthUnapproveBudgetModel>().Map(obj);
        }
    }
}