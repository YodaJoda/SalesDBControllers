using System;

namespace DBClassCollectionLib
{
    public class OrderLine
    {
        public static Connection connection { get; set; }
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order order { get; set; }

    }
}
