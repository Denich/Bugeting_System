using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;

namespace Budget.Web.Models
{
    public class FinancialCenterSelectModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FinancialCenterType Type { get; set; }

        public bool IsSeleceted { get; set; }
    }
}