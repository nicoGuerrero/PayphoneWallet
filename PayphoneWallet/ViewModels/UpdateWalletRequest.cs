using System.ComponentModel.DataAnnotations;

namespace PayphoneWallet.ViewModels
{
	public class UpdateWalletRequest
	{
		public required string Name { get; set; }
		public required decimal Balance { get; set; }
	}
}
