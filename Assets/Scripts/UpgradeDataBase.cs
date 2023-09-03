using System;
using UnityEngine;

public class Data
{
    public int Level;
    public int CostGold;
    public int CostSilver;
}

// Атака
[Serializable] public class Damage : Data { public int damage; }
[Serializable] public class AttackInterval : Data { public int attackInterval; }
[Serializable] public class RadiusAttack : Data { public float radiusAttack; }
[Serializable] public class MultiShot : Data { public float percentChance; }
[Serializable] public class CriticalChance : Data { public float percentChance; }
[Serializable] public class CriticalMultiplier : Data { public float multiplier; }
// Выживаемость
[Serializable] public class MaxHealth : Data { public int maxHealth; }


[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    // Атака
    public Damage[] damages;
    public AttackInterval[] attackIntervals;
    public RadiusAttack[] radiusAttacks;
    public MultiShot[] multiShots;
    public CriticalChance[] criticalChances;
    public CriticalMultiplier[] criticalMultipliers;
    // Выживаемость

    public T GetUpgrade<T>(int level) where T : Data
    {
        foreach (var upgrade in GetUpgradesArray<T>()) if (upgrade.Level == level) return upgrade;
        return null;
    }

    private T[] GetUpgradesArray<T>() where T : Data
    {
        return typeof(T).Name switch
        {
            nameof(Damage) => damages as T[],
            nameof(AttackInterval) => attackIntervals as T[],
            nameof(RadiusAttack) => radiusAttacks as T[],
            nameof(MultiShot) => multiShots as T[],
            nameof(CriticalChance) => criticalChances as T[],
            nameof(CriticalMultiplier) => criticalMultipliers as T[],
            _ => null
        };
    }

}
