using Microsoft.EntityFrameworkCore;
using PayphoneWallet.Core.Models;
using PayphoneWallet.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PayphoneWallet.Data.Repositories
{
    public class WalletRepository : IWalletRepository
	{
        private readonly IApplicationDbContext _context;

        public WalletRepository(IApplicationDbContext context)
		{
            _context = context;
        }

        public async Task<Wallet?> GetByIdAsync(int id) => await _context.Wallets.FindAsync(id);

        public async Task<IEnumerable<Wallet>> GetAllAsync() => await _context.Wallets.ToListAsync();

        public async Task AddAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.Save();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.Save();
            }
        }
    }
}
