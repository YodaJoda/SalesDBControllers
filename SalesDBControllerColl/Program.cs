using DBClassCollectionLib;
using System;

namespace SalesDBControllerColl
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlconn = new Connection("localhost\\sqlexpress", "SalesDB");

            var OLCont = new OrderLinesController(sqlconn);

            var orderline1 = new OrderLine();
            orderline1.OrdersId = 4;
            orderline1.Product = "new product";
            orderline1.Description = "new desc.";
            orderline1.Price = 400;
            orderline1.Quantity = 4;

            OLCont.Create(orderline1);


            var orderlines = OLCont.GetAll();
        }
    }
}
