using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
	public class SalesRecordsController : Controller
	{
		private readonly SalesRecordService _salesService;

		public SalesRecordsController(SalesRecordService salesService)
		{
			_salesService = salesService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> SimpleSearchAsync(DateTime? initialDate, DateTime? finalDate)
		{
			if (!initialDate.HasValue) 
			{
				initialDate = new DateTime(DateTime.Now.Year, 1, 1);
			}
			if (!finalDate.HasValue)
			{
				finalDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			}
			ViewData["initialDate"] = initialDate.Value.ToString("yyyy-MM-dd");
			ViewData["finalDate"] = finalDate.Value.ToString("yyyy/MM/dd");

			var result = await _salesService.FindByDateAsync(initialDate, finalDate);

			return View(result);
		}

		public async Task<IActionResult> GroupingSearch(DateTime? initialDate, DateTime? finalDate)
		{
			if (!initialDate.HasValue)
			{
				initialDate = new DateTime(DateTime.Now.Year, 1, 1);
			}
			if (!finalDate.HasValue)
			{
				finalDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			}

			ViewData["initialDate"] = initialDate.Value.ToString("yyyy/MM/dd");
			ViewData["finalDate"] = finalDate.Value.ToString("yyyy/MM/dd");
			var result = await _salesService.FindByDateGroupingAsync(initialDate, finalDate);

			return View(result);
		}


	}
}
