using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSLoginRegisterForm.Connection
{
    class ServerConnection
    {
        public static string stringConnection = "Data Source=.;Initial Catalog=POS&IS;Integrated Security=True";

        public static DataTable executeSQL(string sql)
        {
            SqlConnection connection = new SqlConnection();
            SqlDataAdapter adapter = default(SqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                connection.ConnectionString = stringConnection;
                connection.Open();

                adapter = new SqlDataAdapter(sql, connection);
                adapter.Fill(dt);

                connection.Close();
                connection = null;
                return dt;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occured: " + ex.Message,
                    "SQL Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            return dt;
        }
    }
}
