using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Room
{
    private IRoomValidator? _roomValidator;

    public Room(Guid uid, string name, DateTime createdDateTime)
    {
        Uid = uid;
        Name = name;
        CreatedDateTime = createdDateTime;
        InGameUsers = new List<InGameUser>();
        Additions = new List<Addition>();
        Cards = new List<InGameCard>();
        StepNumber = 0;
        IsStarted = false;
        IsPaused = false;
        FinishedDateTime = null;
        MaxTimeLeft = null;
        StartDateTime = null;
        PauseStartedTime = null;
    }

    public Guid Uid { get; init; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; init; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? FinishedDateTime { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public int StepNumber { get; private set; }

    public bool IsStarted { get; private set; }

    public bool IsPaused { get; private set; }

    public DateTime? PauseStartedTime { get; private set; }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }

    public virtual ICollection<InGameCard> Cards { get; private set; }

    private bool IsFinished => FinishedDateTime.HasValue;

    private void SetName(string name)
    {
        if (Name != name)
        {
            Name = name;
        }
    }

    private void SetFinishedDateTime(DateTime? finishedDateTime)
    {
        if (FinishedDateTime != finishedDateTime)
        {
            FinishedDateTime = finishedDateTime;
        }
    }

    private void SetStartDateTime()
    {
        if (StartDateTime is null)
        {
            StartDateTime = DateTime.UtcNow;
        }
    }

    public bool RemoveAnimal(Guid userUid, Guid animalUid)
    {
        var inGameUser = FindUserByUid(userUid);
        if (inGameUser is null)
        {
            throw new ValidationException("User not found in room!");
        }

        if (!inGameUser.IsCurrent)
        {
            throw new ValidationException($"User is not current!");
        }

        var animal = inGameUser.Animals.FirstOrDefault(x => x.Uid == animalUid);
        if (animal is null)
        {
            throw new ValidationException("Animal for user not found!");
        }

        return inGameUser.Animals.Remove(animal);

    }

    private void SetMaxTimeLeft(TimeSpan? maxTimeLeft)
    {
        if (MaxTimeLeft != maxTimeLeft)
        {
            MaxTimeLeft = maxTimeLeft;
        }
    }

    private void SetStepNumber(int stepNumber)
    {
        if (StepNumber != stepNumber)
        {
            StepNumber = stepNumber;
        }
    }

    private void SetIsStarted(bool isStarted)
    {
        if (IsStarted != isStarted)
        {
            IsStarted = isStarted;
        }
    }

    private void SetIsPaused(bool isPaused)
    {
        if (IsPaused != isPaused)
        {
            IsPaused = isPaused;
        }
    }

    private void UpdateAdditions(ICollection<Addition> additions)
    {
        var source = Additions.ToList();

        foreach (var addition in additions)
        {
            var exist = source.FirstOrDefault(x => x.Uid == addition.Uid);
            if (exist is null)
            {
                Additions.Add(addition);
            }
        }

        var listToRemove = new List<Addition>();
        foreach (var addition in source)
        {
            if (!additions.Any(x => x.Uid == addition.Uid))
            {
                listToRemove.Add(addition);
            }
        }

        foreach (var addition in listToRemove)
        {
            Additions.Remove(addition);
        }
        UpdateCards(Additions.SelectMany(x => x.Cards).ToArray());
    }

    private void UpdateCards(ICollection<Card> cards)
    {
        var source = Cards.ToList();

        foreach (var card in cards)
        {
            var exist = source.FirstOrDefault(x => x.CardUid == card.Uid && x.RoomUid == Uid);
            if (exist is null)
            {
                Cards.Add(new InGameCard(Uid, card.Uid));
            }
        }

        var listToRemove = new List<InGameCard>();
        foreach (var card in source)
        {
            if (!cards.Any(x => x.Uid == card.CardUid))
            {
                listToRemove.Add(card);
            }
        }

        foreach (var card in listToRemove)
        {
            Cards.Remove(card);
        }

        ShuffleCards();
    }

    private void ShuffleCards()
    {
        var rnd = new Random((int)DateTime.Now.Ticks);
        var orders = Enumerable.Range(0, Cards.Count).ToArray();
        for (int i = 0; i < orders.Length; i++)
        {
            var indexRnd = rnd.Next(0, Cards.Count);
            var temp = orders[i];
            orders[i] = orders[indexRnd];
            orders[indexRnd] = temp;
        }
        for (int i = 0; i < orders.Length; i++)
        {
            Cards.ElementAt(i).Update(orders[i]);
        }
    }

    public Animal CreateAnimalFromNextCard(Guid userUid)
    {
        _roomValidator?.CanUserCreateAnimal(this, userUid);

        var card = Cards.FirstOrDefault();
        if (card is null)
        {
            throw new ValidationException("Card not found!");
        }
        Cards.Remove(card);

        var user = FindUserByUid(userUid) ?? throw new NullReferenceException(nameof(userUid));
        var animal = user.AddAnimal(card.CardUid);

        return animal;
    }

    public void AddAnimalProperty(Guid userUid, Guid animalUid, Guid cardUid, Guid propertyUid)
    {
        var card = Cards.FirstOrDefault(x => x.RoomUid == Uid && x.CardUid == cardUid);
        if (card is null)
        {
            throw new ValidationException("Card not found!");
        }
        Property property;
        if (card.Card.FirstPropertyUid == propertyUid)
        {
            property = card.Card.FirstProperty;
        }
        else if (card.Card.SecondPropertyUid.HasValue && card.Card.SecondPropertyUid == propertyUid)
        {
            property = card.Card.SecondProperty!;
        }
        else
        {
            throw new ValidationException("Card not contain property!");
        }

        var animal = FindAnimal(userUid, animalUid);
        animal.AddProperty(property);
        Cards.Remove(card);
    }

    public void UseAnimalProperty(Guid userUid, Guid sourceAnimalUid, Guid propertyUid, Guid? targetAnimalUid)
    {
        Animal? targetAnimal = null;

        var animal = FindAnimal(userUid, sourceAnimalUid);
        if (targetAnimalUid.HasValue)
        {
            targetAnimal = FindAnimal(targetAnimalUid.Value);
        }

        var property = animal.Properties.FirstOrDefault(x => x.PropertyUid == propertyUid);
        if (property is null)
        {
            throw new ValidationException("Property of animal not found!");
        }

        property.GetPropertyAction().OnUse(animal, targetAnimal);
    }

    public void StartGame(Guid userUid)
    {
        _roomValidator?.CanUserStart(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            SetIsStarted(true);
            SetStartDateTime();

            var firstUser = InGameUsers.First(x => x.Order == 0);
            firstUser.Update(new(isCurrent: true));
        }
    }

    public void NextStep()
    {
        if (IsStarted)
        {
            var currentUser = InGameUsers.Single(x => x.IsCurrent);
            var nextUser = InGameUsers.First(x => x.Order == (currentUser.Order + 1) % InGameUsers.Count);

            currentUser.Update(new(isCurrent: false));
            nextUser.Update(new(isCurrent: true));

            SetStepNumber(StepNumber + 1);
        }
    }

    public void Pause(Guid userUid)
    {
        _roomValidator?.CanUserPause(this, userUid);

        if (IsStarted && !IsPaused)
        {
            SetIsPaused(true);
            PauseStartedTime = DateTime.UtcNow;
        }
    }

    public void Resume(Guid userUid)
    {
        _roomValidator?.CanUserResume(this, userUid);
        if (IsStarted && IsPaused)
        {
            SetIsPaused(false);
            PauseStartedTime = null;
        }
    }

    public void EndGame()
    {
        if (IsStarted)
        {
            SetIsStarted(false);
            SetFinishedDateTime(DateTime.UtcNow);
        }
    }

    public void Init(ICollection<Addition> additions)
    {
        UpdateAdditions(additions);
    }

    public void Update(RoomUpdateModel editModel, Guid userUid)
    {
        _roomValidator?.CanUserUpdate(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            SetMaxTimeLeft(editModel.MaxTimeLeft);

            if (editModel.Name is not null)
            {
                SetName(editModel.Name);
            }
            if (editModel.Additions is not null)
            {
                UpdateAdditions(editModel.Additions);
            }
            if (editModel.UserOrder is not null)
            {
                if (editModel.UserOrder.Count != InGameUsers.Count)
                {
                    throw new ApplicationException($"Count of users is wrong!");
                }

                foreach (var user in InGameUsers)
                {
                    var (_, order) = editModel.UserOrder.Single(x => x.userUid == user.UserUid);
                    user.Update(new(order: order));
                }
            }
        }
    }

    public void RemoveUser(Guid userUid)
    {
        _roomValidator?.CanUserLeave(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            var user = FindUserByUid(userUid) ?? throw new NullReferenceException(nameof(userUid));
            InGameUsers.Remove(user);
        }
    }

    public void AddUser(Guid userUid)
    {
        _roomValidator?.CanUserEnter(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            // первый игрок, который заходит в комнату становится хостом
            bool isUserHost = !InGameUsers.Any();
            InGameUsers.Add(new InGameUser(userUid, Uid, isUserHost));
            InGameUsers.Last().Update(new InGameUserUpdateModel(order: InGameUsers.Count - 1));

        }
    }

    public void SetValidator(IRoomValidator roomValidator)
    {
        _roomValidator = roomValidator;
    }

    public InGameUser? FindUserByUid(Guid uid)
    {
        return InGameUsers.FirstOrDefault(x => x.UserUid == uid);
    }

    public Animal FindAnimal(Guid userUid, Guid animalUid)
    {
        var inGameUser = FindUserByUid(userUid);
        if (inGameUser is null)
        {
            throw new ValidationException("User not found in room!");
        }

        if (!inGameUser.IsCurrent)
        {
            throw new ValidationException($"User is not current!");
        }

        var animal = inGameUser.Animals.FirstOrDefault(x => x.Uid == animalUid);
        if (animal is null)
        {
            throw new ValidationException("Animal for user not found!");
        }

        return animal;
    }

    public Animal FindAnimal(Guid animalUid)
    {
        var animal = InGameUsers.SelectMany(x => x.Animals).FirstOrDefault(x => x.Uid == animalUid);
        if (animal is null)
        {
            throw new ValidationException("Animal for user not found!");
        }

        return animal;
    }
}
