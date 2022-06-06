using Domain.Events;
using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class Room
{
    /// <summary>
    /// Удаление животного из комнаты
    /// </summary>
    /// <param name="userUid"></param>
    /// <param name="animalUid"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
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

    /// <summary>
    /// Создание животного из очередной карты колоды
    /// </summary>
    /// <param name="userUid">Пользователь, которому добавляется животное</param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="NullReferenceException"></exception>
    public Animal CreateAnimalFromNextCard(Guid userUid)
    {
        _roomValidator?.CanUserCreateAnimal(this, userUid);

        var card = Cards.OrderBy(x => x.Order).FirstOrDefault();
        if (card is null)
        {
            throw new ValidationException("Card not found!");
        }
        Cards.Remove(card);

        var user = FindUserByUid(userUid) ?? throw new NullReferenceException(nameof(userUid));
        var animal = user.AddAnimal(card.CardUid);

        return animal;
    }

    /// <summary>
    /// Добавление свойства животному
    /// </summary>
    /// <param name="userUid">Пользователь, которому принадлежит животное</param>
    /// <param name="animalUid">Животное</param>
    /// <param name="cardUid">Карта</param>
    /// <param name="propertyUid">Свойство</param>
    /// <exception cref="ValidationException"></exception>
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

    /// <summary>
    /// Команда на использование свойства у животного
    /// </summary>
    /// <param name="userUid">Пользователь, которому принадлежит животное, которому принадлежит активируемое свойство</param>
    /// <param name="sourceAnimalUid">Животное, которому принадлежит активируемое свойство</param>
    /// <param name="propertyUid">Активируемое свойство</param>
    /// <param name="targetAnimalUid">Животное, на которое направлено свойство</param>
    /// <exception cref="ValidationException"></exception>
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

    /// <summary>
    /// Запуск игровой сессии
    /// </summary>
    /// <param name="userUid">Пользователь, который запустил игру</param>
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

    /// <summary>
    /// Переход хода к следующему пользователю
    /// </summary>
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

    /// <summary>
    /// Установление паузы в комнате
    /// </summary>
    /// <param name="userUid"></param>
    public void Pause(Guid userUid)
    {
        _roomValidator?.CanUserPause(this, userUid);

        if (IsStarted && !IsPaused)
        {
            SetIsPaused(true);
            PauseStartedTime = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Возобновление игры
    /// </summary>
    /// <param name="userUid"></param>
    public void Resume(Guid userUid)
    {
        _roomValidator?.CanUserResume(this, userUid);
        if (IsStarted && IsPaused)
        {
            SetIsPaused(false);
            PauseStartedTime = null;
        }
    }

    /// <summary>
    /// Завершение игры
    /// </summary>
    public void EndGame()
    {
        if (IsStarted)
        {
            SetIsStarted(false);
            SetFinishedDateTime(DateTime.UtcNow);
        }
    }

    /// <summary>
    /// Инициализация команты
    /// </summary>
    /// <param name="additions">Список базовых дополнений</param>
    public void Init(ICollection<Addition> additions)
    {
        UpdateAdditions(additions);
    }

    /// <summary>
    /// Обновление параметров комнаты
    /// </summary>
    /// <param name="editModel">Модель изменения комнаты</param>
    /// <param name="userUid">Пользователь, который вызвал изменение</param>
    /// <exception cref="ApplicationException"></exception>
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
            if (editModel.IsPrivate.HasValue)
            {
                SetIsPrivate(editModel.IsPrivate.Value);
            }
            if (editModel.NumOfCards.HasValue)
            {
                SetNumOfCards(editModel.NumOfCards.Value);
            }
        }
    }

    /// <summary>
    /// Удаление пользователя из комнаты
    /// </summary>
    /// <param name="userUid"></param>
    /// <exception cref="ValidationException"></exception>
    public void RemoveUser(User user)
    {
        _roomValidator?.CanUserLeave(this, user.Id);

        if (!IsStarted && !IsFinished)
        {
            var exsistUser = FindUserByUid(user.Id) ?? throw new ValidationException($"User [{user.Id}] not found in room!");
            InGameUsers.Remove(exsistUser);
            AddDomainEvent(new RoomLeaveUserEvent(this, user.UserName));
        }
    }

    /// <summary>
    /// Добавление пользователя в комнату
    /// </summary>
    /// <param name="userUid"></param>
    public void AddUser(User user)
    {
        _roomValidator?.CanUserEnter(this, user.Id);

        if (!IsStarted && !IsFinished)
        {
            // первый игрок, который заходит в комнату становится хостом
            bool isUserHost = !InGameUsers.Any();
            InGameUsers.Add(new InGameUser(user.Id, Uid, isUserHost));
            InGameUsers.Last().Update(new InGameUserUpdateModel(order: InGameUsers.Count - 1));
            AddDomainEvent(new RoomEnterUserEvent(this, user.UserName));

        }
    }

    /// <summary>
    /// Поиск пользователя в комнате
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public InGameUser? FindUserByUid(Guid uid)
    {
        return InGameUsers.FirstOrDefault(x => x.UserUid == uid);
    }

    /// <summary>
    /// Поиск животного в комнате
    /// </summary>
    /// <param name="userUid">Пользователь, которому принадлежит животное</param>
    /// <param name="animalUid">Животное</param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
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

    /// <summary>
    /// Поиск животного в комнате
    /// </summary>
    /// <param name="animalUid"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
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

