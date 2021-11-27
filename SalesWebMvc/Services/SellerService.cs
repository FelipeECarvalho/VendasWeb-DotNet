﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
	public class SellerService
	{
		private readonly SalesWebMvcContext _context;

		public SellerService(SalesWebMvcContext context) 
		{
			_context = context;
		}

		public async Task<List<Seller>> FindAllAsync() 
		{
			return await _context.Seller.ToListAsync();
		}


		public async Task InsertAsync(Seller seller) 
		{
			_context.Add(seller);
			await _context.SaveChangesAsync();
		}

		public async Task<Seller> FindByIdAsync(int id) 
		{
			return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
		}

		public async Task RemoveAsync(int id) 
		{
			var seller = await _context.Seller.FindAsync(id);
			_context.Seller.Remove(seller);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Seller seller)
		{
			bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
			if (!hasAny)
			{
				throw new NotFoundException("Id not found");
			}
			try
			{
				_context.Update(seller);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException e) 
			{
				throw new DbConcurrencyException(e.Message) ;
			}
		}
	}
}
