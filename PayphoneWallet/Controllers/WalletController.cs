using Microsoft.AspNetCore.Mvc;
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

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateWalletRequest request)
		{
			await _walletService.CreateAsync(request.DocumentId, request.Name);
			return Ok("Billetera creada");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var wallet = await _walletService.GetByIdAsync(id);
			return wallet == null ? NotFound() : Ok(wallet);
		}



		// POST api/<ValuesController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
