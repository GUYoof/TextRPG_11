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
                    Console.WriteLine("휴식을 취합니다.");
                    Console.WriteLine("-500골드");

                    player.HP = player.MaxHP;
                    player.Gold -= 500;
                    
                }
            }
            else if (Choose == 0)
            {
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
                Console.WriteLine($"현재 HP: {player.HP}");

                Console.WriteLine("엔터를 누르면 던전으로 다시 돌아갑니다...");
                Console.ReadLine();
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
