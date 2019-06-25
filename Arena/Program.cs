using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Player player = new Player("Bojovnik1", new FighterEffectManager());
            EnemyNpc enemy = new EnemyNpc("Bojovnik2", new FighterEffectManager());
            FightingPit fightingPit = new FightingPit(player, enemy);
            
            Spell ignite = new Spell("ohen", 10, 2);
            ignite.AddSpellEffectTarget(SpellEffect.FlatAtribute.hp, -2, 0, 1, SpellEffect.Modificator.intelect);
            ignite.AddSpellEffectTarget(SpellEffect.FlatAtribute.hp, -2, 5, 0.1f, SpellEffect.Modificator.intelect);
            ignite.AddSpellEffectCaster(SpellEffect.FlatAtribute.hp, -2, 0, 0, SpellEffect.Modificator.intelect);
            player.SpellBook.Add(ignite);

            Spell spellSily = new Spell("sila", 50, 2);
            spellSily.AddSpellEffectCaster(BonusAtribute.Atributes.StrengthBonus, 5, 5, 0, SpellEffect.Modificator.intelect);
            player.SpellBook.Add(spellSily);

            Weapon zbran = new Weapon("obourucak", Weapon.handle.TwoHand);
            zbran.AddAtribute(BonusAtribute.Atributes.StrengthBonus, 10);
            zbran.MaxDamage = 10;
            zbran.MinDamage = 5;
            player.AddToInventory(zbran);

            Weapon zbran2 = new Weapon("dyka", Weapon.handle.OneHand);
            zbran2.MaxDamage = 4;
            zbran2.MinDamage = 1;
            player.AddToInventory(zbran2);

            Equipment stit = new Equipment("stit", Equipment.Parts.OffHand);
            stit.AddAtribute(BonusAtribute.Atributes.ArmorBonus, 50);
            player.AddToInventory(stit);




            player.OnAttack += ukaz;
            enemy.OnAttack += ukaz;
            player.OnSpellCast += ukazSpell;
            enemy.OnSpellCast += ukazSpell;


            while (true)
            {
                fightingPit.ShowFighters();
                fightingPit.nextTurn();
                foreach (SpellEffect spellProperty in player.GetSpellBuffs())
                {
                    Console.WriteLine("player:" + spellProperty);
                }
                foreach (SpellEffect spellProperty in enemy.GetSpellBuffs())
                {
                    Console.WriteLine("enemy: " + spellProperty);
                }

            }
        }

        static void ukaz(object sender, Fighter.AttackArgs e)
        {
            Console.WriteLine(sender + " - " + e.AttackHit  + " for " + e.Damage + " on " + e.Target );
        }
        static void  ukazSpell(object sender, Fighter.SpellCastArgs e)
        {
            Console.WriteLine(sender + " - " + e.Cast + " " + e.SpellName);
        }
    }
}
