namespace Domain.Models;

/// <summary>
/// Быстрое
/// </summary>
public class Running : Property, IPropertyAction
{
    public Running(Guid uid, string name, bool isPair, bool isOnEnemy)
        : base(uid, name, isPair, isOnEnemy, 0, nameof(Running))
    {
        IsActive = true;
    }

    public bool IsActive { get; private set; }

    public void SetIsActive(bool value)
    {
        IsActive = value;
    }

    public bool? OnDefense(Animal self, Animal enemy)
    {
        if (IsActive)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            var cubeResult = rand.Next(1, 6);
            if (cubeResult >= 4)
            {
                //Если выпали числа 4,5,6
                return true;
            }
        }
        return false;
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }
}
