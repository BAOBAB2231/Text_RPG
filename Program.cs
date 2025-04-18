using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using YJTextRPG;

namespace YJTextRPG
{
    internal class Program
    {
        private static Player player;
        static void Main(string[] args)
        {
            string name = SetPlayerName();

            string chosenClass = SetPlayerClass();

            player = new Player(name, chosenClass);

            ShowMainMenu();

        }
        private static string SetPlayerName()
        {
            string playerName = "";

            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("당신의 이름을 알려주세요.\n");
                Console.Write(": ");
                string input = Console.ReadLine();
                Console.Clear();

                int choice = 0;

                while (true)
                {
                    Console.WriteLine($"당신의 이름은 {input}이(가) 맞습니까?\n");

                    Console.WriteLine("1. 예");
                    Console.WriteLine("2. 아니오\n");

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(": ");
                    bool isValid = int.TryParse(Console.ReadLine(), out choice);

                    if (choice == 1)
                    {
                        playerName = input;
                        break;
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("올바른 값을 입력해주세요.\n");
                        Console.ResetColor();
                    }
                }
                if (choice == 1) break;
            }
            Console.Clear();
            return playerName;
        }
        private static string SetPlayerClass()
        {
            string playerClass = "";

            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");

                Console.WriteLine("1. 전사");

                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(": ");

                int choice = 0;
                bool isValid = int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        playerClass = "전사";
                        Console.WriteLine("당신의 직업은 전사입니다.");
                        Thread.Sleep(2000);
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("올바른 값을 입력해주세요.\n");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
            Console.Clear();
            return playerClass;
        }
        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");

                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();

                int choice = 0;

                if (!int.TryParse(input, out choice) || choice > 3 || choice <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        player.PrintStatus();
                        continue;
                    case 2:
                        ShowInventory();
                        continue;
                    case 3:
                        Shop.ShowShopMenu(player);
                        continue;
                }
            }
        }
        private static void ShowInventory()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("[아이템 목록]");

                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("\n1. 장착 관리");
                    Console.WriteLine("0. 나가기");
                }
                else
                {
                    foreach (var item in player.Inventory)
                    {
                        string equipped = item.IsEqipped ? "[E]" : "   ";
                        string nameFormatted = $"{equipped}{item.Name}".PadRight(12);
                        Console.WriteLine($"- {nameFormatted} | 공격력 +{item.Attack} 방어력 +{item.Defense} | {item.Description}");
                    }
                    Console.WriteLine("\n1. 장착 관리");
                    Console.WriteLine("0. 나가기");
                }

                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();

                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        ManageEquipments();
                        continue;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("올바른 값을 입력해주세요.\n");
                        Console.ResetColor();
                        continue;
                }
                if (choice == 0)
                    break;
            }
        }
        private static void ManageEquipments()
        {
            while (true)
            {
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var Item = player.Inventory[i];
                    string equipped = Item.IsEqipped ? "[E]" : "   ";
                    string nameFommatted = $"{equipped}{Item.Name}".PadRight(12);
                    Console.WriteLine($"- {i + 1} {nameFommatted} | 공격력 +{Item.Attack} 방어력 +{Item.Defense} | {Item.Description}");
                }

                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                if (choice == 0)
                {
                    Console.Clear();
                    break;
                }

                if(choice < 1 || choice > player.Inventory.Count)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                var selectedItem = player.Inventory[choice - 1];

                if(selectedItem.IsEqipped)
                {
                    selectedItem.IsEqipped = false;
                }
                else
                {
                    foreach(var item in player.Inventory)
                    {
                        if((item.Type == selectedItem.Type) && item.IsEqipped)
                        {
                            item.IsEqipped = false;
                        }
                    }

                    selectedItem.IsEqipped = true;
                }
                Console.Clear();
            }
        }
    }
}