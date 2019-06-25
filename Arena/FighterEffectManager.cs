using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class FighterEffectManager : IFighterEffectManager
    {


        public void AddSpellEffect(Fighter caster, Fighter target)
        {
            
        }

        public void ApplySpellEffects(Fighter target)
        {
            
        }

        public void GetDamageFromAttack(Fighter.AttackArgs attackArgs)
        {
           if (attackArgs.AttackHit == Fighter.AttackArgs.HitByAttack.Hit)
            {
                attackArgs.Target.ActualHp -= attackArgs.Damage;
            }
           if (attackArgs.AttackHit == Fighter.AttackArgs.HitByAttack.CriticalHit)
            {
                attackArgs.Target.ActualHp -= attackArgs.Damage;
            }
           if (attackArgs.AttackHit == Fighter.AttackArgs.HitByAttack.Miss)
            {
                attackArgs.Target.ActualHp -= attackArgs.Damage;
            }
        }
    }
}
