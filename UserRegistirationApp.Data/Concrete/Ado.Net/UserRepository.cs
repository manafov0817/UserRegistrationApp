using Npgsql; 
using System;
using System.Collections.Generic;
using System.Data;
using UserRegistirationApp.Data.Abstract;
using UserRegistirationApp.Entity.Entities;

namespace UserRegistirationApp.Data.Concrete.Ado.Net
{
    public class UserRepository : IUserRepository
    {      

        public bool Create(User entity)
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                NpgsqlCommand command = new NpgsqlCommand("public.user_insert", connection);

                command.Connection = connection;


                command.Parameters.AddWithValue("name", DbType.String).Value = entity.Name;
                command.Parameters.AddWithValue("surname", DbType.String).Value = entity.Surname;
                command.Parameters.AddWithValue("username", DbType.String).Value = entity.Username;
                command.Parameters.AddWithValue("email", DbType.String).Value = entity.Email;
                command.Parameters.AddWithValue("password", DbType.String).Value = entity.Password;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                result = (int)command.ExecuteScalar() == 1;


                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public bool Delete(int id)
        {

            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                NpgsqlCommand command = new NpgsqlCommand("public.user_delete", connection);

                command.Connection = connection;

                command.Parameters.AddWithValue("_id", DbType.String).Value = id;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                result = Convert.ToInt64(command.ExecuteScalar()) == 1;

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public User GetById(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            connection.Open();

            User result = new User();

            try
            {

                NpgsqlCommand command = new NpgsqlCommand("public.user_select_by_id", connection);

                command.Connection = connection;

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("_userid", DbType.String).Value = id;

                var data = command.ExecuteReader();

                while (data.Read())
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(data[0]),
                        Name = data[1].ToString(),
                        Surname = data[2].ToString(),
                        Username = data[3].ToString(),
                        Email = data[4].ToString(),
                        Password = data[5].ToString(),
                    };
                    result = user;
                }

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public bool Update(User entity)
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("public.user_update", connection);

                command.Connection = connection;

                command.Parameters.AddWithValue("_id", DbType.String).Value = entity.Id;
                command.Parameters.AddWithValue("_name", DbType.String).Value = entity.Name;
                command.Parameters.AddWithValue("_surname", DbType.String).Value = entity.Surname;
                command.Parameters.AddWithValue("_username", DbType.String).Value = entity.Username;
                command.Parameters.AddWithValue("_email", DbType.String).Value = entity.Email;
                command.Parameters.AddWithValue("_password", DbType.String).Value = entity.Password;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                result = (int)command.ExecuteScalar() == 1 ;

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public ICollection<User> GetAll()
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            connection.Open();

            ICollection<User> result = new List<User>();

            try
            {

                NpgsqlCommand command = new NpgsqlCommand();

                command.Connection = connection;

                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "public.user_select";

                var data = command.ExecuteReader();

                while (data.Read())
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(data[0]),
                        Name = data[1].ToString(),
                        Surname = data[2].ToString(),
                        Username = data[3].ToString(),
                        Email = data[4].ToString(),
                        Password = data[5].ToString(),
                    };

                    result.Add(user);

                }

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public bool ExsistsByUserName(string username)
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                NpgsqlCommand command = new NpgsqlCommand("public.username_exsists", connection);

                command.Connection = connection;

                command.Parameters.AddWithValue("_username", DbType.String).Value = username;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                result = Convert.ToInt64(command.ExecuteScalar()) >= 1;

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public bool ExsistsById(int userId)
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                NpgsqlCommand command = new NpgsqlCommand("public.userid_exists", connection);

                command.Connection = connection;

                command.Parameters.AddWithValue("_id", DbType.String).Value = userId;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                result = Convert.ToInt64(command.ExecuteScalar()) >= 1;

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }
    }
}
