using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YJTextRPG
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsEqipped { get; set; } = false;
    }
}