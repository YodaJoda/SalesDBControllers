using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Text;

namespace DBClassCollectionLib
{
    public class CustomersController
    {
        private static Connection connection { get; set; }



        private Customer FillVendorFromReader(SqlDataReader reader)
        {
            var customer = new Customer()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                City = Convert.ToString(reader["City"]),
                State = Convert.ToString(reader["State"]),
                Sales = Convert.ToDecimal(reader["Sales"]),
                Active = Convert.ToBoolean(reader["Active"])
            };
            return customer;
        }
        public List<Customer> GetAll()
        {
            var sql = "SELECT * From Customers;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var customers = new List<Customer>();
            while (reader.Read())
            {
                var customer = FillVendorFromReader(reader);
                customers.Add(customer);
            }
            reader.Close();
            return customers;
        }
        public Customer GetByPK(int Id)
        {
            var sql = "SELECT * From Customers Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", Id);
            var reader = cmd.ExecuteReader();
            
            //if (!reader.HasRows)
            //{
            //    reader.Close();
            //    return null;
            //}           
            reader.Read();
            var customer = FillVendorFromReader(reader);
            reader.Close();
            return customer;
        }


        private void LoadSqlParametersFromCustomer(SqlCommand cmd, Customer customer)
        {
            cmd.Parameters.AddWithValue("@name", customer.Name);
            cmd.Parameters.AddWithValue("@city", customer.City);
            cmd.Parameters.AddWithValue("@state", customer.State);
            cmd.Parameters.AddWithValue("@sales", customer.Sales);
            cmd.Parameters.AddWithValue("@active", customer.Active);
        }
            public bool Create(Customer customer)
        {
            var sql = "INSERT into Customers "
                        + " (Name, City, State, Sales, Active) "
                        + " VALUES (@name, @city, @state, @sales, @active);";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            LoadSqlParametersFromCustomer(cmd, customer);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
            public bool Change(Customer customer)
        {
            var sql = "UPDATE into Customers "
                        + " (Name, City, State, Sales, Active) "
                        + " VALUES (@name, @city, @state, @sales, @active);";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", customer.Id);
            LoadSqlParametersFromCustomer(cmd, customer);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

            public bool Remove(int Id)
        {
            var sql = "DELETE into Customers "
                    + "Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", Id);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public CustomersController(Connection connection)
        {
            CustomersController.connection = connection;
        }

        }
}