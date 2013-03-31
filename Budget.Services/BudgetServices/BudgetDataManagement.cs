using Budget.Services.BudgetServices.DataProviderContracts;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices
{
    public class BudgetDataManagement : IBudgetDataManagement
    {
        [InjectionConstructor]
        public BudgetDataManagement(ICompanyDataProvider company,
                                    IFinancialCenterDataProvider financialCenters,
                                    IBudgetCategoryDataProvider budgetCategories,
                                    IBudgetCategoryInfoDataProvider budgetCategoryInfos,
                                    IBudgetItemDataProvider budgetItems, IBudgetItemInfoDataProvider budgetItemInfos,
                                    IYearComplexBudgetDataProvider yearComplexBudgets,
                                    IYearComplexBudgetProjectDataProvider yearComplexBudgetProjects,
                                    IEmployeDataProvider employes,
                                    IEmployeContactDataProvider employeContacts,
                                    IMonthComplexBudgetProjectDataProvider monthComplexBudgetProjects,
                                    IQuarterComplexBudgetDataProvider quarterComplexBudgets,
                                    IQuarterComplexBudgetProjectDataProvider quarterComplexBudgetProjects,
                                    ITargetBudgetDataProvider targetBudgets,
                                    ITargetBudgetInfoDataProvider targetBudgetInfos, 
                                    IMonthComplexBudgetDataProvider monthComplexBudgets)
        {
            Company = company;
            FinancialCenters = financialCenters;
            BudgetCategories = budgetCategories;
            BudgetCategoryInfos = budgetCategoryInfos;
            BudgetItems = budgetItems;
            BudgetItemInfos = budgetItemInfos;
            YearComplexBudgets = yearComplexBudgets;
            YearComplexBudgetProjects = yearComplexBudgetProjects;
            Employes = employes;
            EmployeContacts = employeContacts;
            MonthComplexBudgets = monthComplexBudgets;
            MonthComplexBudgetProjects = monthComplexBudgetProjects;
            QuarterComplexBudgets = quarterComplexBudgets;
            QuarterComplexBudgetProjects = quarterComplexBudgetProjects;
            TargetBudgets = targetBudgets;
            TargetBudgetInfos = targetBudgetInfos;
        }

        public IFinancialCenterDataProvider FinancialCenters { get; set; }

        public ICompanyDataProvider Company { get; set; }

        public IBudgetCategoryDataProvider BudgetCategories { get; set; }

        public IBudgetCategoryInfoDataProvider BudgetCategoryInfos { get; set; }

        public IBudgetItemDataProvider BudgetItems { get; set; }

        public IBudgetItemInfoDataProvider BudgetItemInfos { get; set; }

        public IYearComplexBudgetDataProvider YearComplexBudgets { get; set; }

        public IYearComplexBudgetProjectDataProvider YearComplexBudgetProjects { get; set; }

        public IEmployeDataProvider Employes { get; set; }

        public IEmployeContactDataProvider EmployeContacts { get; set; }

        public IMonthComplexBudgetDataProvider MonthComplexBudgets { get; set; }

        public IMonthComplexBudgetProjectDataProvider MonthComplexBudgetProjects { get; set; }

        public IQuarterComplexBudgetDataProvider QuarterComplexBudgets { get; set; }

        public IQuarterComplexBudgetProjectDataProvider QuarterComplexBudgetProjects { get; set; }

        public ITargetBudgetDataProvider TargetBudgets { get; set; }

        public ITargetBudgetInfoDataProvider TargetBudgetInfos { get; set; }
    }
}