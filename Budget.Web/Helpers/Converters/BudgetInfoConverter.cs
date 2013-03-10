using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
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
    }
}