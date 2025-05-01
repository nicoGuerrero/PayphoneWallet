using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Core.Models
{
	public class Wallet
	{
		public int Id { get; set; }
		public string DocumentId { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public decimal Balance { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
