using System.ComponentModel.DataAnnotations;

namespace PayphoneWallet.ViewModels
{
	public class CreateWalletRequest
	{
		public required string DocumentId { get; set; }
		public required string Name { get; set; }
	}
}
