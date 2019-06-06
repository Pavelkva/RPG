using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Equipment: Item
    {
        public enum Parts { Helmet, Necklace, Armor, Pants, Ring, OffHand, Weapon};
        public Parts Part { get; protected set; }
        
        public List<BonusAtribute> BonusAtributes { get; private set; } = new List<BonusAtribute>();

        protected Equipment(string name): base(name)
        {
        }

        public Equipment(string name, Parts part): base(name)
        {
            Name = name;
            Part = part;
        }

        public void AddAtribute (BonusAtribute.Atributes atribute, int bonus)
        {
            BonusAtributes.Add(new BonusAtribute(atribute, bonus, this));
        }
        
        public override string ToString()
        {
            string toString = String.Format("Name: {0} Type: {1}", Name, Part);
            foreach (BonusAtribute bonusAtribute in BonusAtributes)
            {
              string atributes = String.Format(" - {0}: {1}", bonusAtribute.Atribute, bonusAtribute.BonusNumber);
                toString += atributes;
            }
            return toString;
        }
    }
}