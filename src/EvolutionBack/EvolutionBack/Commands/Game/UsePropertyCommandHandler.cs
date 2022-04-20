using MediatR;

namespace EvolutionBack.Commands;

public class UsePropertyCommandHandler : IRequestHandler<UsePropertyCommand>
{
    public UsePropertyCommandHandler()
    {
    }

    public Task<Unit> Handle(UsePropertyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
