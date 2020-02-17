using System;
using System.Linq;
using Task.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			var dataSource = new DataSource();


			var customers = dataSource.Customers;
			var suppliers = dataSource.Suppliers;
			var product = dataSource.Products;


			//заказы первого клиента
			var ordersFirstCustomer = customers.First().Orders;

			//сумма первого заказа у первого клиента
            
			var summFirstOrderOfOFirstCustomer = customers.First().Orders.First().Total;

            Console.WriteLine("Список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит 5000.");
            IEnumerable<Customer> customer = customers
                .Where(n => n.Orders.Sum(x => x.Total) > 5000);
            foreach (var list in customer)
            {
                Console.WriteLine(list.CompanyName);
            }
            Console.WriteLine("Список всех клиентов, чей заказ превосходит 500.");
            IEnumerable<Customer> customer1 = customers
               .Where(n => n.Orders.All(x=>x.Total>500));
            foreach (var list in customer1)
            {
                Console.WriteLine(list.CompanyName);
            }
            Console.WriteLine("Список поставщиков, находящихся в той же стране и том же городе, что и клиент.");
            foreach (var list in customers)
            {
                Console.WriteLine($"{list.CompanyName} c этим клиентом находяться:");
                IEnumerable<Supplier> supplier = suppliers
                    .Where(n => n.Country == list.Country && n.City == list.City);
               
                foreach (var list1 in supplier)
                {
                    Console.WriteLine(list1.SupplierName);
                }
            }
            Console.WriteLine("Список клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора.");
            IEnumerable<Customer> customers2 = customers
                .Where(n=>(!int.TryParse(n.PostalCode, out int d))||(n.Region==null)||!(n.Phone.Contains("(")));
            foreach (var list in customers2)
            {
                Console.WriteLine(list.CompanyName);
            }
            Console.WriteLine("группы товаров.");
            var cheapPrise = 30;
            var middlePrise = 50;
            var products = product
                .OrderBy(n => n.UnitPrice)
                .GroupBy(n => n.UnitPrice <= cheapPrise ? "дешевые" : n.UnitPrice <= middlePrise ? "средние" : "дорогие");
            foreach (var group in products)
            {
                Console.WriteLine($"Группа {group.Key}");
                foreach (var list in group)
                {
                    Console.WriteLine($"Название: {list.ProductName} Цена:{list.UnitPrice}");
                }
            }

        }
    }
}