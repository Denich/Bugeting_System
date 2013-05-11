namespace Budget.Web.Models.BudgetModels
{
    public class BudgetYearModel
    {
        public int Year { get; set; }

        public string YearName { get; set; }

        public bool IsAlreadyExist { get; set; }
    }
}