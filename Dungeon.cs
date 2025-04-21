using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT11
{
    public class Dungeon
    {
        public void DungeonEnter(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1. 쉬운 던전    | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전    | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전  | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write("\n>> ");
                string select = Console.ReadLine();

                if (int.TryParse(select, out int Difficulty))
                {
                    switch (Difficulty)
                    {
                        case 1:
                            EasyDungeon(player);
                            break;
                        case 2:
                            NormalDongeo(player);
                            break;
                        case 3:
                            HardDongeo(player);
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("올바른 숫자를 입력하세요.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력하세요.");
                    Console.ReadLine();
                }
            }
        }

        public void EasyDungeon(Player player)
        {
            Console.Clear();
            Console.WriteLine("쉬운 던전");
            Console.WriteLine("방어력 5 이상 권장하는 던전 입니다.");

            if (player.GetTotalDefense() >= 5)
            {
                DungeonClear(player, 5, 1000);
            }
            else
            {
                ConditionsLack(player, 5);
            }

            Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
            Console.ReadLine();
        }

        public void NormalDongeo(Player player)
        {
            Console.Clear();
            Console.WriteLine("보통 던전");
            Console.WriteLine("방어력 11 이상 권장하는 던전 입니다.");

            if (player.GetTotalDefense() >= 11)
            {
                DungeonClear(player, 11, 1000);
            }
            else
            {
                ConditionsLack(player, 11);
            }

            Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
            Console.ReadLine();
        }

        public void HardDongeo(Player player)
        {
            Console.Clear();
            Console.WriteLine("어려운 던전");
            Console.WriteLine("방어력 17 이상 권장하는 던전 입니다.");

            if (player.GetTotalDefense() >= 17)
            {
                DungeonClear(player, 17, 1000);
            }
            else
            {
                ConditionsLack(player, 17);
            }

            Console.WriteLine("\n엔터를 누르면 던전 선택으로 갑니다.");
            Console.ReadLine();
        }

        public void DungeonClear(Player player, int requiredDefense, int baseReward)
        {
            Console.WriteLine("\n던전 클리어");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine("던전을 클리어 하였습니다.");
            Console.WriteLine("\n[탐험 결과]");

            int defenseCorrectionValue = player.GetTotalDefense() - requiredDefense;
            int beforeHp = player.HP;
            int beforeGold = player.Gold;

            // 기본 범위에서 방어력 보정 적용
            int minDamage = 20 - defenseCorrectionValue;
            int maxDamage = 35 - defenseCorrectionValue;

            Random damage = new Random();
            int playerDamage = damage.Next(minDamage, maxDamage + 1); // 범위 포함

            player.HP -= playerDamage;

            if (player.HP < 0)
            {
                player.HP = 0;
            }

            // 기본 보상에서 보너스 보상 적용
            int minBonusPercent = player.Attack;
            int maxBonusPercent = player.Attack * 2;

            Random Percent = new Random();
            int bonusPercent = Percent.Next(minBonusPercent, maxBonusPercent + 1);

            int bonusGold = baseReward * bonusPercent / 100;
            int totalGold = baseReward + bonusGold;

            player.Gold += totalGold;

            Console.WriteLine($"체력 {beforeHp} → {player.HP}");
            Console.WriteLine($"Gold {beforeGold} G → {player.Gold} G");
        }

        public void ConditionsLack(Player player, int requiredDefense)
        {
            Console.WriteLine("\n방어력이 부족하여 던전 탐험이 실패 할 수 있습니다.");

            Random rand = new Random();
            int chance = rand.Next(0, 101);

            if (chance <= 40)
            {
                Console.WriteLine("\n던전 실패...");
                Console.WriteLine("방어력이 부족하여 몬스터에게 패했습니다.");
                Console.WriteLine("보상 없이 되돌아왔습니다.");

                int defenseCorrectionValue = player.GetTotalDefense() - requiredDefense;
                int beforeHp = player.HP;

                int damage = player.HP / 2; // 절반 데미지
                player.HP -= damage;
                if (player.HP < 0)
                {
                    player.HP = 0;
                }

                Console.WriteLine($"체력 {beforeHp} → {player.HP}");
            }
            else
            {
                Console.WriteLine("\n운좋게 던전을 클리어했습니다!");
                DungeonClear(player, 5, 1000); // 난이도별 값 전달
            }
        }
    }
}
