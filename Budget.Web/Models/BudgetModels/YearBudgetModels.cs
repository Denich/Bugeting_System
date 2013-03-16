using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class YearComplexBudgetModel
    {
        public int Id { get; set; }

        public int AdministrativeUnitId { get; set; }

        public string AdminUnitName { get; set; }

        public int Year { get; set; }

        public double TotalIncome { get; set; }

        public double TotalCosts { get; set; }

        public double Balance { get; set; }
    }
}