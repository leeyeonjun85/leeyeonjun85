//namespace RefReturn
//{
//    class Product
//    {
//        private int price = 100;

//        public ref int GetPrice()
//        {
//            return ref price;
//        }

//        public void PrintPrice()
//        {
//            Console.WriteLine($"Price : {price}"); // Price : 100 / Price : 200
//        }
//    }

//    class MainApp
//    {
//        static void Main(string[] args)
//        {
//            Product carrot = new Product();
//            ref int ref_local_price = ref carrot.GetPrice();
//            int normal_local_price = carrot.GetPrice();

//            carrot.PrintPrice();
//            Console.WriteLine($"Ref Local Price :{ref_local_price}"); // Ref Local Price :100
//            Console.WriteLine($"Normal Local Price :{normal_local_price}"); // Normal Local Price :100

//            ref_local_price = 200;

//            carrot.PrintPrice();
//            Console.WriteLine($"Ref Local Price :{ref_local_price}"); // Ref Local Price :200
//            Console.WriteLine($"Normal Local Price :{normal_local_price}"); // Normal Local Price :100

//        }
//    }
//}
