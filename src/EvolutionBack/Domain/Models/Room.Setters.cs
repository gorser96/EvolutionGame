using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

/// <summary>
/// Описание методов, которые изменяют внутренние свойства или коллекции
/// </summary>
public partial class Room
{
    private void SetName(string name)
    {
        if (Name != name)
        {
            Name = name;
            SetModified();
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

    private void SetIsPrivate(bool isPrivate)
    {
        if (IsPrivate != isPrivate)
        {
            IsPrivate = isPrivate;
            SetModified();
        }
    }

    private void SetMaxTimeLeft(TimeSpan? maxTimeLeft)
    {
        if (MaxTimeLeft != maxTimeLeft)
        {
            MaxTimeLeft = maxTimeLeft;
            SetModified();
        }
    }

    private void SetStepNumber(int stepNumber)
    {
        if (StepNumber != stepNumber)
        {
            StepNumber = stepNumber;
            SetModified();
        }
    }

    private void SetIsStarted(bool isStarted)
    {
        if (IsStarted != isStarted)
        {
            IsStarted = isStarted;
            SetModified();
        }
    }

    private void SetIsPaused(bool isPaused)
    {
        if (IsPaused != isPaused)
        {
            IsPaused = isPaused;
            SetModified();
        }
    }

    /// <summary>
    /// Обновление списка дополнений
    /// </summary>
    /// <param name="additions"></param>
    private void UpdateAdditions(ICollection<Addition> additions)
    {
        var source = Additions.ToList();

        foreach (var addition in additions)
        {
            var exist = source.FirstOrDefault(x => x.Uid == addition.Uid);
            if (exist is null)
            {
                Additions.Add(addition);
                SetModified();
            }
        }

        var listToRemove = new List<Addition>();
        foreach (var addition in source)
        {
            if (addition.IsBase)
            {
                continue;
            }

            if (!additions.Any(x => x.Uid == addition.Uid))
            {
                listToRemove.Add(addition);
            }
        }

        foreach (var addition in listToRemove)
        {
            Additions.Remove(addition);
            SetModified();
        }
        UpdateCards(Additions.SelectMany(x => x.Cards).ToArray());
    }

    /// <summary>
    /// Обновление списка карт колоды
    /// </summary>
    /// <param name="cards"></param>
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

    /// <summary>
    /// Перетасовка карт колоды. Изменяются только значения <see cref="InGameCard.Order"/> у карт колоды
    /// </summary>
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

    /// <summary>
    /// Урезает количество карт в колоде. 
    /// Сначала формирует полнуют колоду карт из добавленных в комнату дополнений, после чего удаляет последние карты
    /// </summary>
    /// <param name="numOfCards">Количество карт, которое должно остаться в колоде</param>
    /// <exception cref="ValidationException"></exception>
    private void SetNumOfCards(int numOfCards)
    {
        if (numOfCards < 1)
        {
            throw new ValidationException("Num of cards should be more then 0!");
        }

        if (numOfCards > Additions.SelectMany(x => x.Cards).Count())
        {
            throw new ValidationException("Num of cards should be less then max cards in all additions!");
        }

        if (numOfCards != Cards.Count)
        {
            // TODO опитимизировать
            UpdateCards(Additions.SelectMany(x => x.Cards).ToArray());

            var toRemove = Cards.OrderBy(x => x.Order).Skip(numOfCards).ToArray();
            foreach (var card in toRemove)
            {
                Cards.Remove(card);
            }
        }
    }
}

