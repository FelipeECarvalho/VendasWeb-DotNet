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
		public Department Department { get; private set; }

		public Seller()
		{
		}

		public Seller(string name, string email, DateTime birthDate, double salary, Department department)
		{
			Name = name;
			Email = email;
			BirthDate = birthDate;
			Salary = salary;
			Department = department;
		}

		public Seller(int id, string name, string email, DateTime birthDate, double salary, Department departament)
		{
			Id = id;
			Name = name;
			Email = email;
			BirthDate = birthDate;
			Salary = salary;
			Department = departament;
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
