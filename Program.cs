namespace TXT11
{
    public enum ItemType { Armor, Weapon }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("안녕하세요. 스파르타 던전에 오신것을 환영합니다.");
            Console.Write("\n당신의 이름은 무엇입니까?: ");
            string characterName = Console.ReadLine();

            string jobName = "";
            int lv = 1, hp = 0, attack = 0, defense = 0, gold = 0, exp = 0 , potioncount = 0 ;
            float  critical = 0;
            bool validJob = false;

            while (!validJob)
            {
                Console.WriteLine("\n1. 전사");
                Console.WriteLine("2. 궁수");
                Console.WriteLine("3. 도적");
                Console.Write("\n당신의 직업을 선택해주세요: ");
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

            Player player = new Player(characterName, jobName, lv, hp, attack, defense, gold, exp, potioncount, critical);
            Shop shop = new Shop();
            DungeonProgram dungeonProgram = new DungeonProgram();
            Hotel rest = new Hotel();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 것을 환영합니다.");
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식");
                Console.WriteLine("0. 게임 종료");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write("\n>> ");
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
                        case 0:
                            Console.WriteLine("게임을 종료합니다.");
                            return;
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

