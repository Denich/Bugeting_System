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


        public static BudgetCategoryInfoSelectModel ToSelectModel(this BudgetCategoryInfo obj, int year, int adminUnitId)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfo, BudgetCategoryInfoSelectModel>().Map(obj);

            model.IsAdded = obj.IsUsedInBudgetProject(year, adminUnitId);

            model.Targets = obj.TargetBudgetInfos != null
                                ? model.Targets = obj.TargetBudgetInfos.Select(t => t.ToSelectModel(year, adminUnitId)).ToList()
                                : null;
            return model;
        }

        public static TargetBudgetInfoSelectModel ToSelectModel(this TargetBudgetInfo obj, int year, int adminUnitId)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfo, TargetBudgetInfoSelectModel>().Map(obj);
            model.IsAdded = obj.IsUsedInBudgetProject(year, adminUnitId);

            model.Items = obj.BudgetItemInfos != null
                              ? obj.BudgetItemInfos.Select(i => i.ToSelectModel(year, adminUnitId)).ToList()
                              : null;

            return model;
        }

        public static BudgetItemInfoSelectModel ToSelectModel(this BudgetItemInfo obj, int year, int adminUnitId)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfo, BudgetItemInfoSelectModel>().Map(obj);
            model.IsAdded = obj.IsUsedInBudgetProject(year, adminUnitId);
            return model;
        }

        public static BudgetCategory ToObj(this BudgetCategoryInfoSelectModel model)
        {
            return new BudgetCategory
                {
                    InfoId = model.Id,
                    TargetBudgets = model.Targets != null ? model.Targets.Select(t => t.ToObj()) : null
                };
        }

        public static TargetBudget ToObj(this TargetBudgetInfoSelectModel model)
        {
            return new TargetBudget
                {
                    InfoId = model.Id,
                    BudgetItems = model.Items != null ? model.Items.Select(i => i.ToObj()) : null
                };
        }

        public static BudgetItem ToObj(this BudgetItemInfoSelectModel model)
        {
            return new BudgetItem
                {
                    InfoId = model.Id,
                };
        }
    }
}