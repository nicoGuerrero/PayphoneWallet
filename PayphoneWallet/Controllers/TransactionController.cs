using Microsoft.AspNetCore.Mvc;
using PayphoneWallet.Service.Contracts;
using PayphoneWallet.ViewModels;


namespace PayphoneWallet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpPost]
		public async Task<IActionResult> Transferir([FromBody] CreateTransactionRequest request)
		{
			try
			{
				await _transactionService.CreateAsync(request.FromWalletId, request.ToWalletId, request.Amount);
				return Ok("Transferencia completa");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("wallet/{walletId}")]
		public async Task<IActionResult> GetByWallet(int walletId)
		{
			var transactions = await _transactionService.GetByWalletIdAsync(walletId);
			return Ok(transactions);
		}
	}
}
