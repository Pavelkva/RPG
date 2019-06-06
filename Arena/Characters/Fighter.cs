using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    public class Fighter
    {
        public string Name { get; set; }
        public int BonusHp { get; set; }
        public int BaseHp { get; set; }
        public int MaxHp
        {
            get { return BaseHp + BonusHp + 10 * BonusStrength + 10 * BaseStrength; }
            private set { }
        }
        private int actualHp;
        public int ActualHp
        {
            get
            {
                if (actualHp > MaxHp)
                    actualHp = MaxHp;
                return actualHp;
            }
            set
            {
                actualHp = value;
            }
        }
        public bool IsAlive
        {
            get
            {
                if (actualHp > 0)
                    return true;
                else
                    return false;
            }
            private set {}
        }
        public int BonusEnergy { get; set; }
        public int BaseEnergy { get; set; }
        public int MaxEnergy
        {
            get { return BaseEnergy + BonusEnergy + 10 * BaseIntellect + 10 * BonusIntellect; }
            protected set { }
        }
        private int actualEnergy;
        public int ActualEnergy
        {
            get
            {
                if (actualEnergy > MaxEnergy)
                    actualEnergy = MaxEnergy;
                return actualEnergy;
            }
            set
            {
                actualEnergy = value;
            }
        }
        public int BonusStrength { get; set; }
        public int BaseStrength { get; set; }
        public int Strength
        {
            get { return BaseStrength + BonusStrength; }
            private set {}
        }
        public int BonusAgility { get; set; }
        public int BaseAgility { get; set; }
        public int Agility
        {
            get { return BaseAgility + BonusAgility; }
            private set {}
        }
        public int BonusIntellect { get; set; }
        public int BaseIntellect { get; set; }
        public int Intellect
        {
            get { return BaseIntellect + BonusIntellect; }
            private set {}
        }
        private int attackPower;
        public int AttackPower
        {
            get
            {
                if ((attackPower + Strength) < 0)
                    return 0;
                else
                return attackPower + Strength;
            }
            set
            {
                attackPower = value;
            }
        }
        private int maxDamage;
        public int MaxDamage { get { return AttackPower + maxDamage; } set { maxDamage = value; } }
        private int minDamage;
        public int MinDamage { get { return AttackPower + minDamage;  } set { minDamage = value; } }
        public int Armor { get; set; } = 0;
        private int bonusDodgeChance;
        public int BonusDodgeChance { get { return bonusDodgeChance + BonusAgility; } set { bonusDodgeChance = value; } }
        private int baseDodgeChance;
        public int BaseDodgeChance { get { return baseDodgeChance + BaseAgility; } set { baseDodgeChance = value; } }
        public int DodgeChance
        {
            get
            {
                if ((BaseDodgeChance + BonusDodgeChance) < 0)
                    return 0;
                else
                return BaseDodgeChance + BonusDodgeChance; 
            }
            private set { }
        }
        private int baseCriticalStrikeChance;
        public int BaseCriticalStrikeChance { get { return baseCriticalStrikeChance + BaseAgility; } set { baseCriticalStrikeChance = value; } }
        private int bonusCriticalStrikeChance;
        public int BonusCriticalStrikeChance { get { return bonusCriticalStrikeChance + BonusAgility;  } set { bonusCriticalStrikeChance = value; } }
        public int CriticalHitChance
        {
            get
            {
                if ((BonusCriticalStrikeChance + BaseCriticalStrikeChance) < 0)
                    return 0;
                else
                    return BaseCriticalStrikeChance + BonusCriticalStrikeChance;
            }
            private set { }
        }
        public double DamageModificator { get { return 100 / (100 + Convert.ToDouble(Armor)); } private set { } }
        protected List<SpellEffect> spellEffects;
        protected List<BonusAtribute> bonusAtributes;
        private List<Spell> spellBook;
        public List<Spell> SpellBook
        {
            get
            {
                foreach (Spell spell in spellBook)
                {
                    foreach (SpellEffect spellEffect in spell.SpellEffectsForCaster)
                    {
                        if (spellEffect.FlatNumber != null)
                        {
                            spellEffect.FinalNumber = CalculateBonusFlatNumber(spellEffect) + (int)spellEffect.FlatNumber;
                        }
                    }
                    foreach (SpellEffect spellEffect in spell.SpellEffectsForTarget)
                    {
                        if (spellEffect.FlatNumber != null)
                        {
                            spellEffect.FinalNumber = CalculateBonusFlatNumber(spellEffect) + (int)spellEffect.FlatNumber;
                        }
                    }
                }

                int CalculateBonusFlatNumber(SpellEffect spellEffect)
                {
                    int finalNumber;
                    int bonusFromStats = Convert.ToInt32(Math.Round(spellEffect.ModificatorNumber * Convert.ToSingle(Intellect), 0));
                    if (spellEffect.FlatNumber < 0)
                        finalNumber = (-1) * bonusFromStats;
                    else
                        finalNumber = bonusFromStats;

                    return finalNumber;
                }
                return spellBook;
            }

            set
            {
                spellBook = value;
            }

        }

        public event EventHandler<AttackArgs> OnAttack;
        public event EventHandler<SpellCastArgs> OnSpellCast;

        public class AttackArgs: EventArgs
        {
            public enum HitByAttack { Hit, CriticalHit, Miss }
            public HitByAttack AttackHit  { get; set; }
            public int Damage { get; set; }
            public Fighter Target { get; set; }
        }

        public class SpellCastArgs : EventArgs
        {
            public enum Success { OnCooldown, NotEnougthEnergy, Casted }
            public Success Cast { get; set; }
        }

        protected Fighter(string name, int maxHp, int maxEnergy, int strength=10, int agility=10, int intellect=10, int armor=0)
        {
            spellEffects = new List<SpellEffect>();
            bonusAtributes = new List<BonusAtribute>();
            spellBook = new List<Spell>();
            BaseStrength = strength;
            BaseAgility = agility;
            BaseIntellect = intellect;
            Name = name;
            BaseHp = maxHp;
            BaseEnergy = maxEnergy;
            ActualHp = MaxHp;
            ActualEnergy = MaxEnergy;  
            Armor = armor;
        }

        protected virtual void Attacked(AttackArgs.HitByAttack attackHit, Fighter target, int damage)
        {
            OnAttack?.Invoke(this, new AttackArgs() { AttackHit = attackHit, Target = target, Damage = damage });
        }
        protected virtual void Casted(SpellCastArgs.Success success)
        {
            OnSpellCast?.Invoke(this, new SpellCastArgs() { Cast = success });
        }

        public void Attack(Fighter fighter)
        {
            if (IsAlive && fighter.IsAlive) // Check if target alive
            {
                Random random = new Random();
                int hit = random.Next(1, 100);
                if (hit > fighter.DodgeChance) // Check if hit
                {
                    double damage = random.Next(MinDamage, MaxDamage + 1);
                    damage = damage * fighter.DamageModificator;
                    damage = Math.Round(damage, 0);
                    int damageInt = Convert.ToInt32(damage); // hit
                    hit = random.Next(1, 100);
                    if (hit <= CriticalHitChance) // Check if crit
                    {
                        damageInt = damageInt * 2;
                        fighter.ActualHp -= damageInt; // Crit
                        Attacked(AttackArgs.HitByAttack.CriticalHit, fighter, damageInt); 
                    }
                    else
                    {
                        fighter.ActualHp -= damageInt; // normal hit
                        Attacked(AttackArgs.HitByAttack.Hit, fighter, damageInt);
                    }
                }
                else
                {
                    Attacked(AttackArgs.HitByAttack.Miss, fighter, 0); // miss
                }
            }
        }

        
        public void AddToSpellBook (Spell spell)
        {
            SpellBook.Add(spell);
        }

        /// <summary>
        /// Add spell effect to spell effect list or sum flat atribute
        /// </summary>
        /// <param name="spellEffect">Spell effect from spell</param>
        public void AddSpellEffect(SpellEffect spellEffect)
        {
            bool spellPropertyActive = false;

            // Deal damage if flatNumber set and time is 0
            if (spellEffect.FlatNumber != null)
            {
                if (spellEffect.Time == 0) // if time 0 heal or damage immediately
                {
                    if (spellEffect.FlatAtributeTarget == SpellEffect.FlatAtribute.hp)
                    ActualHp += spellEffect.FinalNumber;
                    if (spellEffect.FlatAtributeTarget == SpellEffect.FlatAtribute.energy)
                        ActualEnergy += spellEffect.FinalNumber;
                }
                else
                {
                    spellEffects.Add(spellEffect);
                }
            }
            else
            {
                // check if spell buff already active - reset time
                foreach (SpellEffect activeSpellEffect in spellEffects)
                {
                    if (spellEffect == activeSpellEffect)
                    {
                        spellPropertyActive = true;
                        activeSpellEffect.RemainingTime = activeSpellEffect.Time;
                    }
                }

                // if spell buff not active - add it and bonus atributes
                if (!spellPropertyActive)
                {
                    spellEffects.Add(spellEffect);
                    if (spellEffect.BonusAtribute != null)
                    {
                        bonusAtributes.Add(spellEffect.BonusAtribute);
                        ApplyBonusAtributes();
                    }
                }
            }    
        }

        /// <summary>
        /// Apply damage from damage over time effects
        /// </summary>
        public void ApplyFlatDamage ()
        {
            foreach (SpellEffect spellEffect in spellEffects)
            {
                if (spellEffect.FlatNumber != null)
                {
                    if (spellEffect.FlatAtributeTarget == SpellEffect.FlatAtribute.hp)
                        ActualHp += spellEffect.FinalNumber;
                    if (spellEffect.FlatAtributeTarget == SpellEffect.FlatAtribute.energy)
                        ActualEnergy += spellEffect.FinalNumber;
                }
            }
        }

        public List<SpellEffect> GetSpellBuffs()
        {
            foreach (SpellEffect activeSpellEffect in spellEffects)
            {
                if (activeSpellEffect.RemainingTime == 0)
                {
                    bonusAtributes.Remove(activeSpellEffect.BonusAtribute);
                    ApplyBonusAtributes();
                }
            }
            spellEffects.RemoveAll(spellEffect => spellEffect.RemainingTime == 0);
            return spellEffects;
        }

        public void CastSpell (Fighter target, Spell spell)
        {
            if (spell.IsOnCooldown)
            {
                Casted(SpellCastArgs.Success.OnCooldown);
            }
            else if(spell.EnergyCost > ActualEnergy)
            {
                Casted(SpellCastArgs.Success.NotEnougthEnergy);
            }
            else
            {
                spell.Cast(this, target);
                ActualEnergy -= spell.EnergyCost;
                Casted(SpellCastArgs.Success.Casted);
            }
        }

        protected void ApplyBonusAtributes()
        {
            BonusHp = 0;
            BonusEnergy = 0;
            Armor = 0;
            BonusStrength = 0;
            BonusAgility = 0;
            BonusIntellect = 0;
            BonusCriticalStrikeChance = 0;
            BonusDodgeChance = 0;
            foreach (BonusAtribute playerBonusAtribute in bonusAtributes)
            {
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.HpBonus)
                    BonusHp += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.EnergyBonus)
                    BonusEnergy += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.ArmorBonus)
                    Armor += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.StrengthBonus)
                    BonusStrength += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.AgilityBonus)
                    BonusAgility += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.IntelectBonus)
                    BonusIntellect += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.CriticalChanceBonus)
                    BonusCriticalStrikeChance += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.DodgeChangeBonus)
                    BonusDodgeChance += playerBonusAtribute.BonusNumber;
                if (playerBonusAtribute.Atribute == BonusAtribute.Atributes.DodgeChangeBonus)
                    BonusDodgeChance += playerBonusAtribute.BonusNumber;
            }
        }

        public override string ToString()
        {
            string fighter = String.Format("{0} HP: {1}/{2}", Name, ActualHp, MaxHp);
            return fighter;
        }
    }

    
}
