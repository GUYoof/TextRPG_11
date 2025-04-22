using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT11
{
    // 전투 행동 델리게이트 정의
    public delegate void BattleAction();

    // 던전 클래스
    public class Dungeon
    {
        public string Name;
        public Monster Monster;

        public Dungeon(string name, Monster monster)
        {
            Name = name;
            Monster = monster;
        }
    }
    // 몬스터 클래스
    public class Monster
    {
        public string Name;
        public int HP;
        public int Attack;
        public int ExpReward;
        public int GoldReward;

        public Monster(string name, int hp, int atk, int exp, int gold)
        {
            Name = name;
            HP = hp;
            Attack = atk;
            ExpReward = exp;
            GoldReward = gold;
        }
    }
    // 전투 시스템
    public class battlesystem
    {
        public event BattleAction onplayerattack;
        public event BattleAction onusepotion;
        public event BattleAction onmonsterattack;

        private Player player;
        private Monster monster;

        public battlesystem(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;

            // 이벤트
            onplayerattack += () =>
            {
                Console.WriteLine("\n[플레이어의 공격]");
                monster.HP -= player.Attack;
                Console.WriteLine($"{monster.Name}에게 {player.Attack} 데미지를 입혔습니다. 남은 hp: {monster.HP}");
            };


            onmonsterattack += () =>
            {
                Console.WriteLine($"\n[{monster.Name}의 반격!]");
                if (monster.Attack < player.Defense)
                {
                    player.HP = player.HP;
                    Console.WriteLine($"플레이어가 0 데미지를 입었습니다. 현재 hp: {player.HP}");
                }
                else
                {
                    monster.Attack = monster.Attack -= player.Defense;
                    Console.WriteLine($"플레이어가 {monster.Attack -= player.Defense} 데미지를 입었습니다. 현재 hp: {player.HP}");
                }
            };
        }
        public void DungeonEnter()
        {
            Console.WriteLine($"{monster.Name}와의 전투가 시작됩니다!");
            while (player.HP > 0 && monster.HP > 0)
            {
                Console.WriteLine("\n1. 공격");
                Console.Write("행동 선택: ");
                string input = Console.ReadLine();

                if (input == "1") onplayerattack?.Invoke();
                else
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                if (monster.HP <= 0)
                {
                    Console.WriteLine($"{monster.Name}을(를) 쓰러뜨렸습니다! 골드 +{monster.GoldReward}");
                    player.Gold += monster.GoldReward;
                    break;
                }

                onmonsterattack?.Invoke();
                if (player.HP <= 0)
                {
                    Console.WriteLine("플레이어가 쓰러졌습니다...");
                    break;
                }
            }
        }
    }
    public class DungeonProgram
    {
        public void DungeonMain(Player player)
        {

            List<Dungeon> dungeons = new List<Dungeon>
            {
                new Dungeon("고블린 던전", new Monster("고블린", 50, 10, 5, 15)),
                new Dungeon("오크 던전", new Monster("오크", 90, 20, 10, 30)),
                new Dungeon("하이오크 던전", new Monster("하이오크", 200, 30, 15, 100)),
            };

            while (true)
            {
                Console.WriteLine("\n--- 던전을 선택하세요 ---");
                for (int i = 0; i < dungeons.Count; i++)
                    Console.WriteLine($"{i + 1}. {dungeons[i].Name}");
                Console.Write("선택: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int dungeonChoice) || dungeonChoice < 1 || dungeonChoice > dungeons.Count)
                {
                    Console.WriteLine("잘못된 선택");
                    continue;
                }

                var selectedDungeon = dungeons[dungeonChoice - 1];
                battlesystem battle = new battlesystem(player, selectedDungeon.Monster);
                battle.DungeonEnter();

                if (player.HP <= 0) break;

                Console.WriteLine("\n전투 후 행동 선택:");
                Console.WriteLine("1. 마을로 돌아가기");
                Console.WriteLine("2. 던전을 더 탐색하기");
                Console.WriteLine("3. 휴식하기");
                Console.Write("선택: ");
                string choice = Console.ReadLine();

                if (choice == "1") //여관으로 돌아가기
                {
                    //Console.WriteLine("여관으로 돌아갑니다.");
                    //Hotel hotel = new Hotel();
                    //hotel.Rest(player);
                    return;
                }
                else if (choice == "2")
                {
                    battle.DungeonEnter();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("모닥불을 피우고 휴식을 취합니다.");

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n   ◆   ◆");
                    Console.WriteLine("  ◆◆◇ ◆◆◇");
                    Console.WriteLine(" ◆◆◆◇◇◆◆◇");
                    Console.WriteLine(" ◆◆◆◇◇◆◆◆◇");
                    Console.Write("◆◆◆◆◆◇◇◆◇◇");
                    Console.ResetColor();
                    Console.WriteLine(" ○");
                    Console.WriteLine("   ▒   ▒   |┘┘");
                    Console.WriteLine("   ▒   ▒   └═┐┐      ");
                    Console.WriteLine("   ▒      ▓▓ └└   🔥 ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("// \\\\");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲");
                    Console.ResetColor();
                    Console.ReadKey();
            
                    player.HP += 30;
                    Console.WriteLine($"\n{player.HP}");
                    Console.WriteLine("\n다음날 아침이 되자 몬스터가 습격해왔습니다.");
                    battle.DungeonEnter();
                }

            }
        }
    }
    
    
}






//public void EasyDungeon(Player player)
//{
//    Console.Clear();
//    Console.WriteLine("쉬운 던전");
//    Console.WriteLine("방어력 5 이상 권장하는 던전 입니다.");

//    if (player.GetTotalDefense() >= 5)
//    {
//        DungeonClear(player, 5, 1000);
//    }
//    else
//    {
//        ConditionsLack(player, 5);
//    }

//    Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//    Console.ReadLine();
//}

//public void NormalDongeo(Player player)
//{
//    Console.Clear();
//    Console.WriteLine("보통 던전");
//    Console.WriteLine("방어력 11 이상 권장하는 던전 입니다.");

//    if (player.GetTotalDefense() >= 11)
//    {
//        DungeonClear(player, 11, 1000);
//    }
//    else
//    {
//        ConditionsLack(player, 11);
//    }

//    Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//    Console.ReadLine();
//}

//public void HardDongeo(Player player)
//{
//    Console.Clear();
//    Console.WriteLine("어려운 던전");
//    Console.WriteLine("방어력 17 이상 권장하는 던전 입니다.");

//    if (player.GetTotalDefense() >= 17)
//    {
//        DungeonClear(player, 17, 1000);
//    }
//    else
//    {
//        ConditionsLack(player, 17);
//    }

//    Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//    Console.ReadLine();
//}



//public void DungeonEnter(Player player)
//    {
//        while (true)
//        {
//            Console.Clear();
//            Console.WriteLine("던전 입장");
//            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
//            Console.WriteLine("\n1. 쉬운 던전    | 방어력 5 이상 권장");
//            Console.WriteLine("2. 일반 던전    | 방어력 11 이상 권장");
//            Console.WriteLine("3. 어려운 던전  | 방어력 17 이상 권장");
//            Console.WriteLine("0. 나가기");
//            Console.WriteLine("원하시는 행동을 입력해주세요.");
//            Console.Write("\n>> ");
//            string select = Console.ReadLine();

//            if (int.TryParse(select, out int Difficulty))
//            {
//                switch (Difficulty)
//                {
//                    case 1:
//                        EasyDungeon(player);
//                        break;
//                    case 2:
//                        NormalDongeo(player);
//                        break;
//                    case 3:
//                        HardDongeo(player);
//                        break;
//                    case 0:
//                        return;
//                    default:
//                        Console.WriteLine("올바른 숫자를 입력하세요.");
//                        break;
//                }
//            }
//            else
//            {
//                Console.WriteLine("숫자를 입력하세요.");
//                Console.ReadLine();
//            }
//        }
//    }


//public void EasyDungeon(Player player)
//    {
//        Console.Clear();
//        Console.WriteLine("쉬운 던전");
//        Console.WriteLine("방어력 5 이상 권장하는 던전 입니다.");

//        if (player.GetTotalDefense() >= 5)
//        {
//            DungeonClear(player, 5, 1000);
//        }
//        else
//        {
//            ConditionsLack(player, 5);
//        }

//        Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//        Console.ReadLine();
//    }

//    public void NormalDongeo(Player player)
//    {
//        Console.Clear();
//        Console.WriteLine("보통 던전");
//        Console.WriteLine("방어력 11 이상 권장하는 던전 입니다.");

//        if (player.GetTotalDefense() >= 11)
//        {
//            DungeonClear(player, 11, 1000);
//        }
//        else
//        {
//            ConditionsLack(player, 11);
//        }

//        Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//        Console.ReadLine();
//    }

//    public void HardDongeo(Player player)
//    {
//        Console.Clear();
//        Console.WriteLine("어려운 던전");
//        Console.WriteLine("방어력 17 이상 권장하는 던전 입니다.");

//        if (player.GetTotalDefense() >= 17)
//        {
//            DungeonClear(player, 17, 1000);
//        }
//        else
//        {
//            ConditionsLack(player, 17);
//        }

//        Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
//        Console.ReadLine();
//    }

//                public void DungeonClear(Player player, int requiredDefense, int baseReward)
//        {
//            Console.WriteLine("\n던전 클리어");
//            Console.WriteLine("축하합니다!!");
//            Console.WriteLine("던전을 클리어 하였습니다.");
//            Console.WriteLine("\n[탐험 결과]");

//            int defenseCorrectionValue = player.GetTotalDefense() - requiredDefense;
//            int beforeHp = player.HP;
//            int beforeGold = player.Gold;

//            // 기본 범위에서 방어력 보정 적용
//            int minDamage = 20 - defenseCorrectionValue;
//            int maxDamage = 35 - defenseCorrectionValue;

//            Random damage = new Random();
//            int playerDamage = damage.Next(minDamage, maxDamage + 1); // 범위 포함

//            player.HP -= playerDamage;

//            if (player.HP < 0)
//            {
//                player.HP = 0;
//            }

//            // 기본 보상에서 보너스 보상 적용
//            int minBonusPercent = player.Attack;
//            int maxBonusPercent = player.Attack * 2;

//            Random Percent = new Random();
//            int bonusPercent = Percent.Next(minBonusPercent, maxBonusPercent + 1);

//            int bonusGold = baseReward * bonusPercent / 100;
//            int totalGold = baseReward + bonusGold;

//            player.Gold += totalGold;

//            Console.WriteLine($"체력 {beforeHp} → {player.HP}");
//            Console.WriteLine($"Gold {beforeGold} G → {player.Gold} G");
//        }

//        public void ConditionsLack(Player player, int requiredDefense)
//        {
//            Console.WriteLine("\n방어력이 부족하여 던전 탐험이 실패 할 수 있습니다.");

//            Random rand = new Random();
//            int chance = rand.Next(0, 101);

//            if (chance <= 40)
//            {
//                Console.WriteLine("\n던전 실패...");
//                Console.WriteLine("방어력이 부족하여 몬스터에게 패했습니다.");
//                Console.WriteLine("보상 없이 되돌아왔습니다.");

//                int defenseCorrectionValue = player.GetTotalDefense() - requiredDefense;
//                int beforeHp = player.HP;

//                int damage = player.HP / 2; // 절반 데미지
//                player.HP -= damage;
//                if (player.HP < 0)
//                {
//                    player.HP = 0;
//                }

//                Console.WriteLine($"체력 {beforeHp} → {player.HP}");
//            }
//            else
//            {
//                Console.WriteLine("\n운좋게 던전을 클리어했습니다!");
//                DungeonClear(player, 5, 1000); // 난이도별 값 전달
//            }
//        }
//    }
//}
