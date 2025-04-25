using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace TXT11
{
    public static class SaveManager
    {   //세이브파일을 바탕화면에 저장
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "save.txt");
        public static void ShowOptionsMenu(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("옵션 메뉴");
                Console.WriteLine("1. 저장하기");
                Console.WriteLine("2. 불러오기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하는 행동을 입력하세요.");
                Console.Write("\n선택: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int select) && select >= 0)
                {
                    switch (select)
                    {
                    case 1:
                        Save(player);
                        break;

                    case 2:
                        Player loadedPlayer = Load();
                        if (loadedPlayer != null)
                        {
                            player.CopyFrom(loadedPlayer); // 저장 / 불러오기용 데이터 복사 매서드를 메인에 만듬
                        }
                        else
                        {
                            Console.WriteLine("불러오기 실패 하였습니다.");
                        }
                        break;

                    case 0:
                        Town town = new Town(player);
                        town.TownMain();
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
        }
        public static void Save(Player player)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine($"Name:{player.Name}");
                writer.WriteLine($"Job:{player.Job}");
                writer.WriteLine($"Level:{player.Level}");
                writer.WriteLine($"HP:{player.HP}");
                writer.WriteLine($"Attack:{player.Attack}");
                writer.WriteLine($"Defense:{player.Defense}");
                writer.WriteLine($"Gold:{player.Gold}");
                writer.WriteLine($"Exp:{player.Exp}");
                writer.WriteLine($"PotionCount:{player.PotionCount}");
                writer.WriteLine($"Critical:{player.Critical}");

                string inventory = "Inventory:";
                foreach (var item in player.Inventory)
                {
                    string equippedMark = item.IsEquipped ? "[E]" : "";
                    writer.WriteLine($"Item:{equippedMark}|{item.Name}|{item.Price}|{item.Description}|{item.Type}|{item.Attack}|{item.Defense}|{item.IsSold}");
                }

                // 마지막 쉼표 제거 후 저장
                writer.WriteLine(inventory);
            }
            Console.WriteLine("저장 되었습니다.\n엔터를 눌러주세요.");
            Console.ReadLine();
        }

        public static Player Load()
        {
            Console.WriteLine($"불러오기 시도 중... 경로: {path}");
            if (!File.Exists(path)) // 파일이 존재하지 않으면 null 반환
            {
                Console.WriteLine("현재 경로에서 파일을 찾을 수 없습니다:");
                Console.WriteLine(path); // 경로 출력해서 디버깅 도움
                Console.WriteLine("엔터를 눌러 계속하세요.");
                Console.ReadLine();
                return null;
            }

            Player player = new Player("DefaultName", "job", 1, 100, 10, 5, 0, 0, 0); // 기본값으로 플레이어 객체 생성
            player.Inventory = new List<Item>(); // Inventory 초기화

            string[] lines = File.ReadAllLines(path); // 파일에서 줄 단위로 읽기

            foreach (string line in lines)
            {
                if (line.StartsWith("Item:"))
                {
                    string itemData = line.Substring("Item:".Length); //"Item:" 이후의 문자열만 잘라서 itemData에 저장
                    string[] parts = itemData.Split('|');   // 저장된 파일에서 아이템 데이터를 | 기호를 기준으로 데이터를 나누고 배열

                    bool isEquipped = parts[0] == "[E]"; //각각의 아이템 정보 파싱
                    string name = parts[1];
                    int price = int.Parse(parts[2]);
                    string description = parts[3];
                    ItemType type = Enum.Parse<ItemType>(parts[4]);
                    float attack = float.Parse(parts[5]);
                    int defense = int.Parse(parts[6]);
                    bool isSold = bool.Parse(parts[7]);
                    
                    // 새로운 Item 객체를 생성하고, 장착 여부와 판매 여부도 설정
                    Item item = new Item(name, description, price, type, attack, defense) 
                    {
                        IsEquipped = isEquipped,
                        IsSold = isSold
                    };

                    player.Inventory.Add(item); // 파싱된 아이템을 플레이어 인벤토리에 추가
                }
                else
                {
                    string[] keyValue = line.Split(':');
                    if (keyValue.Length < 2) continue;

                    string key = keyValue[0];
                    string value = keyValue[1];

                    switch (key) // 키워드에 따라 player의 속성에 값을 저장
                    {
                        case "Name":
                            player.Name = value;
                            break;
                        case "Job":
                            player.Job = value;
                            break;
                        case "Level":
                            player.Level = int.Parse(value);
                            break;
                        case "HP":
                            player.HP = int.Parse(value);
                            break;
                        case "Attack":
                            player.Attack = int.Parse(value);
                            break;
                        case "Defense":
                            player.Defense = int.Parse(value);
                            break;
                        case "Gold":
                            player.Gold = int.Parse(value);
                            break;
                        case "Exp":
                            player.Exp = int.Parse(value);
                            break;
                        case "PotionCount":
                            player.PotionCount = int.Parse(value);
                            break;
                        case "Critical":
                            player.Critical = float.Parse(value);
                            break;
                    }
                }
            }

            Console.WriteLine("불러오기 완료 되었습니다.\n엔터를 눌러주세요.");
            Console.ReadLine();
            return player;
        }
        public static void Reset()
        {
        }
    }
}