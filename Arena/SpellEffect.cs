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
        public enum Trigger { OnCast, OnAttack, TurnStart, TurnEnd }
        public Trigger SpellTrigger {get; private set;}
        public float ModificatorNumber { get; private set; }
        public int? FlatNumber { get; private set; }
        public int FinalNumber { get; set; } = 0;

        

        /// <summary>
        /// Decrease or increase atribute for specified time then return it to original value
        /// </summary>
        /// <param name="time">Number of rounds when effect is active. -1 for permanent</param>
        /// <param name="bonusAtribute">Atribute to edit</param>
        public SpellEffect (int time, BonusAtribute bonusAtribute, Modificator modAtribute, Trigger spellTrigger = Trigger.OnCast)
        {
            Time = time;
            BonusAtribute = bonusAtribute;
            RemainingTime = time;
            ModificatorAtribute = modAtribute;
            SpellTrigger = spellTrigger;
        }

        /// <summary>
        /// Decrease or increase actual hp or energy pernamently for specified time.
        /// </summary>
        /// <param name="time">Number of rounds when effect is active. 0 for instant effect</param>
        /// <param name="flatAtribute">Specify energy or hp</param>
        /// <param name="flatNumber">How much actual energy or hp</param>
        /// <param name="modNumber"></param>
        /// <param name="modAtribute"></param>
        public SpellEffect(int time, FlatAtribute flatAtribute, int flatNumber,  float modNumber, Modificator modAtribute, Trigger trigger = Trigger.OnCast)
        {
            Time = time;
            FlatAtributeTarget = flatAtribute;
            RemainingTime = time;
            FlatNumber = flatNumber;
            ModificatorAtribute = modAtribute;
            ModificatorNumber = modNumber;
            Trigger = trigger;
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
