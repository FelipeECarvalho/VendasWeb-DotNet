using System;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMvc.Models
{
	public class Seller
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public double Salary { get; set; }
		public int DepartmentId { get; set; }
		public ICollection<SalesRecord> SalesRecord { get; set; } = new List<SalesRecord>();
		public Department Department { get; set; }

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
