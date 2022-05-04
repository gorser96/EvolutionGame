﻿namespace Domain.Models;

/// <summary>
/// Водоплавающее
/// </summary>
public class Swimming : Property, IPropertyAction
{
    public Swimming(Guid uid, string name, bool isPair, bool isOnEnemy)
        : base(uid, name, isPair, isOnEnemy, 0, nameof(Swimming))
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
            if (enemy.Properties.Any(x => x.Property.AssemblyName == nameof(Swimming)))
            {
                //Если атакующий водоплавающее
                return false;
            }
            return true;
        }
        return false;
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }
}
