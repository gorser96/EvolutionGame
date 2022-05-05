namespace Domain.Models;

/// <summary>
/// Водоплавающее
/// </summary>
public class Swimming : Property, IPropertyAction
{
    public Swimming(Guid uid, string name)
        : base(uid, name, isPair: false, isOnEnemy: false, feedIncreasing: 0, typeof(Swimming).FullName!)
    {
    }

    public AnimalPropertyType PropertyType => AnimalPropertyType.PassiveDefense;

    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid)
    {
        return new(false);
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }

    public bool CanAttack(Animal self, Animal enemy)
    {
        // если защищающийся водоплавающее
        var isSelfHasSwimming = IsActive(self);

        // если атакующий водоплавающее
        var isEnemyHasSwimming = IsActive(enemy);
        /*
         * разбор ситуаций (атака -> защита = результат):
         * водоплавающий -> водоплавающий = true
         * водоплавающий -> не водоплавающий = false
         * не водоплавающий -> водоплавающий = false
         * не водоплавающий -> не водоплавающий = true
         * 
         * isEnemyHasSwimming - признак, что атакующий - водоплавающее
         * isSelfHasSwimming - признак, что защищающийся - водоплавающее
         */
        return isSelfHasSwimming && isEnemyHasSwimming;
    }
}
