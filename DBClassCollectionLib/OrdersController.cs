using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBClassCollectionLib
{
    public class OrdersController
    {
        public static Connection connection { get; set; }

        public OrdersController(Connection connection)
        {
            OrdersController.connection = connection;
        }

        public bool AddParameters(SqlCommand cmd, Order order)
        {
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@Date", order.Date);
            cmd.Parameters.AddWithValue("@Description", order.Description);

            return (cmd.ExecuteNonQuery() == 1);
        }

        public bool Create(Order order)
        {
            var sql = "INSERT into Orders; " +
                "(CustomerId, Date, Description ) " +
                "VALUES " +
                $"(@CustomerId, @Date, @Description);";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            return AddParameters(cmd, order);
        }

        public bool Delete(int id)
        {
            var sql = $"REMOVE * from Order where Id = @id";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", id);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }

        public bool Update(Order order)
        {
            var sql = $"UPDATE Order " +
                $"VALUES " +
                $"Id = @id, CustomerId = @CustomerId, Date = @Date, Description = @Description " +
                $"Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", order.Id);
            AddParameters(cmd, order);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }

        public Order ReadFromSQL(SqlDataReader reader)
        {
            var order = new Order()
            {
                Id = Convert.ToInt32(reader["Id"]),
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                Date = Convert.ToDateTime(reader["Date"]),
                Description = Convert.ToString(reader["Description"])

            };
            return order;
        }

        public List<Order> GetAll()
        {
            var sql = $"SELECT * on Order; ";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var orders = new List<Order>();
            while (reader.Read())
            {
                orders.Add(ReadFromSQL(reader));
            }
            reader.Close();
            return orders;
        }

        public Order GetbyPK(int id)
        {
            var sql = $"SELECT * on Order Where id = @id; ";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("@id", id);
            reader.Read();
            var order = ReadFromSQL(reader);
            reader.Close();
            return (order);
        }

    }
}
