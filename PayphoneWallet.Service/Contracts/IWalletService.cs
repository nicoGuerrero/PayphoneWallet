using PayphoneWallet.Core.Models;

namespace PayphoneWallet.Service.Contracts
{
	public interface IWalletService
	{
		Task CreateAsync(string documentId, string name);

		Task<Wallet?> GetByIdAsync(int id);
	}
}
