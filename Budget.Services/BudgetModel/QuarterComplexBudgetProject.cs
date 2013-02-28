using System;

namespace Budget.Services.BudgetModel
{
    public class QuarterComplexBudgetProject : QuarterComplexBudget, IBudgetProject
    {
        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

        public Employe UpdatedPerson { get; set; }

        public bool IsAccepted { get; set; }
    }
}