using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Services.BudgetModel;

namespace Budget.Web.Models
{
    public class FinancialCenterModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public int DirectorId { get; set; }

        public string DirectorName { get; set; }

        public FinancialCenterType Type { get; set; }
    }

    public class AdministrativeUnitModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public int DirectorId { get; set; }

        public string DirectorName { get; set; }
    }
}