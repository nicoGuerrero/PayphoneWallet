using Moq;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using PayphoneWallet.Service;

namespace PayphoneWallet.Tests
{
	public class TransactionServiceTests
	{
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly Mock<IWalletRepository> _walletRepositoryMock;
		private readonly TransactionService _transactionService;

		public TransactionServiceTests()
		{
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_walletRepositoryMock = new Mock<IWalletRepository>();
			_transactionService = new TransactionService(_transactionRepositoryMock.Object, _walletRepositoryMock.Object);
		}

		[Fact]
		public async Task CreateTransactionAsync_Should_Create_Transaction_Between_Wallets()
		{
			// Arrange
			var fromWallet = new Wallet { Id = 1, Balance = 100 };
			var toWallet = new Wallet { Id = 2, Balance = 50 };

			_walletRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(fromWallet);
			_walletRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(toWallet);

			// Act
			await _transactionService.CreateAsync(1, 2, 30);

			// Assert
			Assert.Equal(70, fromWallet.Balance);
			Assert.Equal(80, toWallet.Balance);

			_walletRepositoryMock.Verify(x => x.UpdateAsync(fromWallet), Times.Once);
			_walletRepositoryMock.Verify(x => x.UpdateAsync(toWallet), Times.Once);
			_transactionRepositoryMock.Verify(x => x.AddAsync(It.Is<Transaction>(t => t.Type == "Debit" && t.Amount == 30)), Times.Once);
			_transactionRepositoryMock.Verify(x => x.AddAsync(It.Is<Transaction>(t => t.Type == "Credit" && t.Amount == 30)), Times.Once);
		}

		[Fact]
		public async Task TransferBalanceAsync_Should_Throw_When_Insufficient_Balance()
		{
			var fromWallet = new Wallet { Id = 1, Balance = 10 };
			var toWallet = new Wallet { Id = 2, Balance = 0 };

			_walletRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(fromWallet);
			_walletRepositoryMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(toWallet);

			await Assert.ThrowsAsync<InvalidOperationException>(() =>
				_transactionService.CreateAsync(1, 2, 100));
		}

		[Fact]
		public async Task TransferBalanceAsync_Should_Throw_When_Wallet_Not_Found()
		{
			_walletRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Wallet?)null);

			await Assert.ThrowsAsync<InvalidOperationException>(() =>
				_transactionService.CreateAsync(1, 2, 50));
		}
	}
}