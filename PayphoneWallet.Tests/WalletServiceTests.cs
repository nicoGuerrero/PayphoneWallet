using Moq;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using PayphoneWallet.Service;
using PayphoneWallet.Service.Contracts;

namespace PayphoneWallet.Tests
{
	public class WalletServiceTests
	{

		private readonly Mock<IWalletRepository> _walletRepositoryMock = new();
		private readonly WalletService _walletService;


		public WalletServiceTests()
		{
			_walletRepositoryMock = new Mock<IWalletRepository>();
			_walletService = new WalletService(_walletRepositoryMock.Object);
		}

		[Fact]
		public async Task GetAllWalletsAsync_ReturnsListOfWallets()
		{
			// Arrange
			_walletRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Wallet>
			{
				new Wallet { Id = 1, Name = "Wallet A" },
				new Wallet { Id = 2, Name = "Wallet B" }
			});


			// Act
			var result = await _walletService.GetAllAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Collection(result,
				item => Assert.Equal("Wallet A", item.Name),
				item => Assert.Equal("Wallet B", item.Name));
		}

		[Fact]
		public async Task GetWalletByIdAsync_ReturnsWallet_IfExists()
		{
			var wallet = new Wallet { Id = 1, Name = "Test Wallet" };
			_walletRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(wallet);

			var result = await _walletService.GetByIdAsync(1);

			Assert.NotNull(result);
			Assert.Equal("Test Wallet", result!.Name);
		}

		[Fact]
		public async Task UpdateWalletAsync_UpdatesName_WhenWalletExists()
		{
			var wallet = new Wallet { Id = 1, Name = "Old Name", Balance = 5, UpdatedAt = DateTime.MinValue };
			_walletRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(wallet);

			await _walletService.UpdateAsync(1, "New Name", 10);

			_walletRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Wallet>(w => w.Name == "New Name" && w.Balance == 10)), Times.Once);
		}

		[Fact]
		public async Task DeleteWalletAsync_CallsRepository()
		{
			await _walletService.DeleteAsync(1);

			_walletRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
		}
	} 
}
