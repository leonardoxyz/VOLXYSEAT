using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.ClientModel;

namespace Volxyseat.Domain.Services
{
    public class ClientService
    {
        private readonly IRepository<Client, Guid> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IRepository<Client, Guid> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
            await _unitOfWork.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateClientAsync(Guid id, Client client)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient == null)
            {
                return null; // Cliente não encontrado
            }

            // Atualize os campos do cliente existente com os valores do cliente
            // que foi passado como parâmetro.
            existingClient.Nome = client.Nome; // Exemplo de campo para atualização

            _clientRepository.UpdateAsync(existingClient);
            await _unitOfWork.SaveChangesAsync();
            return existingClient;
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true; // Supondo que a exclusão tenha sido bem-sucedida
        }
    }
}
