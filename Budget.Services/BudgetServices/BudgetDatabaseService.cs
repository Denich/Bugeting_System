namespace Budget.Services.BudgetServices
{
    public class BudgetDatabaseService
    {
        //Todo: add to resources
        private const string _connString = "user id=sa; password=123asdQ; Data Source=.; Inital Catalog=MyCompany_Database;";


        /*public ComplexBudget GetBudgetTemplate()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connString))
            {
                //create the command    
                using (SqlCommand command = connection.GetCommand("SELECT * FROM dbo.table1 WHERE textBox2 = @textBox2", CommandType.Text))
                {
                    // add the parameter
                    command.AddParameter("@textBox1", TextBox1.Text, SqlDbType.VarChar);
                    command.AddParameter("@textBox2", TextBox2.Text, SqlDbType.VarChar);

                    // initialize the reader and execute the command 
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (!reader.HasRows)
                        {
                            reader.Close();
                            command.CommandText = "INSERT INTO dbo.table1 (textBox1, textBox2) VALUES (@textBox1, @textBox2)";
                            command.ExecuteNonQuery();
                        }
                    }
            }

            return new ComplexBudget();
        }*/
    }
}
