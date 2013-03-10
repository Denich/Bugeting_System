using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models
{
    public class BudgetHierarchyEntityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BudgetHierarchyEntityModel> ChildEntityes { get; set; }
    }
}