using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using PayphoneWallet.Service.Contracts;

namespace PayphoneWallet.Service
{
	public class WalletService : IWalletService
	{
		private readonly IWalletRepository _walletRepository;

		public WalletService(IWalletRepository walletRepository)
		{
			_walletRepository = walletRepository;
		}

		public async Task<IEnumerable<Wallet>> GetAllAsync()
		{
			return await _walletRepository.GetAllAsync();
		}

		public async Task<Wallet?> GetByIdAsync(int id)
		{
			return await _walletRepository.GetByIdAsync(id);
		}

		public async Task CreateAsync(string documentId, string name)
		{
			var wallet = new Wallet
			{
				DocumentId = documentId,
				Name = name,
				Balance = 0,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _walletRepository.AddAsync(wallet);
		}

		public async Task UpdateAsync(int id, string name, decimal balance)
		{
			var wallet = await _walletRepository.GetByIdAsync(id);
			if (wallet == null) throw new InvalidOperationException("Billetera no encontrada.");

			wallet.Name = name;
			wallet.Balance = balance;
			wallet.UpdatedAt = DateTime.UtcNow;
			await _walletRepository.UpdateAsync(wallet);
		}

		public async Task DeleteAsync(int id)
		{
			await _walletRepository.DeleteAsync(id);
		}
	}
}
