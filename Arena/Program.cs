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
            

            Player player = new Player("Bojovnik1");
            EnemyNpc enemy = new EnemyNpc("Bojovnik2");
            FightingPit fightingPit = new FightingPit(player, enemy);
            Inventory inventory = new Inventory();
            Spell spellSily = new Spell("sila", 10, 2);
            Spell spellSily2 = new Spell("sila2", 10, 2);
            spellSily2.AddSpellBuffTarget(SpellEffect.FlatAtribute.hp, -2, 0, 1, SpellEffect.Modificator.intelect);
            spellSily2.AddSpellBuffTarget(SpellEffect.FlatAtribute.hp, -2, 5, 0.1f, SpellEffect.Modificator.intelect);
            Spell spellSily3 = new Spell("sila3", 10, 2);

            spellSily.AddSpellBuffCaster(BonusAtribute.Atributes.StrengthBonus, 5, 5);
           

            Weapon zbran = new Weapon("sekac", Weapon.handle.TwoHand);
            zbran.AddAtribute(BonusAtribute.Atributes.AgilityBonus, 50);
            zbran.AddAtribute(BonusAtribute.Atributes.StrengthBonus, 10);
            zbran.MaxDamage = 2;
            zbran.MinDamage = 1;

            inventory[0] = zbran;
            fightingPit.AddItemToPlayerControler(zbran);

            player.SpellBook.Add(spellSily2);


            player.OnAttack += ukaz;
            enemy.OnAttack += ukaz;


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
    }
}
