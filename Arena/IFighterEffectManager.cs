namespace Arena
{
    // Manager is checking every spell effect 
    public interface IFighterEffectManager
    {
        void GetDamageFromAttack(Fighter.AttackArgs attackArgs);
        void AddSpellEffect(Fighter caster, Fighter target);
        void ApplySpellEffects(Fighter target);

    }
}