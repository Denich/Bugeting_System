using System;
using System.Collections;
using System.Collections.Generic;

namespace Budget.Web.Helpers.Converters
{
    public class BudgetProjectItemViewModel
    {
        public int InfoId { get; set; }

        public string Name { get; set; }

        public IEnumerable<BudgetProjectItemViewModel> ChildItems { get; set; }
    }
}