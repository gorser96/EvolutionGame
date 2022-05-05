﻿using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CreateAnimalCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Unit> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();

        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(room));
        }

        var animal = room.CreateAnimalFromNextCard(user.Id);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
