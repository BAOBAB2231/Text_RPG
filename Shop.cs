using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YJTextRPG
{
    class Shop
    {
        public static List<Item> Items { get; set; } = new List<Item>
        {
            new Item
            {
                Name = "수련자 갑옷",
                Type = "방어구",
                Attack = 0,
                Defense = 5,
                Description = "수련에 도움을 주는 갑옷입니다.",
                Price = 1000,
            },

            new Item
            {
                Name = "무쇠갑옷",
                Type = "방어구",
                Attack = 0,
                Defense = 9,
                Description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                Price = 2000,
            },

            new Item
            {
                Name = "스파르타의 갑옷",
                Type = "방어구",
                Attack = 0,
                Defense = 15,
                Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                Price = 3500,
            },

            new Item
            {
                Name = "낡은 검",
                Type = "무기",
                Attack = 2,
                Defense = 0,
                Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                Price = 600,
            },

            new Item
            {
                Name = "청동 도끼",
                Type = "무기",
                Attack = 5,
                Defense = 0,
                Description = "어디선가 사용됐던거 같은 도끼입니다.",
                Price = 1500,
            },

            new Item
            {
                Name = "스파르타의 창",
                Type = "무기",
                Attack = 7,
                Defense = 0,
                Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                Price = 2000,
            },

            new Item
            {
                Name = "튜터의 마우스",
                Type = "무기",
                Attack = 20,
                Defense = 2,
                Description = "튜터의 기운을 받아 머리가 총명해진다.",
                Price = 6500,
            }
        };
        public static void ShowShopMenu(Player player)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    bool alreadyOwned = player.Inventory.Any(inv => inv.Name == item.Name);
                    string status = alreadyOwned ? "보유 중" : $"{item.Price} G";
                    string nameFommatted = item.Name.PadRight(12);
                    string atkFommatted = $"+{item.Attack}".PadRight(3);
                    string defFommatted = $"+{item.Defense}".PadRight(3);
                    Console.WriteLine($"- {item.Name.PadRight(12)} | 공격력 {atkFommatted} 방어력 {defFommatted} | {item.Description} | {status}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기");

                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();

                int choice;

                if (!int.TryParse(input, out choice) || choice > 1 || choice < 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                switch(choice)
                {
                    case 0:
                        break;
                    case 1:
                        BuyItem(player);
                        continue;
                }

                if (choice == 0)
                    break;
            }
            Console.Clear();
        }
        public static void BuyItem(Player player)
        {
            Console.Clear();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    bool isOwnedForDisplay = player.Inventory.Any(inv => inv.Name == item.Name);
                    string status = isOwnedForDisplay ? "보유 중" : $"{item.Price} G";
                    string nameFommatted = item.Name.PadRight(12);
                    string atkFommatted = $"+{item.Attack}".PadRight(3);
                    string defFommatted = $"+{item.Defense}".PadRight(3);
                    Console.WriteLine($"- {i+1} {item.Name.PadRight(12)} | 공격력 {atkFommatted} 방어력 {defFommatted} | {item.Description} | {status}");
                }

                Console.WriteLine("\n0. 나가기");

                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();

                int choice;

                if (!int.TryParse(input, out choice) || choice > Items.Count || choice < 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                if (choice == 0)
                    break;

                var selectedItem = Items[choice - 1];
                bool isAlreadyOwned = player.Inventory.Any(inv => inv.Name == selectedItem.Name);

                if(isAlreadyOwned)
                {
                    continue;
                }

                if(player.Gold < selectedItem.Price)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Gold 가 부족합니다.");
                    Console.ResetColor();
                    continue;
                }

                player.Gold -= selectedItem.Price;

                player.Inventory.Add(new Item
                {
                    Name = selectedItem.Name,
                    Type = selectedItem.Type,
                    Attack = selectedItem.Attack,
                    Defense = selectedItem.Defense,
                    Description = selectedItem.Description,
                    Price = selectedItem.Price,
                    IsEqipped = false,
                });

                if (choice == 0)
                    break;
            }
            Console.Clear();
        }
    }
}
