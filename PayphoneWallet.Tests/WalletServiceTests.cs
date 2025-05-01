using Moq;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using PayphoneWallet.Service;

namespace PayphoneWallet.Tests
{
	public class WalletServiceTests
	{
		private readonly Mock<IWalletRepository> _walletRepositoryMock;
		private readonly WalletService _walletService;

		public WalletServiceTests()
		{
			_walletRepositoryMock = new Mock<IWalletRepository>();
			_walletService = new WalletService(_walletRepositoryMock.Object);
		}

		[Fact]
		public async Task CreateWalletAsync_Should_Create_New_Wallet()
		{
			// Arrange
			var docId = "123456";
			var name = "Test User";

			// Act
			await _walletService.CreateAsync(docId, name);

			// Assert
			_walletRepositoryMock.Verify(x => x.AddAsync(It.Is<Wallet>(
				w => w.DocumentId == docId && w.Name == name && w.Balance == 0)), Times.Once);
		}
	}
}