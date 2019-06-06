using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    public class SpellEffect
    {
        public int Time { get; private set; }
        private int remainingTime;
        public int RemainingTime
        {
            get
            {
                return remainingTime;
            }
            set
            {
                remainingTime = value;
                if (remainingTime > Time)
                {
                    remainingTime = Time;
                }
            }
        }
        public BonusAtribute BonusAtribute { get; private set; }
        public enum FlatAtribute { hp, energy };
        public FlatAtribute FlatAtributeTarget { get; private set; }
        public enum Modificator
        {
            strength,
            agility,
            intelect
        }
        public Modificator ModificatorAtribute { get; private set; }
        public float ModificatorNumber { get; private set; }
        public int? FlatNumber { get; private set; }
        public int FinalNumber { get; set; } = 0;
        

        /// <summary>
        /// Modify atribute for certain time.
        /// </summary>
        /// <param name="time">Number of rounds when effect is active. -1 For permanent</param>
        /// <param name="bonusAtribute">Atribute to edit</param>
        public SpellEffect (int time, BonusAtribute bonusAtribute)
        {
            Time = time;
            BonusAtribute = bonusAtribute;
            RemainingTime = time;
        }

        public SpellEffect(int time, FlatAtribute flatAtribute, int flatNumber,  float modNumber, Modificator modAtribute)
        {
            Time = time;
            FlatAtributeTarget = flatAtribute;
            RemainingTime = time;
            FlatNumber = flatNumber;
            ModificatorAtribute = modAtribute;
            ModificatorNumber = modNumber;
        }

        public override string ToString()
        {
            if (BonusAtribute != null)
                return string.Format("Atribute: {0} Time: {1}/{2}", BonusAtribute, RemainingTime, Time);
            else
                return string.Format("Damage: {0} Time: {1}/{2}", FinalNumber, RemainingTime, Time);
        }
    }
}
