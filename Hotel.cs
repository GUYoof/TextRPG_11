using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TXT11;

namespace TXT11
{
    public class Hotel
    {
        

        public void Rest(Player player)
        {
            Console.WriteLine("휴식하기");

            Console.WriteLine("500G를 내면 체력을 회복 할수 있습니다.");
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

                    player.HP= 100;
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
}
