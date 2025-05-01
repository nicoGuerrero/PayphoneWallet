using System.ComponentModel.DataAnnotations;

namespace PayphoneWallet.ViewModels
{
	public class CreateTransactionRequest
	{

		public required int FromWalletId { get; set; }
		public required int ToWalletId { get; set; }
		public required decimal Amount { get; set; }

	}
}
