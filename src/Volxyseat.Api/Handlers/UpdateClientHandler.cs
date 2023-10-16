using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volxyseat.Domain.Models.ClientModel;
using Volxyseat.Domain.Core.Data;

namespace Volxyseat.Api.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientRequest, Client>
    {
        private readonly IRepository<Client, Guid> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientHandler(IRepository<Client, Guid> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Client> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var existingClient = await _clientRepository.GetByIdAsync(request.Id);

            if (existingClient == null)
            {
                return null;
            }
            //adicionar as props da model
            existingClient.Name = request.Name;
            existingClient.Email = request.Email;
            existingClient.Cpf = request.Cpf;
            existingClient.Phone = request.Phone;

            //atualizar e salvar com a unity
            await _clientRepository.UpdateAsync(existingClient);
            await _unitOfWork.SaveChangesAsync();

            return existingClient;
        }
    }
}
