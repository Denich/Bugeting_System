﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataServices
{
    public class ComlexBudgetDatabaseService
    {
        public YearComplexBudget GetYearComplexBudget(string administrativeUnitName, int year, BudgetType type)
        {
            throw new NotImplementedException();
        }

        public QuarterComplexBudget GetQuarterComplexBudget(string administrativeUnitName, int year, int quarter, BudgetType type)
        {
            throw new NotImplementedException();
        }

        public MonthComplexBudget GetMonthComplexBudget(string administrativeUnitName, int year, int month, BudgetType type)
        {
            throw new NotImplementedException();
        }
    }
}