using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models;
using Budget.Web.Models.BudgetModels;
using EmitMapper;

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

        #endregion
        
        public static YearComplexBudgetViewModel ToViewModel(this YearComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, YearComplexBudgetViewModel>().Map(obj);

            model.BudgetItems = obj.BudgetCategories != null
                                    ? obj.BudgetCategories.Select(b => b.ToProjectViewModel())
                                    : null;

            return model;
        }

        public static QuarterComplexBudgetViewModel ToViewModel(this QuarterComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, QuarterComplexBudgetViewModel>().Map(obj);

            model.BudgetItems = obj.BudgetCategories != null
                        ? obj.BudgetCategories.Select(b => b.ToProjectViewModel())
                        : null;

            return model;
        }

        public static MonthComplexBudgetViewModel ToViewModel(this MonthComplexBudgetProject obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudgetProject, MonthComplexBudgetViewModel>().Map(obj);

            model.BudgetItems = obj.BudgetCategories != null
            ? obj.BudgetCategories.Select(b => b.ToProjectViewModel())
            : null;

            return model;
        }

        public static YearComplexBudgetListViewModel ToListModel(this YearComplexBudgetProject obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudgetProject, YearComplexBudgetListViewModel>().Map(obj);
        }

        public static QuarterComplexBudgetListViewModel ToListModel(this QuarterComplexBudgetProject obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudgetProject, QuarterComplexBudgetListViewModel>().Map(obj);
        }

        public static MonthComplexBudgetListViewModel ToListModel(this MonthComplexBudgetProject obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudgetProject, MonthComplexBudgetListViewModel>().Map(obj);
        }

        #endregion

        public static YearComplexBudgetResultListViewModel ToListModel(this YearComplexBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<YearComplexBudget, YearComplexBudgetResultListViewModel>().Map(obj);
        }

        public static QuarterComplexBudgetResultListViewModel ToListModel(this QuarterComplexBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<QuarterComplexBudget, QuarterComplexBudgetResultListViewModel>().Map(obj);
        }

        public static MonthComplexBudgetResultListViewModel ToListModel(this MonthComplexBudget obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<MonthComplexBudget, MonthComplexBudgetResultListViewModel>().Map(obj);
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