using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXT11;

namespace TXT11
{
    public class Shop
    {
        public List<Item> Items { get; private set; }

        public Shop()
        {
            Items = new List<Item>()
                {
                    new Item("티셔츠", "| 얇은 천옷입니다.", 500, ItemType.Armor, 0, 2),
                    new Item("수련자 갑옷", "| 수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, 0, 5),
                    new Item("무쇠 갑옷", "| 무쇠로 만들어져 튼튼한 갑옷입니다.", 1800, ItemType.Armor, 0, 9),
                    new Item("스파르타의 갑옷", "| 전설의 갑옷입니다.", 3500, ItemType.Armor, 0, 15),
                    new Item("몽둥이", "| 나무로 만들어진 몽둥이입니다.", 300, ItemType.Weapon, 1, 0),
                    new Item("낡은 검", "| 쉽게 볼 수 있는 낡은 검입니다.", 600, ItemType.Weapon, 2, 0),
                    new Item("청동 도끼", "| 어딘가 사용된 흔적이 있는 도끼입니다.", 1500, ItemType.Weapon, 5, 0),
                    new Item("스파르타의 창", "| 전설의 전사들이 사용한 창입니다.", 2700, ItemType.Weapon, 7, 0)
                };
        }

        public void ShowItemList(Player player)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            foreach (var item in Items)
            {
                string priceText = item.IsSold ? "판매 완료" : $"{item.Price} G";

                string statText = "";
                if (item.Type == ItemType.Weapon && item.Attack > 0)
                {
                    statText = $"공격력 +{item.Attack}";
                }
                else if (item.Type == ItemType.Armor && item.Defense > 0)
                {
                    statText = $"방어력 +{item.Defense}";
                }
                Console.WriteLine($"- {item.Name} : {statText} {item.Description} ({priceText})");
            }
        }

        public void PotionShopEnter(Player player)
        {
            while (true)
            {
                int maxPotionCount = 5;
                Console.Clear();
                Console.WriteLine("포션상점");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine($"\n [(최대소지갯수) : {maxPotionCount}]");

                Console.WriteLine($" 포션 가격: 30G " );
                Console.Write("몇 개 구매하시겠습니까?");
                Console.WriteLine("[0]. 상점으로 돌아가기");
                do
                {
                    if (int.TryParse(Console.ReadLine(), out int amount) && amount >= 0 )
                    {
                        int cost = amount * 30;


                        if (amount == 0)
                        {
                            ShopEnter(player);
                        }

                        else if (amount <= maxPotionCount - player.PotionCount)
                        {
                            if (player.Gold >= cost)
                            {
                                player.Gold -= cost;
                                player.PotionCount += amount;
                                Console.WriteLine($"포션 {amount}개 구매 완료. 현재 포션{player.PotionCount} / {maxPotionCount}(최대포션갯수) 남은 골드: {player.Gold}");
                            }
                            else Console.WriteLine("골드가 부족합니다.");
                        }
                        else Console.WriteLine("소지 가능한 포션 수를 초과했습니다.");
                    }

                    else Console.WriteLine("올바른 값을 입력해주세요");
                } while (true);
            }
        }
            

        public void ShopEnter(Player player)
        {
            while (true)
            {
                ShowItemList(player);
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 포션 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하는 행동을 입력하세요.");
                Console.Write("\n선택: ");

                string select = Console.ReadLine();
                do
                {
                    if (int.TryParse(select, out int output) && output >= 0)
                    {
                        if (output == 0)
                        {
                            Town town = new Town(player);
                            town.TownMain();
                        }
                        else if (output == 1)
                        {
                            ProceedPurchase(player);
                        }
                        else if (output == 2)
                        {
                            PotionShopEnter(player);
                        }
                        else
                        {
                            Console.WriteLine("올바른 번호를 입력해주세요.");
                        }
                        Console.WriteLine("숫자를 입력해주세요.");

                    }
                } while (true);

            }
        }

        public void ProceedPurchase(Player player)
        {
            Console.Clear();
            Console.WriteLine("구매할 아이템 번호를 선택하세요.\n");

            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                string priceText = item.IsSold ? "판매 완료" : $"{item.Price} G";

                string statText = "";
                if (item.Type == ItemType.Weapon && item.Attack > 0)
                {
                    statText = $"공격력 +{item.Attack}";
                }
                else if (item.Type == ItemType.Armor && item.Defense > 0)
                {
                    statText = $"방어력 +{item.Defense}";
                }
                Console.WriteLine($"{i + 1}.  {item.Name} : {statText} {item.Description} ({priceText})");
            }
            Console.WriteLine("\n어떤 아이템을 구매하겠습니까? 번호를 입력하세요.");
            Console.WriteLine("\n0. 취소하고 돌아가기");
            Console.Write("\n선택: ");

            string select = Console.ReadLine();

            if (int.TryParse(select, out int index))
            {
                if (index == 0)
                {
                    ShopEnter(player);
                }
                else if (index >= 1 && index <= Items.Count)
                {
                    HandlePurchase(player, index);
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");

                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");

            }
        }

        public void HandlePurchase(Player player, int index)
        {
            var selectedItem = Items[index - 1];
            if (player.Inventory.Any(i => i.Name == selectedItem.Name))
                Console.WriteLine("이미 구매한 아이템입니다.");
            else if (player.Gold >= selectedItem.Price)
            {
                player.Gold -= selectedItem.Price;
                selectedItem.IsSold = true;
                player.Inventory.Add(selectedItem);
                Console.WriteLine($"'{selectedItem.Name}'을(를) 구매했습니다!");
            }
            else Console.WriteLine("Gold가 부족합니다.");
            Console.ReadLine();
        }
    }
}
