using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudget : ComplexBudget
    {
        public int QuarterNumber { get; set; }

        public int Year { get; set; }
    }
}
