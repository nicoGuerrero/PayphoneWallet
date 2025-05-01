using Microsoft.EntityFrameworkCore;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly IApplicationDbContext _context;

		public TransactionRepository(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId)
		{
			return await _context.Transactions
				.Where(t => t.WalletId == walletId)
				.OrderByDescending(t => t.CreatedAt)
				.ToListAsync();
		}

		public async Task AddAsync(Transaction transaction)
		{
			_context.Transactions.Add(transaction);
			await _context.Save();
		}
	}
}