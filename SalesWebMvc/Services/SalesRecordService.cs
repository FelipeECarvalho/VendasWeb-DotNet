using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
	public class SalesRecordService
	{
		private readonly SalesWebMvcContext _context;

		public SalesRecordService(SalesWebMvcContext context)
		{
			_context = context;
		}

		public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initialDate, DateTime? finalDate) 
		{
			var result = from sales in _context.SalesRecord select sales;

			if (initialDate.HasValue)
			{
				result = result.Where(x => x.Date >= initialDate);
			}

			if (finalDate.HasValue) 
			{
				result = result.Where(x => x.Date <= finalDate);
			}

			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync();
		}

		public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? initialDate, DateTime? finalDate)
		{
			var result = from sales in _context.SalesRecord select sales;

			if (initialDate.HasValue)
			{
				result = result.Where(x => x.Date >= initialDate);
			}

			if (finalDate.HasValue)
			{
				result = result.Where(x => x.Date <= finalDate);
			}

			var data = await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync(); 

			return data.GroupBy(x => x.Seller.Department).ToList();
		}


	}
}
