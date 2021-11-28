﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
	public class SellersController : Controller
	{
		private readonly SellerService _sellerService;
		private readonly DepartmentService _departmentService;

		public SellersController(SellerService sellerService, DepartmentService departmentService)
		{
			_sellerService = sellerService;
			_departmentService = departmentService;
		}


		public async Task<IActionResult> Index()
		{
			var sellers = await _sellerService.FindAllAsync();

			return View(sellers);
		}


		public async Task<IActionResult> Create()
		{
			var departments = await _departmentService.FindAllAsync();
			var viewModel = new SellerFormViewModel { Departments = departments };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Seller seller)
		{
			if (!ModelState.IsValid) 
			{
				return View(seller);
			}
			await _sellerService.InsertAsync(seller);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not provided" }));
			}

			var seller = await _sellerService.FindByIdAsync(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not found" }));
			}

			return View(seller);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _sellerService.RemoveAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch (IntegrityException e) 
			{
				return RedirectToAction(nameof(Error), (new { message = e.Message }));
			}
		}


		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not provided" }));
			}

			var seller = await _sellerService.FindByIdAsync(id.Value);

			if (seller == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not found" }));
			}

			return View(seller);

		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not provided" }));
			}

			var seller = await _sellerService.FindByIdAsync(id.Value);
			if (seller == null)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id not found" }));
			}

			List<Department> departments = await _departmentService.FindAllAsync();
			SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

			return View(viewModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id,Seller seller) 
		{
			if (!ModelState.IsValid)
			{
				var departments = await _departmentService.FindAllAsync();
				SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments, Seller = seller};
				return View(viewModel);
			}
			if (Id != seller.Id)
			{
				return RedirectToAction(nameof(Error), (new { message = "Id mismatch" }));
			}

			try
			{
				await _sellerService.UpdateAsync(seller);
				return RedirectToAction(nameof(Index));
			}
			catch (ApplicationException e)
			{
				return RedirectToAction(nameof(Error), (new { message = e.Message }));
			}
		}

		public IActionResult Error(string message) 
		{
			var viewModel = new ErrorViewModel 
			{
				Message = message,
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			};

			return View(viewModel);
		}
	}
}
