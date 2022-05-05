using Domain.Models;
using EvolutionBack.Commands;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using EvolutionTests.TestServices;
using Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EvolutionTests;

public class PropertyTests : IDisposable
{
    private readonly WebServiceTest _services;
    private readonly UserCredentials _userCredentials;
    private readonly Guid _roomUid;

    public PropertyTests()
    {
        _services = new();

        using var scope = _services.GetScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        _ = mediator.Send(userCreateCommand).Result;

        var command = new UserLoginCommand("test_user", "123test");
        var userView = mediator.Send(command).Result;

        _userCredentials = new(userView.UserName);

        var createCommand = new RoomCreateCommand("test room", _userCredentials);
        var roomViewModel = mediator.Send(createCommand).Result;

        _roomUid = roomViewModel.Uid;

        var startCommand = new StartGameCommand(roomViewModel.Uid, _userCredentials);
        _ = mediator.Send(startCommand).Result;
    }

    private async Task<AnimalViewModel> CreateAnimal()
    {
        using var scope = _services.GetScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new CreateAnimalCommand(_userCredentials, _roomUid));

        var roomViewModel = scope.ServiceProvider.GetRequiredService<RoomQueries>().GetRoomViewModel(_roomUid);

        return roomViewModel?.InGameUsers.First(x => x.User.UserName == _userCredentials.UserName).Animals.Last()
            ?? throw new InvalidOperationException();
    }

    private async Task<AnimalViewModel> AddProperty(Guid animalUid, Guid propertyUid, Guid cardUid)
    {
        using var scope = _services.GetScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new AddPropertyCommand(_roomUid, animalUid, propertyUid, cardUid, _userCredentials));
        var roomViewModel = scope.ServiceProvider.GetRequiredService<RoomQueries>().GetRoomViewModel(_roomUid);

        return roomViewModel?.InGameUsers.First(x => x.User.UserName == _userCredentials.UserName).Animals.First(x => x.Uid == animalUid)
            ?? throw new InvalidOperationException();
    }

    private IList<InGameCard> GetInGameCards()
    {
        using var scope = _services.GetScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();

        return db.InGameCards.AsNoTracking()
            .Include(x => x.Card)
            .Include(x => x.Card).ThenInclude(x => x.Addition)
            .Include(x => x.Card).ThenInclude(x => x.FirstProperty)
            .Include(x => x.Card).ThenInclude(x => x.SecondProperty)
            .Where(x => x.RoomUid == _roomUid)
            .ToList();
    }

    private (InGameCard card, Guid propertyUid) GetCardByPropertyName(string assemblyName)
    {
        var card = GetInGameCards().FirstOrDefault(x => x.Card.FirstProperty.AssemblyName == assemblyName ||
                                                    x.Card.SecondProperty?.AssemblyName == assemblyName)
            ?? throw new InvalidOperationException();

        return (
            card,
            card.Card.FirstProperty.AssemblyName == assemblyName
            ? card.Card.FirstPropertyUid
            : card.Card.SecondPropertyUid.GetValueOrDefault());
    }

    private async Task<ActionResponse> Attack(Guid attackerUid, Guid defensiveUid)
    {
        using var scope = _services.GetScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(new AttackCommand(attackerUid, defensiveUid, null, _roomUid, _userCredentials));
    }

    [Fact]
    public async Task Burrowing_test_false()
    {
        // создаем животное
        var selfAnimalView = await CreateAnimal();
        // выбираем первую карточку с норным
        var (cardWithBurrowing, burrowingPropertyUid) = GetCardByPropertyName(typeof(Burrowing).FullName!);
        // добавляем свойство норного к животному
        selfAnimalView = await AddProperty(selfAnimalView.Uid, burrowingPropertyUid, cardWithBurrowing.CardUid);

        // создаем второе животное
        var enemyAnimalView = await CreateAnimal();
        // выбираем первую карточку с хищником
        var (cardWithCarnivorous, carnivorousPropertyUid) = GetCardByPropertyName(typeof(Carnivorous).FullName!);
        // добавляем свойство хищника второму животному
        enemyAnimalView = await AddProperty(enemyAnimalView.Uid, carnivorousPropertyUid, cardWithCarnivorous.CardUid);

        // Атакуем первое животное вторым
        var actionResponse = await Attack(enemyAnimalView.Uid, selfAnimalView.Uid);

        Assert.Equal(AttackResponseType.SuccessAttack, actionResponse.ActionType);
    }

    [Fact]
    public void Burrowing_test_true()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Carnivorous_test()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
