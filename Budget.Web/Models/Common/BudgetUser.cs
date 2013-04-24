using System.Security.Principal;
using Budget.Services.BudgetModel;

namespace Budget.Web.Models
{
    public class BudgetUser
    {
        public Employe EmployeInfo { get; set; }

        public IPrincipal User { get; set; }
    }
}