using MediatR;
using Volxyseat.Domain.Models.ClientModel;

public class UpdateClientRequest : IRequest<Client>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}