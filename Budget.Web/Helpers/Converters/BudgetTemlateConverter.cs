using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;
using Budget.Web.Models;
using EmitMapper;

namespace Budget.Web.Helpers.Converters
{
    public static class BudgetTemlateConverter
    {
        public static BudgetCategoryInfoModel ToModel(this BudgetCategoryInfo obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfo, BudgetCategoryInfoModel>().Map(obj);

            if (obj.TargetBudgetInfos != null && obj.TargetBudgetInfos.Any())
            {
                model.TargetBudgetInfoModels = new List<TargetBudgetInfoModel>(obj.TargetBudgetInfos.Select(b => b.ToModel()));
            }

            return model;
        }

        public static BudgetCategoryInfo ToObj(this BudgetCategoryInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetCategoryInfoModel, BudgetCategoryInfo>().Map(obj);
        }

        public static TargetBudgetInfoModel ToModel(this TargetBudgetInfo obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfo, TargetBudgetInfoModel>().Map(obj);

            if (obj.BudgetItemInfos != null && obj.BudgetItemInfos.Any())
            {
                model.BudgetItemInfoModels = new List<BudgetItemInfoModel>(obj.BudgetItemInfos.Select(b => b.ToModel()));
            }

            return model;
        }

        public static TargetBudgetInfo ToObj(this TargetBudgetInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<TargetBudgetInfoModel, TargetBudgetInfo>().Map(obj);
        }

        public static BudgetItemInfoModel ToModel(this BudgetItemInfo obj)
        {
            var model = ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfo, BudgetItemInfoModel>().Map(obj);

            return model;
        }

        public static BudgetItemInfo ToObj(this BudgetItemInfoModel obj)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<BudgetItemInfoModel, BudgetItemInfo>().Map(obj);
        }
    }
}