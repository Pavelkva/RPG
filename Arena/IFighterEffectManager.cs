namespace Arena
{
    // Manager is checking every spell effect 
    public interface IFighterEffectManager
    {
        void GetDamageFromAttack(Fighter attacker, Fighter target);
        void AddSpellEffect(Fighter caster, Fighter target);
        void ApplySpellEffects();

    }
}