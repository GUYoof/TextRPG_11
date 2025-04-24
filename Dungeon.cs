using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXT11;


namespace TXT11
{
    //던전 구현
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
    //몬스터 구현
    public class Monster
    {
        public string Name;
        public int HP;
        public float Attack;
        public int ExpReward;
        public int GoldReward;




        public Monster(string name, int hp, float atk, int exp, int gold)
        {
            Name = name;
            HP = hp;
            Attack = atk;
            ExpReward = exp;
            GoldReward = gold;
        }
    }

    public class Battlesystem
    {
        private Player player;
        private List<Monster> monsters;

        private int totalExpGained = 0;
        private int totalGoldGained = 0;
        public Battlesystem(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;
        }
        //몬스터 출현 구현
        public void DungeonEnter()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{monsters.Count}마리의 {monsters[0].Name}이(가) 나타났습니다!");
            Console.ResetColor();
            //몬스터의 현재 상태 창
            while (player.HP > 0 && monsters.Any(m => m.HP > 0))
            {
                Console.WriteLine("\n현재 몬스터 상태:");
                for (int i = 0; i < monsters.Count; i++)
                {
                    string status = monsters[i].HP > 0 ? $"{monsters[i].Name} - HP: {monsters[i].HP}" : $"{monsters[i].Name} (dead)";
                    Console.WriteLine($"{i + 1}. {status}");
                }
                //행동 선택
                Console.WriteLine("\n1. 공격");
                Console.WriteLine("2. 포션 사용");
                Console.Write("행동 선택: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write("공격할 몬스터 번호를 선택하세요: ");
                    string targetInput = Console.ReadLine();
                    if (int.TryParse(targetInput, out int targetIndex) &&
                        targetIndex >= 1 && targetIndex <= monsters.Count &&
                        monsters[targetIndex - 1].HP > 0)
                    {
                        Monster target = monsters[targetIndex - 1];
                        PlayerAttack(target);
                        if (target.HP <= 0)
                        {
                            Console.WriteLine($"{target.Name}을(를) 쓰러뜨렸습니다!");
                            totalExpGained += target.ExpReward;
                            totalGoldGained += target.GoldReward;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
                else if (input == "2")
                {
                    player.UsePotion();
                }
                else
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                // 모든 살아있는 몬스터가 공격
                foreach (var m in monsters.Where(m => m.HP > 0))
                {
                    MonsterAttack(m);
                    if (player.HP <= 0)
                    {
                        Console.WriteLine("플레이어가 쓰러졌습니다...");
                        return;
                    }
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n전투 종료! 총 획득 보상:");
            Console.WriteLine($"🪙 골드 +{totalGoldGained}");

            player.GainExp(totalExpGained);
            player.Gold += totalGoldGained;
            Console.ResetColor();
        }
        private void PlayerAttack(Monster monster)
        {
            //최소 최대 데미지 구현
            Console.WriteLine("\n[플레이어의 공격]");
            Random random = new Random();
            float randomValue = 0.9f + ((float)random.NextDouble() * 0.2f);
            float damage = player.Attack * randomValue;
            //크리티컬 구현
            if (player.CriticalChance())
            {
                damage *= 1.6f;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("⚡ 크리티컬 히트! ⚡");
                Console.ResetColor();
            }
            int finalDamage = (int)MathF.Ceiling(damage);
            monster.HP -= finalDamage;
            Console.WriteLine($"{monster.Name}에게 {finalDamage} 데미지를 입혔습니다. 남은 HP: {Math.Max(monster.HP, 0)}");
        }

        private void MonsterAttack(Monster monster)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[{monster.Name}의 반격!]");
            Console.ResetColor();
            //플레이어 회피율 구현(몬스터의 명중률 구현)
            if (player.AvoidChance())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("플레이어가 공격을 회피했습니다!");
                Console.ResetColor();
                return;
            }

            float damage = monster.Attack - player.Defense;
            if (damage <= 0)
            {
                damage = 0;
            }

            player.HP -= damage;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"플레이어가 {damage} 데미지를 입었습니다. 현재 HP: {Math.Max(player.HP, 0)}");
            Console.ResetColor();
        }
    }
    public class DungeonProgram
    {
        //던전 메인함수
        public void DungeonMain(Player player)
        {
            Random random = new Random();
            List<Dungeon> dungeons = new List<Dungeon>
            {
                new Dungeon("고블린 던전", new Monster("고블린", 10 , 7, 5 , 15 )),
                new Dungeon("오크 던전", new Monster("오크", 30 , 10 , 10 , 30 )),
                new Dungeon("하이오크 던전", new Monster("하이오크", 50  , 15 , 15, 100 )),
                new Dungeon("오크사령관", new Monster("오크사령관",70, 20, 30, 200)),
            };

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("        ▲              ▲");
                Console.WriteLine("       ■■              ■■");
                Console.WriteLine("      ■■■  ░░░░░░░░░░  ■■■");
                Console.WriteLine("     ■■■■■░░░░░░░░░░░░■■■■■");
                Console.WriteLine("      ■■■░  ░░░░░░░░░  ░■■■");
                Console.WriteLine("      ░░░░░░░░░ ░ ░░░░░░░░");
                Console.WriteLine(" 🔥   ░░░░░░░░░░░░░░░░░░░░    🔥");
                Console.WriteLine(" |     ░░░▲▼▲▼▲▼▲▼▲▼▲▼▲░░     | ");
                Console.WriteLine("/|\\      ░░░░░░░░░░░░░░      /|\\");
                Console.ResetColor();
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
                int monsterCount = random.Next(1, 5);

                List<Monster> monsterList = new List<Monster>();
                for (int i = 0; i < monsterCount; i++)
                {
                    int variation = random.Next(0, 6);
                    var m = selectedDungeon.Monster;

                    monsterList.Add(new Monster(
                        m.Name,
                        m.HP + 5 * variation,
                        m.Attack + variation,
                        m.ExpReward + variation,
                        m.GoldReward + variation
                    ));
                }

                Battlesystem battle = new Battlesystem(player, monsterList);
                battle.DungeonEnter();
                Campfire campfire = new Campfire();
                if (player.HP <= 0) break;

                Console.WriteLine("\n전투 후 행동 선택:");
                Console.WriteLine("1. 마을로 돌아가기");
                Console.WriteLine("2. 던전을 더 탐색하기");
                Console.WriteLine("3. 휴식하기");
                Console.Write("선택: ");
                string choice = Console.ReadLine();

                if (choice == "1") //여관으로 돌아가기
                {
                    Town town = new Town(player);
                    town.TownMain();
                    return;
                }
                else if (choice == "2")
                {
                    DungeonMain(player);
                }
                else if (choice == "3")
                {
                    campfire.Rest(player, this);
                }

            }
        }
    }
}