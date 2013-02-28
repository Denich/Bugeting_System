using System;

namespace Budget.Services.BudgetModel
{
    public interface IBudgetProject
    {
        int Revision { get; set; }

        DateTime RevisionDate { get; set; }

        Employe UpdatedPerson { get; set; }

        bool IsAccepted { get; set; }
    }
}