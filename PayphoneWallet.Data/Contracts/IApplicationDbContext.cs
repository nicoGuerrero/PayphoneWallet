using Microsoft.EntityFrameworkCore;
using PayphoneWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data.Contracts
{
	public interface IApplicationDbContext
	{
		DbSet<Wallet> Wallets { get; set; }
		DbSet<Transaction> Transactions { get; set; }
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		Task<int> Save();
	}
}
