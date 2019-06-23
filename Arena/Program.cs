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
            Inventory inventory = new Inventory();
            Spell spellSily = new Spell("Strenght", 10, 2);
            Spell Ignite = new Spell("Ignite", 10, 2);
            Ignite.AddSpellEffectTarget(SpellEffect.FlatAtribute.hp, -2, 0, 1, SpellEffect.Modificator.intelect);
            Ignite.AddSpellEffectTarget(SpellEffect.FlatAtribute.hp, -2, 5, 0.1f, SpellEffect.Modificator.intelect);
            Ignite.AddSpellEffectCaster(SpellEffect.FlatAtribute.hp, -2, 0, 0, SpellEffect.Modificator.intelect);
            Spell spellSily3 = new Spell("sila3", 10, 2);

            spellSily.AddSpellEffectCaster(BonusAtribute.Atributes.StrengthBonus, 5, 5);
           

            Weapon zbran = new Weapon("sekac", Weapon.handle.TwoHand);
            zbran.AddAtribute(BonusAtribute.Atributes.AgilityBonus, 50);
            zbran.AddAtribute(BonusAtribute.Atributes.StrengthBonus, 10);
            zbran.MaxDamage = 2;
            zbran.MinDamage = 1;

            inventory[0] = zbran;
            fightingPit.AddItemToPlayerControler(zbran);

            player.SpellBook.Add(Ignite);


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
