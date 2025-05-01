using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		protected readonly IConfiguration Configuration;

		public ApplicationDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(Configuration.GetConnectionString("Payphone"));
		}

		public DbSet<Wallet> Wallets {  get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		public Task<int> Save()
		{
			return SaveChangesAsync();
		}
	}
}
