namespace Domain.Models;

public interface IPropertyAction
{
    public bool IsActive { get; }

    /// <summary>
    /// Метод изменения состояния свойства <see cref="IsActive"/>
    /// </summary>
    /// <param name="value"></param>
    public void SetIsActive(bool value);

    /// <summary>
    /// Событие, которое срабатывает при защите животного
    /// </summary>
    /// <param name="self">защищающееся животное (кому принадлежит свойство)</param>
    /// <param name="enemy">атакующее животное</param>
    /// <returns> 
    /// null - если свойство не является защитным<br/>
    /// true - если защита успешно сработала<br/>
    /// false - если защита не сработала
    /// </returns>
    public bool? OnDefense(Animal self, Animal enemy);

    /// <summary>
    /// Событие, которое срабатывает при его использовании в свой ход
    /// </summary>
    /// <param name="self">животное, которому принадлежит свойство</param>
    public void OnUse(Animal self);
}
