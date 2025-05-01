using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Core.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public int WalletId { get; set; }
		public decimal Amount { get; set; }
		public string Type { get; set; } = string.Empty; // Credito | Debito
		public DateTime CreatedAt { get; set; }
	}
}
