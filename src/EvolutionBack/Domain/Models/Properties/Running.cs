namespace Domain.Models;

/// <summary>
/// Быстрое
/// </summary>
public class Running : Property, IPropertyAction
{
    public Running(Guid uid, string name)
        : base(uid, name, isPair: false, isOnEnemy: false, feedIncreasing: 0, typeof(Running).FullName!)
    {
    }

    public AnimalPropertyType PropertyType => AnimalPropertyType.ActiveDefense;

    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid)
    {
        if (IsActive(self))
        {
            Random rand = new((int)DateTime.Now.Ticks);
            var cubeResult = rand.Next(1, 6);
            if (cubeResult >= 4)
            {
                //Если выпали числа 4,5,6
                return new(true);
            }
        }

        return new(false);
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }

    public bool CanAttack(Animal self, Animal enemy)
    {
        return true;
    }
}
