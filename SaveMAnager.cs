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
    public static class SaveMAnager
    {
        private static string path = "save.txt";
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
                    string statText = "";

                    if (item.Type == ItemType.Weapon && item.Attack > 0)
                        statText = $"공격력 +{item.Attack}";
                    else if (item.Type == ItemType.Armor && item.Defense > 0)
                        statText = $"방어력 +{item.Defense}";

                    inventory += $"{equippedMark}{item.Name} {statText} {item.Description} ({item.Type})";
                }

                // 마지막 쉼표 제거 후 저장
                writer.WriteLine(inventory);
            }
            Console.WriteLine("저장 되었습니다.");
        }
        public static void Lord()
        {
        }

        public static void Reset()
        {
        }
    }
}