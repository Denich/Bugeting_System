using Budget.Services.BudgetServices.DataProviderContracts;

namespace Budget.Services.BudgetServices
{
    public interface IBudgetDataManagement
    {
        IFinancialCenterDataProvider FinancialCenters { get; set; }

        ICompanyDataProvider Company { get; set; }

        IBudgetCategoryDataProvider BudgetCategories { get; set; }

        IBudgetCategoryInfoDataProvider BudgetCategoryInfos { get; set; }

        IBudgetItemDataProvider BudgetItems { get; set; }

        IBudgetItemInfoDataProvider BudgetItemInfos { get; set; }

        IYearComplexBudgetDataProvider YearComplexBudgets { get; set; }

        IYearComplexBudgetProjectDataProvider YearComplexBudgetProjects { get; set; }

        IEmployeDataProvider Employes { get; set; }

        IEmployeContactDataProvider EmployeContacts { get; set; }

        IMonthComplexBudgetDataProvider MonthComplexBudgets { get; set; }

        IMonthComplexBudgetProjectDataProvider MonthComplexBudgetProjects { get; set; }

        IQuarterComplexBudgetDataProvider QuarterComplexBudgets { get; set; }

        IQuarterComplexBudgetProjectDataProvider QuarterComplexBudgetProjects { get; set; }

        ITargetBudgetDataProvider TargetBudgets { get; set; }

        ITargetBudgetInfoDataProvider TargetBudgetInfos { get; set; }
    }
}
