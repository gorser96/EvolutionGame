namespace Domain.Models;

public interface IPropertyAction
{
    /// <summary>
    /// Тип свойства по принципу работы
    /// </summary>
    public AnimalPropertyType PropertyType { get; }

    /// <summary>
    /// Событие, которое срабатывает при защите животного
    /// </summary>
    /// <param name="self">защищающееся животное (кому принадлежит свойство)</param>
    /// <param name="enemy">атакующее животное</param>
    /// <param name="targetUid">Uid цели, которая выбрана для защиты (другое животное или свойство)</param>
    /// <returns></returns>
    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid);

    /// <summary>
    /// Событие, которое срабатывает при его использовании в свой ход
    /// </summary>
    /// <param name="self">животное, которому принадлежит свойство</param>
    /// <param name="target">животное, на которое направлено свойство</param>
    public void OnUse(Animal self, Animal? target);

    /// <summary>
    /// Проверка возможности нападения на животное
    /// </summary>
    /// <param name="self">защищающееся животное (кому принадлежит свойство)</param>
    /// <param name="enemy">атакующее животное</param>
    /// <returns>
    /// true - если можно атаковать<br/>
    /// false - если нельзя атаковать
    /// </returns>
    public bool CanAttack(Animal self, Animal enemy);
}

public enum AnimalPropertyType
{
    /// <summary>
    /// Пассивная защита (большой, норное, водоплавающее и т.д.)
    /// </summary>
    PassiveDefense = 1,

    /// <summary>
    /// Активная защита (раковина, мимикрия, отбрасывание хвоста и т.д.)
    /// </summary>
    ActiveDefense = 2,

    /// <summary>
    /// Пассивное свойство для нападения (острое зрение)
    /// </summary>
    PassiveAttack = 3,

    /// <summary>
    /// Активное свойство для нападения (засада, интеллект)
    /// </summary>
    ActiveAttack = 4,

    /// <summary>
    /// Активируемое свойство
    /// </summary>
    Active = 5,

    /// <summary>
    /// Пассивное свойство
    /// </summary>
    Passive = 6,
}

/// <summary>
/// Результат срабатывания защиты
/// </summary>
public record DefenseResult
{
    public DefenseResult(bool isSuccees, DefenseData? data = null)
    {
        IsSuccees = isSuccees;
        Data = data;
    }

    public bool IsSuccees { get; init; }

    public DefenseData? Data { get; init; }
}

/// <summary>
/// Вспомогательные данные результата защиты
/// </summary>
public record DefenseData
{
    public DefenseData(Animal? targetAnimal, Property? targetProperty)
    {
        TargetAnimal = targetAnimal;
        TargetProperty = targetProperty;
    }

    public Animal? TargetAnimal { get; init; }

    public Property? TargetProperty { get; set; }
}