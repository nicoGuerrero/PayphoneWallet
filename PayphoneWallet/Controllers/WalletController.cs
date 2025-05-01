using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Service.Contracts;
using PayphoneWallet.ViewModels;


namespace PayphoneWallet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalletController : ControllerBase
	{
		private readonly IWalletService _walletService;
		private readonly ITransactionService _transactionService;

		public WalletController(IWalletService walletService, ITransactionService transactionService)
		{
			_walletService = walletService;
			_transactionService = transactionService;
		}

		// GET: api/Wallet
		[HttpGet]
		public async Task<IEnumerable<Wallet>> GetWallets()
		{
			return await _walletService.GetAllAsync();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var wallet = await _walletService.GetByIdAsync(id);
			return wallet == null ? NotFound() : Ok(wallet);
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateWalletRequest request)
		{
			await _walletService.CreateAsync(request.DocumentId, request.Name);
			return Ok("Billetera Creada.");
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateWalletRequest request)
		{
			try
			{
				await _walletService.UpdateAsync(id, request.Name, request.Balance);
				return Ok("Billetera Modificada.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _walletService.DeleteAsync(id);
			return Ok("Billetera Eliminada.");
		}
	}
}
