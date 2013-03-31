using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Microsoft.Practices.Unity;

namespace Budget.Services.Helpers
{
    public static class IocContainerExtenssions
    {
        //todo: parse this from xml file
        public static UnityContainer SetDefaults(this UnityContainer unityContainer)
        {
            unityContainer.RegisterType<BudgetCategoryInfo>();
            unityContainer.RegisterType<IBudgetClient, BudgetClient>();
            unityContainer.RegisterType<IBudgetDataManagement, BudgetDataManagement>();
            unityContainer.RegisterType<IAdministrativeUnitDataProvider, AdministrativeUnitDataProvider>();

            unityContainer.RegisterType<IQuarterComplexBudgetProjectDataProvider, QuarterComplexBudgetProjectDataProvider>();
            unityContainer.RegisterInstance("QuarterComplexBudgetProjectProcedures",
                                            new DbProcedureSet("usp_QuarterComplexBudgetProjectSelect", "usp_QuarterComplexBudgetProjectSelect",
                                                               "usp_QuarterComplexBudgetProjectUpdate",
                                                               "usp_QuarterComplexBudgetProjectDelete", "usp_QuarterComplexBudgetProjectInsert"));


            unityContainer.RegisterType<IQuarterComplexBudgetDataProvider, QuarterComplexBudgetDataProvider>();
            unityContainer.RegisterInstance("QuarterComplexBudgetProcedures",
                                            new DbProcedureSet("usp_QuarterComplexBudgetSelect", "usp_QuarterComplexBudgetSelect",
                                                               "usp_QuarterComplexBudgetUpdate",
                                                               "usp_QuarterComplexBudgetDelete", "usp_QuarterComplexBudgetInsert"));

            unityContainer.RegisterType<IMonthComplexBudgetProjectDataProvider, MonthComplexBudgetProjectDataProvider>();
            unityContainer.RegisterInstance("MonthComplexBudgetProjectProcedures",
                                            new DbProcedureSet("usp_MonthComplexBudgetProjectSelect", "usp_MonthComplexBudgetProjectSelect",
                                                               "usp_MonthComplexBudgetProjectUpdate",
                                                               "usp_MonthComplexBudgetProjectDelete", "usp_MonthComplexBudgetProjectInsert"));


            unityContainer.RegisterType<IMonthComplexBudgetDataProvider, MonthComplexBudgetDataProvider>();
            unityContainer.RegisterInstance("MonthComplexBudgetProcedures",
                                            new DbProcedureSet("usp_MonthComplexBudgetSelect", "usp_MonthComplexBudgetSelect",
                                                               "usp_MonthComplexBudgetUpdate",
                                                               "usp_MonthComplexBudgetDelete", "usp_MonthComplexBudgetInsert"));

            unityContainer.RegisterType<IYearComplexBudgetProjectDataProvider, YearComplexBudgetProjectDataProvider>();
            unityContainer.RegisterInstance("YearComplexBudgetProjectProcedures",
                                            new DbProcedureSet("usp_YearComplexBudgetProjectSelect", "usp_YearComplexBudgetProjectSelect",
                                                               "usp_YearComplexBudgetProjectUpdate",
                                                               "usp_YearComplexBudgetProjectDelete", "usp_YearComplexBudgetProjectInsert"));

            unityContainer.RegisterType<IYearComplexBudgetDataProvider, YearComplexBudgetDataProvider>();
            unityContainer.RegisterInstance("YearComplexBudgetProcedures",
                                            new DbProcedureSet("usp_YearComplexBudgetSelect", "usp_YearComplexBudgetSelect",
                                                               "usp_YearComplexBudgetUpdate",
                                                               "usp_YearComplexBudgetDelete", "usp_YearComplexBudgetInsert"));


            unityContainer.RegisterType<IBudgetCategoryDataProvider, BudgetCategoryDataProvider>();
            unityContainer.RegisterInstance("BudgetCategoryProcedures",
                                            new DbProcedureSet("usp_BudgetCategorySelect", "usp_BudgetCategorySelect",
                                                               "usp_BudgetCategoryUpdate",
                                                               "usp_BudgetCategoryDelete", "usp_BudgetCategoryInsert"));


            unityContainer.RegisterType<IBudgetCategoryInfoDataProvider, BudgetCategoryInfoDataProvider>();
            unityContainer.RegisterInstance("BudgetCategoryInfoProcedures",
                                            new DbProcedureSet("usp_BudgetCategoryInfoSelect", "usp_BudgetCategoryInfoSelect",
                                                               "usp_BudgetCategoryInfoUpdate",
                                                               "usp_BudgetCategoryInfoDelete", "usp_BudgetCategoryInfoInsert"));

            unityContainer.RegisterType<ITargetBudgetInfoDataProvider, TargetBudgetInfoDataProvider>();
            unityContainer.RegisterInstance("TargetBudgetInfoProcedures",
                                            new DbProcedureSet("usp_TargetBudgetInfoSelect", "usp_TargetBudgetInfoSelect",
                                                               "usp_TargetBudgetInfoUpdate",
                                                               "usp_TargetBudgetInfoDelete", "usp_TargetBudgetInfoInsert"));

            unityContainer.RegisterType<IBudgetItemInfoDataProvider, BudgetItemInfoDataProvider>();
            unityContainer.RegisterInstance("BudgetItemInfoProcedures",
                                            new DbProcedureSet("usp_BudgetItemInfoSelect", "usp_BudgetItemInfoSelect",
                                                               "usp_BudgetItemInfoUpdate",
                                                               "usp_BudgetItemInfoDelete", "usp_BudgetItemInfoInsert"));

            unityContainer.RegisterType<ICompanyDataProvider, CompanyDataProvider>();
            unityContainer.RegisterInstance("CompanyProcedures",
                                            new DbProcedureSet("usp_CompanySelect", "usp_CompanySelect",
                                                               "usp_CompanyUpdate",
                                                               "usp_CompanyDelete", "usp_CompanyInsert"));


            unityContainer.RegisterType<IFinancialCenterDataProvider, FinancialCenterDataProvider>();
            unityContainer.RegisterInstance("FinancialCenterProcedures",
                                            new DbProcedureSet("usp_FinancialCenterSelect", "usp_FinancialCenterSelect",
                                                               "usp_FinancialCenterUpdate",
                                                               "usp_FinancialCenterDelete", "usp_FinancialCenterInsert"));


            unityContainer.RegisterType<IBudgetCategoryDataProvider, BudgetCategoryDataProvider>();
            unityContainer.RegisterInstance("BudgetCategoryProcedures",
                                            new DbProcedureSet("usp_BudgetCategorySelect", "usp_BudgetCategorySelect",
                                                               "usp_BudgetCategoryUpdate",
                                                               "usp_BudgetCategoryDelete", "usp_BudgetCategoryInsert"));

            unityContainer.RegisterType<IEmployeContactDataProvider, EmployeContactDataProvider>();
            unityContainer.RegisterInstance("EmployeContactProcedures",
                                            new DbProcedureSet("usp_EmployeContactSelect", "usp_EmployeContactSelect",
                                                               "usp_EmployeContactUpdate",
                                                               "usp_EmployeContactDelete", "usp_EmployeContactInsert"));

            unityContainer.RegisterType<IBudgetItemDataProvider, BudgetItemDataProvider>();
            unityContainer.RegisterInstance("BudgetItemProcedures",
                                            new DbProcedureSet("usp_BudgetItemSelect", "usp_BudgetItemSelect",
                                                               "usp_BudgetItemUpdate",
                                                               "usp_BudgetItemDelete", "usp_BudgetItemInsert"));

            unityContainer.RegisterType<ITargetBudgetDataProvider, TargetBudgetDataProvider>();
            unityContainer.RegisterInstance("TargetBudgetProcedures",
                                            new DbProcedureSet("usp_TargetBudgetSelect", "usp_TargetBudgetSelect",
                                                               "usp_TargetBudgetUpdate",
                                                               "usp_TargetBudgetDelete", "usp_TargetBudgetInsert"));

            unityContainer.RegisterType<IEmployeDataProvider, EmployeDataProvider>();
            unityContainer.RegisterInstance("EmployeProcedures",
                                            new DbProcedureSet("usp_EmployeSelect", "usp_EmployeSelect",
                                                               "usp_EmployeUpdate",
                                                               "usp_EmployeDelete", "usp_EmployeInsert"));
            return unityContainer;
        }
    }
}
