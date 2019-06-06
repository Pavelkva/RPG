using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Item
    {
        public string Name { get; protected set; }

        public Item (string name)
        {
            Name = name;
        }
    }
}
