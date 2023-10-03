using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.ClientModel;
using Volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class ClientRepository : IRepository<Client, Guid>
    {
        private readonly ApplicationDataContext _context;

        public ClientRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(Client entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
