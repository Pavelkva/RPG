using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Arena
{
    class FightingPit
    {
        public int TurnNumber { get; set; }
        PlayerController playerController;
        EnemyNpc enemyNpc;
        Player player;


        public FightingPit (Player player, EnemyNpc enemyNpc)
        {
            TurnNumber = 0;
            this.enemyNpc = enemyNpc;
            this.player = player;
            playerController = new PlayerController(this.player, this.enemyNpc);
        }

        public string ShowFighters()
        {
            return player.ToString() + " /n " + enemyNpc.ToString();
        }

        public void nextTurn()
        {
            player.ApplyFlatDamage();
            player.GetSpellBuffs().ForEach(spellPropert => spellPropert.RemainingTime -= 1);
            player.GetSpellBuffs().RemoveAll(spellPropert => spellPropert.RemainingTime == 0);
            player.SpellBook.ForEach(spell => spell.CooldownReamingTime -= 1);
            playerController.PlayerTurn();
            enemyNpc.ApplyFlatDamage();
            enemyNpc.GetSpellBuffs().ForEach(spellPropert => spellPropert.RemainingTime -= 1);
            enemyNpc.GetSpellBuffs().RemoveAll(spellPropert => spellPropert.RemainingTime == 0);
            enemyNpc.SpellBook.ForEach(spell => spell.CooldownReamingTime -= 1);
            enemyNpc.PlayTurn(player);
            TurnNumber += 1;
        }

        public void AddItemToPlayerControler( Equipment equipment)
        {
            playerController.AddItem(equipment);
        }

        public void AddSpellToPlayerControler(Spell spell)
        {
            playerController.AddSpell(spell);
        }
    }
}
