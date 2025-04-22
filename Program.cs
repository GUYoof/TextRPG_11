using System.Net.NetworkInformation;

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
            int lv = 1, hp = 0, attack = 0, defense = 0, gold = 0, exp = 0, potioncount = 0;
            float critical = 0;
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




//public class Game
//{
//    PlayerInfo _info = new();
//    Status _status = new();
//    Inventory _inventory = new();

//    Market _Market = new();

//    JobList _job = new();


//    public void Start()
//    {
//        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
//        Console.WriteLine(" 당신의 이름을 입력해주세요.");

//        _info.SetName();


//        Console.WriteLine("당신이 입력한 이름은" + _info.name + "입니다");
//        Console.WriteLine("직업을 선택하세요");
//        Console.WriteLine("1.전사 2. 마법사 3.궁수");
//        int SelectJob = int.Parse(Console.ReadLine());
//        if (SelectJob == 1)
//        {
//            Console.WriteLine("당신의 직업은" + _job.Warrior + "입니다.");
//            _job.SelectedJob = _job.Warrior;
//        }
//        else if (SelectJob == 2)
//        {
//            Console.WriteLine("당신의 직업은" + _job.Magician + " 입니다.");
//            _job.SelectedJob = _job.Magician;
//        }
//        else if (SelectJob == 3)
//        {
//            Console.WriteLine("당신의 직업은" + _job.Archer + "입니다.");
//            _job.SelectedJob = _job.Archer;
//        }

//        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
//        AtTown();
//    }

//    public void AtTown()
//    {
//        Console.WriteLine("1. 상태보기");
//        Console.WriteLine("2. 인벤토리");
//        Console.WriteLine("3. 상점");
//        Console.WriteLine("4. 휴식");
//        Console.WriteLine("원하시는 행동을 선택해주세요.\n>>");
//        int num = int.Parse(Console.ReadLine());
//        if (num == 1)
//        {
//            ShowStatus();
//        }
//        else if (num == 2)
//        {
//            ShowInventory();
//        }
//        else if (num == 3)
//        {
//            ShowMarket();
//        }
//        else if (num == 4)
//        {
//            Rest();
//        }
//        else
//        {
//            Console.WriteLine("잘못된 입력입니다");
//            AtTown();
//        }
//    }

//    // 상태를 보여주는 함수
//    public void ShowStatus()
//    {
//        //  상태창
//        Console.WriteLine("상태보기");
//        Console.WriteLine("캐릭터의 정보가 표시 됩니다.");
//        Console.WriteLine("레벨 :   " + _status.level);
//        Console.WriteLine("직업: " + _job.SelectedJob);
//        Console.WriteLine("이름 :   " + _info.name);
//        Console.WriteLine("공격력 : " + _status.PlayerAtkDmg);
//        Console.WriteLine("방어력 : " + _status.Armor);
//        Console.WriteLine("체력 :   " + _status.hp);
//        Console.WriteLine("GOLD :   " + _status.Gold);

//        AtTown();
//    }

//    public void ShowInventory()
//    {
//        Console.WriteLine("현재 보유 중인 아이템을 관리할 수 있습니다.");
//        Console.WriteLine("[아이템 목록]");
//        Console.WriteLine("1. 장착하기 0. 나가기");
//        int Choose = int.Parse(Console.ReadLine());

//        if (Choose == 1)
//        {

//            {

//            }
//            Console.WriteLine("1. 장착하기 0. 나가기");
//            int Choose1 = int.Parse(Console.ReadLine());
//            if (Choose1 == 1)
//            {
//                Console.WriteLine();
//            }
//            else if (Choose1 == 0)
//            {
//                AtTown();
//            }
//        }
//        else if (Choose == 0)
//        {
//            AtTown();
//        }
//        else
//        {
//            Console.WriteLine("올바른 숫자를 입력하세요요");
//            ShowInventory();
//        }
//    }

//    public void ShowMarket()
//    {
//        Console.WriteLine("필요한 아이템을 얻을수 있는 상점입니다.");
//        {
//            if (_job.SelectedJob == _job.Warrior)
//            {
//                Console.WriteLine("1. 구매하기\t 2.그만두기");
//                int Choose1 = int.Parse(Console.ReadLine());

//                if (Choose1 == 1)
//                {
//                    Console.WriteLine("전사 전용무기");
//                    Console.WriteLine();
//                    Console.WriteLine("Sword  | 엄청큰 태도 | ATK + 10 | 200G");      // DMG +10
//                    Console.WriteLine("Shield | 엄청큰 방패 | ATK + 5  | 200G");     // Armor +5
//                    Console.WriteLine("Hammer | 엄청큰 망치 | ATK + 15 | 200G ");     // DMG + 15
//                    Console.WriteLine("Spear  | 엄청긴 창   | ATK + 13 | 200G ");      // DMG + 13
//                }
//                else if (Choose1 == 2)
//                {
//                    AtTown();
//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다");
//                    ShowMarket();
//                }

//            }
//            if (_job.SelectedJob == _job.Magician)
//            {
//                Console.WriteLine("1. 구매하기\t  2.그만두기");
//                int Choose1 = int.Parse(Console.ReadLine());

