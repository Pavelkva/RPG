using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Weapon : Equipment
    {
        private int minDamage;
        public int MinDamage
        {
            get
            {
                if (minDamage < 0)
                    minDamage = 0;
                return minDamage;
            }
            set
            {
                minDamage = value;
            }
        }
        private int maxDamage;
        public int MaxDamage
        {
            get
            {
                if (maxDamage < 0)
                    maxDamage = 0;
                return maxDamage;
            }
            set
            {
                maxDamage = value;
            }
        }
        public enum handle { OneHand, TwoHand }
        public handle Handle { get; set; }

        public Weapon(string name, handle handle): base(name)
        {
            Name = name;
            Part = Parts.Weapon;
            Handle = handle;
        }
    }
}
