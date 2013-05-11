using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public abstract class AdministrativeUnit
    {
        private Employe _director;

        [InjectionConstructor]
        protected AdministrativeUnit() {}

        protected AdministrativeUnit(string name)
            : this(0, name)
        {
            
        }

        protected AdministrativeUnit(int id, string name) :this()
        {
            Id = id;
            Name = name;
        }

        [Dependency]
        public IEmployeDataProvider EmployeDataProvider { get; set; }

        public int DirectorId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Employe Director
        {
            get
            {
                return _director ?? EmployeDataProvider.Get(DirectorId);
            }
            set
            {
                _director = value;

                DirectorId = value.Id;
            }
        }

        public string Adress { get; set; }

        public string Phone { get; set; }
    }
}