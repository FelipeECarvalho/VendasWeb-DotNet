using System;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models
{
	public class SalesRecord
	{
		public int Id { get; private set; }
		public DateTime Date { get; private set; }
		public double Amount { get; private set; }
		public SaleStatus Status { get; set; }
		public Seller Seller { get; private set; }

		public SalesRecord()
		{
		}

		public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
		{
			Id = id;
			Date = date;
			Amount = amount;
			Status = status;
			Seller = seller;
		}
	}
}
