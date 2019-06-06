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

        /// <summary>
        /// Forward list of spell effects to caster and target 
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        public void Cast(Fighter caster, Fighter target)
        {
            SpellEffectsForCaster.ForEach(spellBuff => caster.AddSpellEffect(spellBuff));
            SpellEffectsForTarget.ForEach(spellBuff => target.AddSpellEffect(spellBuff));
            CooldownReamingTime = Cooldown;
        }

        /// <summary>
        /// Decrease or increase atribute for specified time then return it to original value for caster.
        /// </summary>
        /// <param name="atribute">Target atribute</param>
        /// <param name="count">How much to change atribute</param>
        /// <param name="time">How long the change is active</param>
        public void AddSpellEffectCaster (BonusAtribute.Atributes atribute, int count, int time)
        {
            SpellEffectsForCaster.Add(new SpellEffect(time, new BonusAtribute(atribute, count, this)));
        }

        /// <summary>
        /// Decrease or increase actual hp or energy pernamently for specified time for caster.
        /// </summary>
        /// <param name="hpOrMana">Target atribute</param>
        /// <param name="count">How much to change atribute</param>
        /// <param name="time">How many rounds the effect repeat. 0 for instant effect</param>
        /// <param name="modificatorNumber">Multiplier between basic number and atribute</param>
        /// <param name="modifcatorType">Atribute which modifies value</param>
        public void AddSpellEffectCaster(SpellEffect.FlatAtribute hpOrMana, int count, int time, float modificatorNumber, SpellEffect.Modificator modifcatorType)
        {
            SpellEffectsForCaster.Add(new SpellEffect(time, hpOrMana, count, modificatorNumber, modifcatorType));
        }

        /// <summary>
        /// Decrease or increase atribute for specified time then return it to original value for target.
        /// </summary>
        /// <param name="atribute">Target atribute</param>
        /// <param name="count">How much to change atribute</param>
        /// <param name="time">How long the change is active</param>
        public void AddSpellEffectTarget(BonusAtribute.Atributes atribute, int count, int time)
        {
            SpellEffectsForTarget.Add(new SpellEffect(time, new BonusAtribute(atribute, count, this)));
        }

        /// <summary>
        /// Decrease or increase actual hp or energy pernamently for specified time for target.
        /// </summary>
        /// <param name="hpOrMana">Target atribute</param>
        /// <param name="count">How much to change atribute</param>
        /// <param name="time">How many rounds the effect repeat. 0 for instant effect</param>
        /// <param name="modificatorNumber">Multiplier between basic number and atribute</param>
        /// <param name="modifcatorType">Atribute which modifies value</param>
        public void AddSpellEffectTarget(SpellEffect.FlatAtribute hpOrMana, int count, int time, float modificatorNumber, SpellEffect.Modificator modifcatorType)
        {
            SpellEffectsForTarget.Add(new SpellEffect(time, hpOrMana, count, modificatorNumber, modifcatorType));
        }


    }
}
