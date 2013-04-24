namespace Budget.Web.Models
{
    public class BaseBudgetListViewModel
    {
        public int Id { get; set; }

        public int AdministrativeUnitId { get; set; }

        public double TotalIncome { get; set; }

        public double TotalCosts { get; set; }

        public double Balance { get; set; }
    }
}