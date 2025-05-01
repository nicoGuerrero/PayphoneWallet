using PayphoneWallet.Core.Models;

namespace PayphoneWallet.Service.Contracts
{
	public interface ITransactionService
	{
		Task CreateAsync(int fromWalletId, int toWalletId, decimal amount);

		Task<IEnumerable<Transaction>> GetByWalletIdAsync(int id);
	}
}
