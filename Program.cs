using System.Net.NetworkInformation;

namespace TXT11
{
    public enum ItemType { Armor, Weapon }

    public class Program
    {


        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("현 생 에 선 게임개발자 인");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" 르 탄 이 가");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n 6 6 6 6 6 년전 ");
            Console.ResetColor();
            Console.WriteLine("이 세 계 로  떨"); 

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n     ■■■■  ■■■■  ■■■■ ✧  🏢");
            Console.Write("    ■     ■     ■        ");
            Console.ResetColor();
            Console.WriteLine("🏢 🤸‍ 어");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     ■■■   ■■■   ■■■     🏢");
            Console.WriteLine("  ✦     ■     ■     ■    🏢");
            Console.Write("✴︎   ■■■■  ■■■■  ■■■■  ");
            Console.ResetColor();
            Console.WriteLine("   🏢    져");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                      급 🏢");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n ⚔️ 소드 마스터⚔️  겸 대마법사📖 도전기 ");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\n\"어서오게나 이세계 청년. 여긴 66666년 전 세계라네.\"");
            Console.WriteLine("\n\"자네 이름이 뭔가?\n");
            string characterName = Console.ReadLine();

            Console.WriteLine($"\n\"{characterName}-. 환영하네.\"");
            Console.ReadLine();
            
            string jobName = "";
            int lv = 1, hp = 0, attack = 0, defense = 0, gold = 0, exp = 0, potioncount = 0;
            float critical = 0;
            bool validJob = false;

            while (!validJob)
            {
                Console.Clear();
                Console.WriteLine($"\"이 세계를 모험을 하기 위해서는 직업이 필요하지.\"");
                Console.WriteLine($"\n\"자, 어떤 길을 떠날텐가?\"");
                Console.WriteLine("\n1. 전사");
                Console.WriteLine("2. 궁수");
                Console.WriteLine("3. 도적\n");
                
                string select = Console.ReadLine();


                if (int.TryParse(select, out int job))
                {
                    switch (job)
                    {
                        case 1:
                            jobName = "전사";
                            hp = 100;
                            attack = 10;
                            defense = 5;
                            gold = 1500;
                            validJob = true;
                            break;
                        case 2:
                            jobName = "궁수";
                            hp = 80;
                            attack = 15;
                            defense = 3;
                            gold = 1500;
                            validJob = true;
                            break;
                        case 3:
                            jobName = "도적";
                            hp = 90;
                            attack = 12;
                            defense = 3;
                            gold = 2000;
                            validJob = true;
                            break;
                        default:
                            Console.WriteLine("\n올바른 숫자를 입력해주세요.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n숫자를 입력해주세요.");
                    Console.ReadLine();
                }

            }
                Console.Clear();
                Console.WriteLine($"\n\"{jobName}... 나쁘지 않은 선택이야.\"");
                Console.WriteLine($"\n\"모험을 떠날 준비가 되었는가?\"");
                Console.ReadKey();
                Console.WriteLine("\n\"그럼 행운을 빌겠네...\"");
                Console.ReadLine();
                Console.WriteLine("\n.");
                Console.WriteLine("\n.");
                Console.WriteLine("\n.");
            Player player = new Player(characterName, jobName, lv, hp, attack, defense, gold, exp, potioncount);
            Town town = new Town(player);
            town.TownMain();
        }
    }

    public class Town
    {
        private Player player;

        Shop shop = new Shop();
        DungeonProgram dungeonProgram = new DungeonProgram();
        Hotel rest = new Hotel();

        public Town(Player player)
        {
            this.player = player;

        }

        public void TownMain()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("마을에 도착했습니다. 여기서는 다양한 활동을 할 수 있습니다.");
                Console.ResetColor();
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식");
                Console.WriteLine("6. 옵션");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string select = Console.ReadLine();
                if (int.TryParse(select, out int menu))
                {
                    switch (menu)
                    {
                        case 1:
                            player.ShowStatus();
                            break;
                        case 2:
                            player.ShowInventory();
                            break;
                        case 3:
                            shop.ShopEnter(player);
                            break;
                        case 4:
                            dungeonProgram.DungeonMain(player);
                            break;
                        case 5:
                            rest.Rest(player);
                            break;
                        case 6:
                            SaveManager.ShowOptionsMenu(player);
                            break;
                        default:
                            Console.WriteLine("올바른 숫자를 입력해주세요.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Console.ReadLine();
                }
            }
        }
    }
}