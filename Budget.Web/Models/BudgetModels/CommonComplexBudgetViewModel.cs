using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Budget.Web.Models.BudgetModels
{
    public class CommonComplexBudgetViewModel
    {
        public int Id { get; set; }

        public int AdministrativeUnitId { get; set; }

        public string AdministrativeUnitName { get; set; }

        public string Period { get; set; }

        public string Caption { get; set; }

        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

        public bool IsFinal { get; set; }

        public IList<string> BudgetItemValues { get; set; }

        public double TotalIncome { get; set; }

        public double TotalCosts { get; set; }

        public double Balance { get; set; }
    }
}