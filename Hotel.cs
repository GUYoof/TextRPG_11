using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TXT11;

namespace TXT11
{
    public class Hotel
    {


        public void Rest(Player player)
        {
            Console.Clear();
            Console.WriteLine("여관 이용 하기");

            Console.WriteLine("500G를 내면 여관에서 체력을 회복 할수 있습니다.");
            Console.WriteLine(); //공백 추가
            Console.WriteLine("1. 휴식하기\n 0.나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해주세요.\n>>");
            int Choose = int.Parse(Console.ReadLine());
            if (Choose == 1)
            {
                if (player.Gold >= 500)
                {
                    Console.Clear() ;
                    Console.WriteLine("휴식을 취합니다.");
                    Console.WriteLine("-500골드");
                    Console.ReadKey();

                    player.HP = player.MaxHP;
                    player.Gold -= 500;
                    
                }
            }
            else if (Choose == 0)
            {
                Console.WriteLine($"골드가 부족합니다.현재 골드 : {player.Gold}");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                Rest(player);
            }
        }
    }
    
    public class Campfire
    {
        
        public void Rest(Player player, DungeonProgram dungeonProgram)
        {
            Console.Clear();
            Console.WriteLine("모닥불에서 휴식하기");

            Console.WriteLine("1. 휴식하기\n 0.나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해주세요.\n>>");
            int Choose = int.Parse(Console.ReadLine());
            if (Choose == 1)
            {
                Console.WriteLine("휴식을 취합니다. 체력 +30 회복");
                player.HP += 30;
                if (player.HP > player.MaxHP)
                {
                    player.HP = player.MaxHP;
                }

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
                Console.WriteLine($"현재 HP: {player.HP}");
                Console.WriteLine("\n아침이 밝아옵니다 다음엔 어떤 던전을 탐사할까요?");
                Console.ReadKey();
                dungeonProgram.DungeonMain(player);
            }
            else if (Choose == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                Rest(player, dungeonProgram);
            }
        }
    }
}
