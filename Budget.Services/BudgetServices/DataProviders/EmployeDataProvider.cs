using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class EmployeDataProvider : IEmployeDataProvider
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public Employe GetEmploye(int employeId)
        {
            return new Employe();
        }
    }
}