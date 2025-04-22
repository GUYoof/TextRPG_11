using TXT11;

public class Player
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public float Attack { get; set; }  
    public int Defense { get; set; }  
    public int Gold { get; set; }
    public int Exp { get; set; }

    private static readonly int[] LevelRequirements = { 10, 35, 65, 100 };


    public Player(string name, string job, int level, int hp, float attack, int defense, int gold, int exp)
    {
        Name = name;
        Job = job;
        Level = level;
        HP = hp;
        Attack = attack;
        Defense = defense;
        Gold = gold;
        Exp = exp;
    }

    public void GainExp(int amount)
    {
        Console.WriteLine($"경험치 +{amount}");
        int prevLevel = Level;
        int prevExp = Exp;
        float prevAtk = Attack;
        int prevDef = Defense;

        Exp += amount;

        while (Level - 1 < LevelRequirements.Length && Exp >= LevelRequirements[Level - 1])
        {
            Exp -= LevelRequirements[Level - 1];
            Level++;
            Attack += 0.5f;
            Defense +=  1;
            Console.WriteLine($"레벨업! Lv.{Level - 1} → Lv.{Level}");
        }

        
    }
}


// if (monster.HP <= 0)
// {
//     Console.WriteLine($"{monster.Name}을(를) 쓰러뜨렸습니다!");
//     Console.WriteLine($"골드 +{monster.GoldReward}");

//     player.Gold += monster.GoldReward;

//     player.GainExp(monster.ExpReward);
// }