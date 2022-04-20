using MediatR;

namespace EvolutionBack.Commands;

public class AddPairPropertyCommandHandler : IRequestHandler<AddPairPropertyCommand>
{
    public AddPairPropertyCommandHandler()
    {

    }

    public Task<Unit> Handle(AddPairPropertyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
