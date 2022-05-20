namespace Domain.Models;

/// <summary>
/// Игровое дополнение
/// </summary>
public class Addition
{
    public Addition(Guid uid, string name, bool isBase)
    {
        Uid = uid;
        Name = name;
        IsBase = isBase;
        Cards = new List<Card>();
        Rooms = new List<Room>();
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public bool IsBase { get; private set; }

    public string? IconName { get; private set; }

    public byte[]? Icon { get; private set; }

    public virtual ICollection<Card> Cards { get; private set; }

    public virtual ICollection<Room> Rooms { get; private set; }

    public void Update(string? name = null, IList<Card>? cards = null, string? iconName = null, byte[]? icon = null)
    {
        if (name is not null)
        {
            Name = name;
        }
        if (cards is not null)
        {
            UpdateCards(cards);
        }
        if (iconName is not null)
        {
            IconName = iconName;
        }
        if (icon is not null)
        {
            Icon = icon;
        }
    }

    private void UpdateCards(IList<Card> targetCollection)
    {
        var source = Cards.ToList();

        foreach (var obj in targetCollection)
        {
            var existObj = source.FirstOrDefault(x => x.Uid == obj.Uid);
            if (existObj != null)
            {
                existObj.Update(obj);
            }
            else
            {
                var newCard = new Card(obj.Uid, obj.AdditionUid, obj.FirstPropertyUid, obj.SecondPropertyUid);
                newCard.Update(obj);
                Cards.Add(newCard);
            }
        }

        var listToRemove = new List<Card>();
        foreach (var obj in source)
        {
            if (!targetCollection.Any(x => x.Uid == obj.Uid))
            {
                listToRemove.Add(obj);
            }
        }

        foreach (var objRemove in listToRemove)
        {
            Cards.Remove(objRemove);
        }
    }
}
