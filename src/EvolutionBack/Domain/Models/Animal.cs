namespace Domain.Models;

/// <summary>
/// Класс животного
/// </summary>
public partial class Animal
{
    public Animal(Guid uid, Guid inGameUserUserUid, Guid inGameUserRoomUid, Guid inGameCardUid)
    {
        Uid = uid;
        FoodCurrent = 0;
        FoodMax = 1;
        State = AnimalState.Alive;
        Properties = new List<InAnimalProperty>();
        InGameUserUserUid = inGameUserUserUid;
        InGameUserRoomUid = inGameUserRoomUid;
        InGameCardUid = inGameCardUid;
    }

    public Guid Uid { get; private set; }

    public Guid InGameUserUserUid { get; init; }

    public Guid InGameUserRoomUid { get; init; }

    public Guid InGameCardUid { get; init; }

    /// <summary>
    /// Игрок, которому принадлежит животное
    /// </summary>
    public virtual InGameUser? InGameUser { get; private set; }

    /// <summary>
    /// Карточка, которая была использована для создания животного
    /// </summary>
    public virtual InGameCard? InGameCard { get; private set; }

    /// <summary>
    /// Текущее количество еды
    /// </summary>
    public int FoodCurrent { get; private set; }

    /// <summary>
    /// Потребность животного в еде
    /// </summary>
    public int FoodMax { get; private set; }

    /// <summary>
    /// Состояние животного
    /// </summary>
    public AnimalState State { get; private set; }

    /// <summary>
    /// Свойства животного
    /// </summary>
    public virtual ICollection<InAnimalProperty> Properties { get; private set; }

    private void SetMaxFood(int value)
    {
        FoodMax = value;
    }

    private void SetFood(int value)
    {
        FoodCurrent = value;
        if (FoodCurrent >= FoodMax)
        {
            AddState(AnimalState.Feeded);
        }
    }
}

/// <summary>
/// Состояния животного
/// </summary>
[Flags]
public enum AnimalState
{
    None = 0,
    /// <summary>
    /// Живое
    /// </summary>
    Alive = 1,
    /// <summary>
    /// Накормлено
    /// </summary>
    Feeded = 2,
    /// <summary>
    /// Отравлено
    /// </summary>
    Poisoned = 4,
    /// <summary>
    /// Спит
    /// </summary>
    Sleeping = 8,
    /// <summary>
    /// В раковине
    /// </summary>
    InShell = 16,
    /// <summary>
    /// В укрытии
    /// </summary>
    InShelter = 32,
}