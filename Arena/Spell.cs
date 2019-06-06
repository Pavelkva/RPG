using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    public class Spell
    {
        public string Name { get; set; }
        public Fighter Target { get; set; }
        public Fighter Caster { get; set; }
        public int EnergyCost { get; set; }
        public int Cooldown { get; private set; }
        private int cooldownReamingTime;
        public int CooldownReamingTime
        {
            get
            {
                if (cooldownReamingTime < 0)
                    return 0;
                else
                    return cooldownReamingTime;
                    
            }
            set
            {
                cooldownReamingTime = value;
            }
        }
        private bool isOnCooldown;
        public bool IsOnCooldown
        {
            get
            {
                if (CooldownReamingTime == 0)
                    return false;
                else
                    return true;
            }
                set
            {
                isOnCooldown = value;
            }
        }
        
        public List<SpellEffect> SpellEffectsForCaster { get; private set; }
        public List<SpellEffect> SpellEffectsForTarget { get; private set; }

        /// <summary>
        /// Spell
        /// </summary>
        /// <param name="name">Name of spell</param>
        /// <param name="energycost">Energy cost for spell</param>
        /// <param name="cooldown">Number of turns for spell recovery</param>
        public Spell(string name, int energycost, int cooldown)
        {
            Name = name;
            EnergyCost = energycost;
            Cooldown = cooldown;
            SpellEffectsForCaster = new List<SpellEffect>();
            SpellEffectsForTarget = new List<SpellEffect>();
        }

        public void Cast(Fighter caster, Fighter target)
        {
            SpellEffectsForCaster.ForEach(spellBuff => caster.AddSpellEffect(spellBuff));
            SpellEffectsForTarget.ForEach(spellBuff => target.AddSpellEffect(spellBuff));
            CooldownReamingTime = Cooldown;
        }

        public void AddSpellBuffCaster (BonusAtribute.Atributes atribute, int count, int time)
        {
            SpellEffectsForCaster.Add(new SpellEffect(time, new BonusAtribute(atribute, count, this)));
        }
        public void AddSpellBuffCaster(SpellEffect.FlatAtribute hpOrMana, int count, int time, float modificatorNumber, SpellEffect.Modificator modifcatorType)
        {
            SpellEffectsForCaster.Add(new SpellEffect(time, hpOrMana, count, modificatorNumber, modifcatorType));
        }

        public void AddSpellBuffTarget(BonusAtribute.Atributes atribute, int count, int time)
        {
            SpellEffectsForTarget.Add(new SpellEffect(time, new BonusAtribute(atribute, count, this)));
        }
        public void AddSpellBuffTarget(SpellEffect.FlatAtribute hpOrMana, int count, int time, float modificatorNumber, SpellEffect.Modificator modifcatorType)
        {
            SpellEffectsForTarget.Add(new SpellEffect(time, hpOrMana, count, modificatorNumber, modifcatorType));
        }


    }
}
