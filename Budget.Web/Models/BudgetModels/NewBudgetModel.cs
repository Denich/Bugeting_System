using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class NewBudgetModel
    {
        [DisplayName("Бюджетний рік:")]
        public int SelectedYear { get; set; }

        [DisplayName("Включити квартальні бюджети")]
        public bool GenerateQuarterBudgets { get; set; }

        [DisplayName("Включити місячні бюджети")]
        public bool GenerateMonthBudgets { get; set; }

        public IEnumerable<BudgetYearModel> AllowedYears { get; set; }
    }
}