//                if (Choose1 == 1)
//                {
//                    Console.WriteLine("마법사 전용무기");
//                    Console.WriteLine();
//                    Console.WriteLine("Wands| 아주 작은 요술봉    | Magic + 10 | 200G");     // DMG + 10
//                    Console.WriteLine("Charm| 엄청 큰 부적        | Magic + 13 | 200G");     // DMG + 13
//                    Console.WriteLine("staff| 엄청 큰 마법 지팡이  | Magic + 15 | 200G");     // DMG + 15   
//                }
//                else if (Choose1 == 2)
//                {
//                    AtTown();
//                }
//                {
//                    Console.WriteLine("잘못된 입력입니다");
//                    ShowMarket();
//                }
//            }
//            if (_job.SelectedJob == _job.Archer)
//            {
//                Console.WriteLine("1. 구매하기\t 2.그만두기");
//                int Choose1 = int.Parse(Console.ReadLine());

//                if (Choose1 == 1)
//                {
//                    Console.WriteLine("궁수 전용무기");
//                    Console.WriteLine();
//                    Console.WriteLine("Bow      | 가벼운 활      | ATK + 10 | 200G");         // DMG + 10
//                    Console.WriteLine("Arrow    | 매우 좋은 화살  | ATK + 5 | 200G");       // DMG + 5
//                    Console.WriteLine("CrossBow | 엄청큰 석궁    | ATK + 20 | 200G");    // DMG + 20
//                }
//                else if (Choose1 == 2)
//                {
//                    AtTown();
//                }
//                {
//                    Console.WriteLine("잘못된 입력입니다");
//                    ShowMarket();

//                }
//            }

//        }

//    }


//    //휴식
//    public void Rest()
//    {

//        Console.WriteLine("휴식하기");
//        Console.WriteLine("500G를 내면 체력을 회복 할수 있습니다.");
//        Console.WriteLine(); //공백 추가
//        Console.WriteLine("1. 휴식하기\n 0.나가기");
//        Console.WriteLine();
//        Console.WriteLine("원하시는 행동을 선택해주세요.\n>>");
//        int Choose = int.Parse(Console.ReadLine());
//        if (Choose == 1)
//        {
//            if (_status.Gold >= 500)
//            {
//                Console.WriteLine("휴식을 취합니다.");
//                Console.WriteLine("-500골드");

//                _status.hp = 100;
//                _status.Gold -= 500;
//                AtTown();
//            }
//        }
//        else if (Choose == 0)
//        {
//            AtTown();
//        }
//        else
//        {
//            Console.WriteLine("잘못된 입력입니다");
//            Rest();
//        }
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        Game startgame = new();
//        startgame.Start();
//    }
//}




//// strings.Add("Short1");
//// strings.Add("Short2");
//// strings.Add("Short3");

//// string Short1 = strings[0];

//// strings.Remove("Short1"); // 
//// strings.RemoveAt(0);



//public class PlayerInfo
//{
//    public string name;

//    public void SetName()
//    {
//        name = Console.ReadLine();
//    }

//}

//// public class Warrior
//// {
////     공격력 15 방어력 5 체력 100
//// }

//// public class Magician
//// {
////     공격력 14 방어력 4 체력 80
//// }
//// public class Archer
//// {
////     공격혁 12 방어력3 체력 90
//// }

//public class JobList
//{
//    public string Warrior = "전사";
//    public string Magician = "마법사";
//    public string Archer = "궁사";

//    public string SelectedJob;
//}



//public class Market
//{

//}

////스탯
//public class Status
//{
//    public int level = 1;
//    public int PlayerAtkDmg = 10;
//    public int Armor = 5;
//    public float hp = 100.0f;
//    public int Gold = 1500;
//}

//// 아이템템 생성성
//public class Items
//{
//    // 생성자
//    public Items(string itemName, string description, int value, int cost)
//    {
//        Name = itemName;
//        Description = description;
//        Value = value;
//        Cost = cost;
//    }

//    public string Name { get; set; }
//    public string Description { get; set; }
//    public int Value { get; set; }
//    public int Cost { get; set; }
//    public bool Equipped { get; set; }
//    public bool Purchased { get; set; }
//}


//// 인벤토리리
//public class Inventory
//{
//    public List<Items> OwnedItemList = new List<Items>(); // 소유중인 아이템 리스트

//    // 생성자
//    public Inventory()
//    {
//        //  아이템 명칭, 설명, 수치값, 가격

//        Items woodenSword = new Items("나무칼", "플레이어가 기본적으로 장착하는 무기", 5, 0);
//        Items Woodensheild = new Items("나무 방패", "생각보다 튼튼하다", 3, 0);

//        OwnedItemList.Add(woodenSword);
//    }

//    // 구입한 아이템을 추가하는 함수
//    public void AddItem(Items item)
//    {
//        OwnedItemList.Add(item);
//    }

//    // 보유한 아이템을 삭제하는 함수
//    public void RemoveItem(Items item)
//    {
//        if (OwnedItemList.Contains(item) == false) // 보유한 아이템 목록에 매개변수로 받은 아이템이 없을경우
//            return;

//        OwnedItemList.Remove(item); // 삭제가 이루어지는 부분
//    }
//}