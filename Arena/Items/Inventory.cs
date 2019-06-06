using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Inventory
    {
        private Item[] inventory = new Item[12];

        public Item this[int a]
        {
            get { return inventory[a]; }
            set { inventory[a] = value; }
        }

    }
}
