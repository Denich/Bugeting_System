namespace Budget.Services.Helpers
{
    public class DbProcedureSet
    {
        public DbProcedureSet(string selectProcedureName, string selectByIdProcedureName, string updateProcedureName, string deleteByIdProcedureName, string insertProcedureName)
        {
            SelectProcedureName = selectProcedureName;
            SelectByIdProcedureName = selectByIdProcedureName;
            UpdateProcedureName = updateProcedureName;
            DeleteProcedureName = deleteByIdProcedureName;
            InsertProcedureName = insertProcedureName;
        }

        public string SelectProcedureName { get; set; }

        public string SelectByIdProcedureName { get; set; }

        public string UpdateProcedureName { get; set; }

        public string DeleteProcedureName { get; set; }

        public string InsertProcedureName { get; set; }
    }
}