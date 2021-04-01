using System.Collections.Generic;

namespace pgrid_collection
{

	public class Supplier
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
	}

	public class SupplierBase : List<Supplier>
	{
		public SupplierBase()
			: base()
		{
		}
	}

	public class Product
	{
		public string ProductName { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public double UnitPrice { get; set; }
		public int Quantity { get; set; }
		public SupplierBase Suppliers { get; set; }
	}

	public class ProductList : List<Product>
	{
		public ProductList()
			: base()
		{
			Add(new Product() { ProductName = "Chang", Country = "UK", City = "Cowes", UnitPrice = 19, Quantity = 10, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Gravad lax", Country = "Italy", City = "Reggio Emilia", UnitPrice = 12.5, Quantity = 16, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Ravioli Angelo", Country = "Brazil", City = "Rio de Janeiro", UnitPrice = 19, Quantity = 12, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Tarte au sucre", Country = "Germany", City = "QUICK-Stop", UnitPrice = 22, Quantity = 50, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Steeleye Stout", Country = "USA", City = "Reggio Emilia", UnitPrice = 18, Quantity = 20, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Pavlova", Country = "Austria", City = "Graz", UnitPrice = 21, Quantity = 52, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Longlife Tofu", Country = "USA", City = "Boise", UnitPrice = 7.75, Quantity = 120, Suppliers = new SupplierBase() });
			Add(new Product() { ProductName = "Alice Mutton", Country = "Canada", City = "Tsawwassen", UnitPrice = 44, Quantity = 16, Suppliers = new SupplierBase() });
		}
	}
}
