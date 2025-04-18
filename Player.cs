using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YJTextRPG
{
    public class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; } = 1;
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; } = 150000;

        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, string playerClass)
        {
            Name = name;
            Class = playerClass;

            switch (playerClass)
            {
                case "전사":
                    Attack = 10;
                    Defense = 5;
                    HP = 100;
                    break;
            }

            Inventory.Add(new Item
            {
                Name = "낡은 검",
                Type = "무기",
                Attack = 2,
                Defense = 0,
                Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                IsEqipped = false
            });
        }

        public void PrintStatus()
        {
            Console.Clear();

            while (true)
            {
                int bonusAttack = Inventory
                    .Where(Item => Item.IsEqipped)
                    .Sum(Item => Item.Attack);

                int bonusDefense = Inventory
                    .Where(Item => Item.IsEqipped)
                    .Sum(Item => Item.Defense);

                int totalAttack = Attack + bonusAttack;
                int totalDefense = Defense + bonusDefense;

                Console.WriteLine($"Lv. {Level:D2}");
                Console.WriteLine($"{Name} ( {Class} )");
                Console.WriteLine($"공격력 : {totalAttack} {(bonusAttack > 0 ? $"(+{bonusAttack})" : "")}");
                Console.WriteLine($"방어력 : {totalDefense} {(bonusDefense > 0 ? $"(+{bonusDefense})" : "")}");
                Console.WriteLine($"체 력 : {HP}");
                Console.WriteLine($"Gold : {Gold} G");

                Console.WriteLine("\n0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(": ");
                string input = Console.ReadLine();

                int choice;

                if(!int.TryParse(input, out choice) || choice != 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("올바른 값을 입력해주세요.\n");
                    Console.ResetColor();
                    continue;
                }

                Console.Clear();
                break;
            }
        }
    }
}