using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using PayphoneWallet.Service.Contracts;

namespace PayphoneWallet.Service
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IWalletRepository _walletRepository;

		public TransactionService(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
		{
			_transactionRepository = transactionRepository;
			_walletRepository = walletRepository;
		}

		public async Task CreateAsync(int fromWalletId, int toWalletId, decimal amount)
		{
			// Validaciones
			if (amount <= 0)
				throw new ArgumentException("El monto debe ser mayor que cero.");

			var fromWallet = await _walletRepository.GetByIdAsync(fromWalletId);
			var toWallet = await _walletRepository.GetByIdAsync(toWalletId);

			if (fromWallet == null || toWallet == null)
				throw new InvalidOperationException("Una o ambas billeteras no existen.");

			if (fromWallet.Balance < amount)
				throw new InvalidOperationException("Saldo insuficiente.");

			fromWallet.Balance -= amount;
			toWallet.Balance += amount;
			fromWallet.UpdatedAt = DateTime.UtcNow;
			toWallet.UpdatedAt = DateTime.UtcNow;

			await _walletRepository.UpdateAsync(fromWallet);
			await _walletRepository.UpdateAsync(toWallet);

			var debit = new Transaction
			{
				WalletId = fromWalletId,
				Amount = amount,
				Type = "Debito",
				CreatedAt = DateTime.UtcNow
			};

			var credit = new Transaction
			{
				WalletId = toWalletId,
				Amount = amount,
				Type = "Credito",
				CreatedAt = DateTime.UtcNow
			};

			await _transactionRepository.AddAsync(debit);
			await _transactionRepository.AddAsync(credit);
		}

		public async Task<IEnumerable<Transaction>> GetByWalletIdAsync(int id)
		{
			return await _transactionRepository.GetByWalletIdAsync(id);
		}
	}
}
