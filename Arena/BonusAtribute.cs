using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    public class BonusAtribute
    {
        public enum Atributes { ArmorBonus, StrengthBonus, AgilityBonus, IntelectBonus, HpBonus, EnergyBonus, DodgeChangeBonus, CriticalChanceBonus}
        public Atributes Atribute { get; set; }
        public int BonusNumber { get; set; }
        public object Source { get; private set; }

        public BonusAtribute(Atributes atribute, int bonusNumber, object source)
        {
            Atribute = atribute;
            BonusNumber = bonusNumber;
            Source = source;
        }

        public override string ToString()
        {
            return Atribute + ": " + BonusNumber;
        }
    }
}
