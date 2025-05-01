using PayphoneWallet.Core.Models;

namespace PayphoneWallet.Service.Contracts
{
	public interface IWalletService
	{
		Task<IEnumerable<Wallet>> GetAllAsync();
		Task<Wallet?> GetByIdAsync(int id);
		Task CreateAsync(string documentId, string name);
		Task UpdateAsync(int id, string name, decimal balance);
		Task DeleteAsync(int id);
	}
}
