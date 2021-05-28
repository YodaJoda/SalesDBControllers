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
            var orderlines = OLCont.GetAll();
        }
    }
}
