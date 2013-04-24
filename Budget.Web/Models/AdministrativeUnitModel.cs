namespace Budget.Web.Models
{
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