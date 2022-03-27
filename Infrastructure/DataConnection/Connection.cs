using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace Infrastructure.DataConnection
{
    public static class Connection
    {
        public static SqlConnection DbConnection()
        {

            var Server = AppSettings.GetValues("ConnectionStrings:Server");
            var Database = AppSettings.GetValues("ConnectionStrings:Database");
            var User = AppSettings.GetValues("ConnectionStrings:User");
            var Pass = AppSettings.GetValues("ConnectionStrings:Password");

            var db = ContextAccessor.Current.Session.GetString("Db");
            Database = !string.IsNullOrEmpty(db) ? db : Database;

            string connectionString = $"Server={Server}" +
                $";Database={Database};" +
                $"User={User};Password={Pass};" +
                $"Integrated Security=False;MultipleActiveResultSets=True";

            return new SqlConnection(connectionString);
        }
    }
}
