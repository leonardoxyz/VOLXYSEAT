using MediatR;
using Microsoft.AspNetCore.Mvc;
using Volxyseat.Domain.Models.ClientModel;
using Volxyseat.Domain.Services;

namespace Volxyseat.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly IMediator _mediator;
        public ClientController(ClientService clientService, IMediator mediator)
        {
            _clientService = clientService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            var createdClient = await _clientService.CreateClientAsync(client);
            return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> UpdateClientAsync(Guid id, [FromBody] UpdateClientRequest request)
        {
            request.Id = id;

            var updatedClient = await _mediator.Send(request);

            if (updatedClient == null)
            {
                return NotFound();
            }

            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
