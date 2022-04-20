using MediatR;

namespace EvolutionBack.Commands;

public class AddPropertyCommandHandler : IRequestHandler<AddPropertyCommand>
{
    public AddPropertyCommandHandler()
    {
    }

    public Task<Unit> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
