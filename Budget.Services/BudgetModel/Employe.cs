using System.Data;

namespace Budget.Services.BudgetModel
{
    public class Employe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MidleName { get; set; }

        public CompanyPosition Position { get; set; }

        public EmployeContacts Contact { get; set; }

        public string FullName {
            get
            {
                var fullName = string.Empty;
                
                if (Name != null)
                {
                    fullName += " "+ Name;
                }

                if (SecondName != null)
                {
                    fullName += " " + SecondName;
                }

                if (MidleName != null)
                {
                    fullName += " " + MidleName;
                }

                return fullName;
            }
        }
    }
}