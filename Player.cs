﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TXT11;
using static System.Net.Mime.MediaTypeNames;

namespace TXT11
{
    public class Player
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; } // = 1;
        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; } // = 5;
        public int PotionCount { get; set; } = 0;
        public float Critical { get; set; }
        public float Avoid { get; set; }
        
        private static Random rand = new Random();

        private static readonly int[] LevelRequirements = { 10, 35, 65, 100 }; // 레벨업에 필요한 경험치

        //포션사용함수
        public void UsePotion()
        {
            //포션 없을때의 예외처리
            if (PotionCount <= 0)
            {
                Console.WriteLine("포션이 없습니다!");
                Console.WriteLine("빈 인벤토리를 뒤적거리다가 쳐 맞았습니다!");
                return;
            }
            else
            {
                int heal = 30;
                HP += heal;
                if (HP > MaxHP) HP = MaxHP;
                PotionCount--;
                Console.WriteLine($"포션 사용!");
                Console.WriteLine($"남은 포션 개수 : {PotionCount}");
            }
        }

        // 레벨업 보상 경험치 ...
        public void GainExp(int amount)
        {
            Console.WriteLine($"경험치 +{amount}");
            int prevLevel = Level;
            int prevExp = Exp;
            float prevAtk = Attack;
            int prevDef = Defense;

            Exp += amount;

            while (Level - 1 < LevelRequirements.Length && Exp >= LevelRequirements[Level - 1])
            {
                Exp -= LevelRequirements[Level - 1];
                Level++;
                Attack += 0.5f;
                Defense += 1;
                Console.WriteLine($"레벨업! Lv.{Level - 1} → Lv.{Level}");
                Console.WriteLine($"공격력 {prevAtk} → {Attack}");
                Console.WriteLine($"방어력 {prevDef} → {Defense}");
            }

            Console.WriteLine($"Exp {prevExp} → {Exp}");


        }
        //인벤토리 리스트로 구현
        public List<Item> Inventory { get; set; } = new List<Item>();
        //플레이어 정의
        public Player(string name, string job, int level, int hp, float attack, int defense, int gold, int exp , int potioncount)
        {
            Name = name;
            Job = job;
            Level = level;
            HP = hp;
            Attack = attack;
            Defense = defense;
            Gold = gold;
            Exp = exp;
            PotionCount = potioncount;
            //플레이어 직업에 따라 최대 체력과 치명 회피율 조정
            switch (job.ToLower())
            {
                case "전사":
                    MaxHP = 100;
                    Critical = 0.2f;
                    Avoid = 0.1f;
                    break;
                case "도적":
                    MaxHP = 90;
                    Critical = 0.4f;
                    Avoid = 0.3f;
                    break;
                case "궁수":
                    MaxHP = 80;
                    Critical = 0.3f;
                    Avoid = 0.2f;
                    break;
                default:
                    MaxHP = 100; // 기본값
                    break;
            }
        }
        //플레이어 능력치 적용에 필요한 함수
        public bool CriticalChance()
        {
            float roll = (float)rand.NextDouble();
            return roll < Critical;
        }

        public bool AvoidChance()
        {
            float roll = (float)rand.NextDouble();
            return roll < Avoid;
        }

        public float GetTotalAttack()
        {
            float bonus = Inventory.Where(i => i.Type == ItemType.Weapon).Sum(i => i.Attack);
            return Attack + bonus;
        }

        public int GetTotalDefense()
        {
            int bonus = Inventory.Where(i => i.Type == ItemType.Armor).Sum(i => i.Defense);
            return Defense + bonus;
        }
        //플레이어 스테이터스 함수
        public void ShowStatus()
        {
            float bonusAttack = 0;
            int bonusDefense = 0;

            foreach (Item item in Inventory)
            {
                if (item.IsEquipped == true)
                {
                    bonusAttack += item.Attack;
                    bonusDefense += item.Defense;
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===========================");
            Console.WriteLine($"| 이름   : {Name}");
            Console.WriteLine($"| 레벨   : {Level}");
            Console.WriteLine($"| 경험치 : {Exp}");
            Console.WriteLine($"| 직업   : {Job}");
            Console.WriteLine($"| 체력   : {HP}");

            // 보너스가 있는 경우에만 (+숫자) 출력
            string attackText = bonusAttack > 0 ? $"{Attack} +({bonusAttack})" : $"{Attack}";
            string defenseText = bonusDefense > 0 ? $"{Defense} +({bonusDefense})" : $"{Defense}";

            Console.WriteLine($"| 공격력 : {attackText}");
            Console.WriteLine($"| 방어력 : {defenseText}");
            Console.WriteLine($"| Gold   : {Gold}");
            Console.WriteLine("===========================");
            Console.ResetColor();
            Console.WriteLine("엔터를 누르면 이전 화면으로 갑니다.");
            Console.ReadLine();

        }

        public void InventoryItemList()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    var item = Inventory[i];
                    string equippedMark = item.IsEquipped ? "[E] " : "";

                    string statText = "";
                    if (item.Type == ItemType.Weapon && item.Attack > 0)
                        statText = $"공격력 +{item.Attack}";
                    else if (item.Type == ItemType.Armor && item.Defense > 0)
                        statText = $"방어력 +{item.Defense}";

                    Console.WriteLine($"{i + 1}. {equippedMark}{item.Name} {statText} {item.Description} ({item.Type})");
                }
            }
        }
        //플레이어 인벤토리 함수
        public void ShowInventory()
        {
            while (true)
            {
                InventoryItemList();

                Console.WriteLine($"\n포션 개수 : {PotionCount}");
                Console.WriteLine("\n\n1. 장착하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하는 행동을 입력해주세요.");
                string select = Console.ReadLine();

                if (int.TryParse(select, out int menuChoice))
                    if (menuChoice == 0)
                    {
                        return; // 인벤토리 종료
                    }
                    else if (menuChoice == 1)
                    {
                        ProcessedEquipment();
                    }
                    else
                    {
                        Console.WriteLine("올바른 번호를 입력해주세요.");
                        Console.ReadLine();
                    }
                else
                {
                    Console.WriteLine("숫자를 입력하세요.");
                    Console.ReadLine();
                }
            }
        }
        //장비 아이템 장착 구현
        public void ProcessedEquipment()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("\n[아이템 목록]");

                if (Inventory.Count == 0)
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < Inventory.Count; i++)
                    {
                        var item = Inventory[i];
                        string equippedMark = item.IsEquipped ? "[E] " : "";

                        string statText = "";
                        if (item.Type == ItemType.Weapon && item.Attack > 0)
                            statText = $"공격력 +{item.Attack}";
                        else if (item.Type == ItemType.Armor && item.Defense > 0)
                            statText = $"방어력 +{item.Defense}";

                        Console.WriteLine($"{i + 1}. {equippedMark}{item.Name} {statText} {item.Description} ({item.Type})");
                    }
                }
                Console.WriteLine("\n장착하거나 해제할 아이템을 선택해 주세요.");
                Console.WriteLine("\n0. 취소하고 돌아가기");
                Console.Write("\n선택: ");
                string select = Console.ReadLine();

                if (int.TryParse(select, out int index))
                {
                    if (index == 0)
                        return;
                    else if (index >= 1 && index <= Inventory.Count)
                    {
                        EquippedItem(index);
                    }
                    else
                    {
                        Console.WriteLine("올바른 번호를 입력해주세요.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Console.ReadLine();
                }
            }
        }

        public void EquippedItem(int index)
        {
            Item selectedItem = Inventory[index - 1];

            if (!selectedItem.IsEquipped)
            {
                foreach (Item item in Inventory)
                    if (item.Type == selectedItem.Type && item.IsEquipped)
                    {
                        item.IsEquipped = false;
                        Console.WriteLine($"{item.Name}이 장착 해제되었습니다.");
                    }
                selectedItem.IsEquipped = true;
                Console.WriteLine($"\n{selectedItem.Name}을(를) 장착했습니다.");
            }
            else
            {
                selectedItem.IsEquipped = false;
                Console.WriteLine($"\n{selectedItem.Name}을(를) 해제했습니다.");
            }

            Console.WriteLine("\n엔터를 누르면 돌아갑니다.");
            Console.ReadLine();
        }

        public void CopyFrom(Player other)
        {
            this.Name = other.Name;
            this.Job = other.Job;
            this.Level = other.Level;
            this.HP = other.HP;
            this.MaxHP = other.MaxHP;
            this.Attack = other.Attack;
            this.Defense = other.Defense;
            this.Gold = other.Gold;
            this.Exp = other.Exp;
            this.PotionCount = other.PotionCount;
            this.Critical = other.Critical;
            this.Avoid = other.Avoid;

            this.Inventory = new List<Item>();
            foreach (var item in other.Inventory)
            {
                // Item 클래스에 복사 생성자가 없으므로 새 객체로 생성
                this.Inventory.Add(new Item(item.Name, item.Description, item.Price, item.Type, item.Attack, item.Defense)
                {
                    IsSold = item.IsSold,
                    IsEquipped = item.IsEquipped
                });
            }
        }
    }
}
