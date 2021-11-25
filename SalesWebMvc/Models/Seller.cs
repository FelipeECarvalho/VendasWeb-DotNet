using System;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMvc.Models
{
	public class Seller
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Email { get; private set; }
		public DateTime BirthDate { get; private set; }
		public double Salary { get; private set; }
		public ICollection<SalesRecord> SalesRecord { get; private set; } = new List<SalesRecord>();
		public Departament Departament { get; private set; }

		public Seller()
		{
		}

		public Seller(int id, string name, string email, DateTime birthDate, double salary, Departament departament)
		{
			Id = id;
			Name = name;
			Email = email;
			BirthDate = birthDate;
			Salary = salary;
			Departament = departament;
		}

		public void AddSales(SalesRecord sale) 
		{
			SalesRecord.Add(sale);
		}

		public void RemoveSales(SalesRecord sale)
		{
			SalesRecord.Remove(sale);
		}

		public double TotalSales(DateTime initial, DateTime final)
		{
			return SalesRecord.Where(sale => sale.Date >= initial && sale.Date <= final)
						.Sum(sale => sale.Amount);
		}
	}
}
