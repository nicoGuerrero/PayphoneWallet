using PayphoneWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data.Contracts
{
	public interface IWalletRepository
	{
		Task<Wallet?> GetByIdAsync(int id);
		Task<IEnumerable<Wallet>> GetAllAsync();
		Task AddAsync(Wallet wallet);
		Task UpdateAsync(Wallet wallet);
		Task DeleteAsync(int id);
	}
}
