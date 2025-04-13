using System;

[Serializable]
public class StatBlock
{
    public int Health;
    public int Armor;
    public int MagicResist;
    public int Attack;
    public int AbilityPower;
    public int AttackSpeed;
    public int Mana;
    public int Tenacity;
    public int Stamina;
    public int Luck;

    public static int[] ToArray(StatBlock stats)
    {
        return new int[]
        {
        stats.Health,
        stats.Armor,
        stats.MagicResist,
        stats.Attack,
        stats.AbilityPower,
        stats.AttackSpeed,
        stats.Mana,
        stats.Tenacity,
        stats.Stamina,
        stats.Luck
        };
    }

    public static StatBlock FromArray(int[] arr)
    {
        if (arr.Length != 10) throw new ArgumentException("Array must be length 10");

        return new StatBlock
        {
            Health = arr[0],
            Armor = arr[1],
            MagicResist = arr[2],
            Attack = arr[3],
            AbilityPower = arr[4],
            AttackSpeed = arr[5],
            Mana = arr[6],
            Tenacity = arr[7],
            Stamina = arr[8],
            Luck = arr[9]
        };
    }

    static public StatBlock BasePotentialCalculation(StatBlock Potential)
    {
        return new StatBlock();
    }
}