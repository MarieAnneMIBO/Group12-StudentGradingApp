using System.Data.SQLite;

namespace Project_1C_
{
    public class Database
    {
        private static string connectionString = "Data Source=database.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void Initialize()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string queryUsers = @"CREATE TABLE IF NOT EXISTS users (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        username TEXT UNIQUE,
                                        password TEXT
                                      );";

                using (var cmd = new SQLiteCommand(queryUsers, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
