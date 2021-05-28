using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBClassCollectionLib
{
    public class OrderLinesController
    {
        public static Connection connection { get; set; }

        public OrderLinesController(Connection connection)
        {
            OrderLinesController.connection = connection;
        }

        public bool AddParameters(SqlCommand cmd, OrderLine orderLine)
        {
            cmd.Parameters.AddWithValue("@OrdersId", orderLine.OrdersId);
            cmd.Parameters.AddWithValue("@Product", orderLine.Product);
            cmd.Parameters.AddWithValue("@Description", orderLine.Description);
            cmd.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
            cmd.Parameters.AddWithValue("@Price", orderLine.Price);
            return (cmd.ExecuteNonQuery() == 1);
        }

        public bool Create(OrderLine orderLine)
        {
            var sql = "INSERT into OrderLines; " +
                "(OrdersId, Product, Description, Quantity, Price ) " +
                "VALUES " +
                $"(@OrdersId, @Product, @Description, @Quantity, @Price);";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            return AddParameters(cmd, orderLine);
        }

        public bool Delete(int id)
        {
            var sql = $"REMOVE * from OrderLines where Id = @id";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", id);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }

        public bool Update(OrderLine orderLine)
        {
            var sql = $"UPDATE OrderLines " +
                $"VALUES " +
                $"Id = @id, OrdersId = @OrdersId, Product = @Product, Description = @Description, Quantity = @Quantity, Price = @Price " +
                $"Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", orderLine.Id);
            AddParameters(cmd, orderLine);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }

        public OrderLine ReadFromSQL(SqlDataReader reader)
        {
            var orderLine = new OrderLine()
            {
                Id = Convert.ToInt32(reader["Id"]),
                OrdersId = Convert.ToInt32(reader["OrdersId"]),
                Product = Convert.ToString(reader["Product"]),
                Description = Convert.ToString(reader["Description"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                Price = Convert.ToDecimal(reader["Price"])
            };
            return orderLine;
        }

        public List<OrderLine> GetAll()
        {
            var sql = $"SELECT * on OrderLines; ";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var orderLines = new List<OrderLine>();
            while (reader.Read())
            {
                orderLines.Add(ReadFromSQL(reader));
            }
            reader.Close();
            return orderLines;
        }

        public OrderLine GetbyPK(int id)
        {
            var sql = $"SELECT * on OrderLines Where id = @id; ";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("@id", id);
            reader.Read();
            var orderLine = ReadFromSQL(reader);
            reader.Close();
            return (orderLine);
        }

        public void GetOrder(OrderLine orderLine)
        {
            var ordersController = new OrdersController(connection);
            orderLine.order = (ordersController.GetbyPK(orderLine.OrdersId));


        }
        private void GetOrderForAll (List<OrderLine> orderLines)
        {
            foreach (var i in orderLines)
            {
                GetOrder(i);
            }
        }








    }
}
