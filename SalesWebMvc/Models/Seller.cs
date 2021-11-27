using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMvc.Models
{
	public class Seller
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "{0} required")]
		[StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
		public string Name { get; set; }


		[EmailAddress(ErrorMessage = "Enter a valid email")]
		[Required(ErrorMessage = "{0} required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }


		[Required(ErrorMessage = "{0} required")]
		[Display(Name = "Birth Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime BirthDate { get; set; }


		[Required(ErrorMessage = "{0} required")]
		[Range(100, 50000, ErrorMessage = "{0} must be from {1} to {2}")]
		[DisplayFormat(DataFormatString = "{0:F2}")]
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
