//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IngenuityNow.GrowthTracker.ExcelReader
//{
//    public class ExcelReader
//    {
//        public List<Row> Read()
//        {
//            string pathToExcelFile = @"C:\Users\andys\OneDrive\Documents\GBLI Stuff";
//            ConnectionExcel ConxObject = new ConnectionExcel(pathToExcelFile);
//            //Query a worksheet with a header row  
//            var query1 = from a in ConxObject.UrlConnexion.Worksheet<Row>()
//                         select a;

//            return query1.ToList();
//            //foreach (var result in query1)
//            //{
//            //    string products = "ProductId : {0}, ProductName: {1}";
//            //    Console.WriteLine(string.Format(products, result.ProductId, result.ProductName));
//            //}
//            //Console.ReadKey();
//        }
//    }
//}
