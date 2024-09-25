using MySql.Data.MySqlClient;

using OnChessApi.Models;
using OnChessApi.Services;

namespace OnChessApi.Repository
{
    public class MySqlRepository
    {
        private readonly string _connectionString;

        public MySqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteQuery()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT * FROM city";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public bool AddUser(UserModel user)
        {
            string password = null;
            string passwordSalt = null;

            if (string.IsNullOrEmpty(user.LoginProvider))
            {
                CryptService crypt = new();

                string guid = Guid.NewGuid().ToString("N");

                password = crypt.EncryptPassword(user.Password, guid);
                passwordSalt = crypt.Encrypt(guid);
            }

            using (MySqlConnection connection = new(_connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "INSERT INTO users (FirstName, LastName, Email, Password, PasswordSalt, LoginProvider) VALUES (@firstName, @lastName, @email, @password, @passwordSalt, @loginProvider)";

                    using (MySqlCommand command = new(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                        command.Parameters.AddWithValue("@loginProvider", user.LoginProvider);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return false;
        }

        public bool GetUser(string email)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT * FROM users WHERE Email = @email";

                    using (MySqlCommand command = new(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string? password = reader["password"].ToString();
                                string? passwordSalt = reader["PasswordSAlt"].ToString();

                                new CryptService().VerifyPassword("ccc", passwordSalt, password);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return false;
        }
    }
}
