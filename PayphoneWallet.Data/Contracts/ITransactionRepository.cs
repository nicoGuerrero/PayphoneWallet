using PayphoneWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data.Contracts
{
	public interface ITransactionRepository
	{
		Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId);
		Task AddAsync(Transaction transaction);
	}
}
