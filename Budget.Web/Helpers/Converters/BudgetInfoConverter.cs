using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Web.Models;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class BudgetInfoConverter
    {
        public static BudgetHierarchyEntityModel ToEntityModel(this BudgetCategoryInfo obj)
        {
            var model = ObjectMapperManager.DefaultInstance
                                           .GetMapper<BudgetCategoryInfo, BudgetHierarchyEntityModel>()
                                           .Map(obj);

            if (obj.TargetBudgetInfos != null && obj.TargetBudgetInfos.Any())
            {
                model.ChildEntityes =
                    new List<BudgetHierarchyEntityModel>(obj.TargetBudgetInfos.Select(b => b.ToEntityModel()));
            }

            return model;
        }

        public static BudgetHierarchyEntityModel ToEntityModel(this TargetBudgetInfo obj)
        {
            var model = ObjectMapperManager.DefaultInstance
                                           .GetMapper<TargetBudgetInfo, BudgetHierarchyEntityModel>()
                                           .Map(obj);

            if (obj.BudgetItemInfos != null && obj.BudgetItemInfos.Any())
            {
                model.ChildEntityes =
                    new List<BudgetHierarchyEntityModel>(obj.BudgetItemInfos.Select(b => b.ToEntityModel()));
            }

            return model;
        }

        public static BudgetHierarchyEntityModel ToEntityModel(this BudgetItemInfo obj)
        {
            return ObjectMapperManager.DefaultInstance
                                      .GetMapper<BudgetItemInfo, BudgetHierarchyEntityModel>()
                                      .Map(obj);
        }

        public static BudgetCategoryInfoModel ToModel(this BudgetCategoryInfo obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfo, BudgetCategoryInfoModel>().Map(obj);
        }

        public static BudgetCategoryInfo ToObj(this BudgetCategoryInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfoModel, BudgetCategoryInfo>().Map(obj);
        }

        public static TargetBudgetInfoModel ToModel(this TargetBudgetInfo obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfo, TargetBudgetInfoModel>().Map(obj);
        }

        public static TargetBudgetInfo ToObj(this TargetBudgetInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfoModel, TargetBudgetInfo>().Map(obj);
        }

        public static BudgetItemInfoModel ToModel(this BudgetItemInfo obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfo, BudgetItemInfoModel>().Map(obj);
        }

        public static BudgetItemInfo ToObj(this BudgetItemInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfoModel, BudgetItemInfo>().Map(obj);
        }


        public static BudgetCategoryInfoSelectModel ToSelectModel(this BudgetCategoryInfo obj, YearComplexBudgetProject baseCompanyYearBudget)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfo, BudgetCategoryInfoSelectModel>().Map(obj);

            model.IsAdded = baseCompanyYearBudget.BudgetCategories.Any(c => c.InfoId == model.Id);

            model.Targets = obj.TargetBudgetInfos.Select(t => t.ToSelectModel(baseCompanyYearBudget)).ToList();

            return model;
        }

        public static TargetBudgetInfoSelectModel ToSelectModel(this TargetBudgetInfo obj, YearComplexBudgetProject baseCompanyYearBudget)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfo, TargetBudgetInfoSelectModel>().Map(obj);

            model.IsAdded = baseCompanyYearBudget.BudgetCategories.Any(c => c.TargetBudgets.Any(b => b.InfoId == model.Id));

            model.Items = obj.BudgetItemInfos.Select(i => i.ToSelectModel(baseCompanyYearBudget)).ToList();

            return model;
        }

        public static BudgetItemInfoSelectModel ToSelectModel(this BudgetItemInfo obj, YearComplexBudgetProject baseCompanyYearBudget)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfo, BudgetItemInfoSelectModel>().Map(obj);

            model.IsAdded =
                baseCompanyYearBudget.BudgetCategories.Any(
                    c => c.TargetBudgets.Any(t => t.BudgetItems.Any(i => i.InfoId == model.Id)));
                
            return model;
        }

        public static BudgetCategory ToObj(this BudgetCategoryInfoSelectModel model, IBudgetDataManagement data)
        {
            var obj = data.BudgetCategories.GetTemplate();
            obj.InfoId = model.Id;
            
            if (model.Targets != null)
            {
                obj.TargetBudgets = model.Targets.Where(b => b.IsAdded).Select(t => t.ToObj(data));
            }

            return obj;
        }

        public static TargetBudget ToObj(this TargetBudgetInfoSelectModel model, IBudgetDataManagement data)
        {
            var obj = data.TargetBudgets.GetTemplate();
            obj.InfoId = model.Id;

            if (model.Items != null)
            {
                obj.BudgetItems = model.Items.Where(b => b.IsAdded).Select(i => i.ToObj(data));
            }
            
            return obj;
        }

        public static BudgetItem ToObj(this BudgetItemInfoSelectModel model, IBudgetDataManagement data)
        {
            var obj = data.BudgetItems.GetTemplate();
            obj.InfoId = model.Id;
            return obj;
        }
    }
}