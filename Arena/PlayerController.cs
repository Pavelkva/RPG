using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Arena
{
    class PlayerController
    {
        private Player player;
        private Fighter enemy;
        ReadAtribut readAtribut = new ReadAtribut();

        public List<Equipment> items = new List<Equipment>();
        public List<Spell> spells = new List<Spell>();

        public PlayerController(Player player, EnemyNpc enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void AddItem (Equipment equipment)
        {
            items.Add(equipment);
        }
        public void AddSpell(Spell spell)
        {
            spells.Add(spell);
        }

        public void PlayerTurn()
        {
            string switcher = Console.ReadLine();
            switch (switcher)
            {
                case "strength":
                    player.BonusStrength += readAtribut.ReadNumber();
                    break;
                case "agility":
                    player.BonusAgility += readAtribut.ReadNumber();
                    break;
                case "intellect":
                    player.BonusIntellect += readAtribut.ReadNumber();
                    break;
                case "maxHp":
                    player.BonusHp += readAtribut.ReadNumber();
                    break;
                case "maxEnergy":
                    player.BonusEnergy += readAtribut.ReadNumber();
                    break;
                case "armor":
                    player.Armor += readAtribut.ReadNumber();
                    break;
                case "attackPower":
                    player.AttackPower += readAtribut.ReadNumber();
                    break;
                case "attack":
                    player.Attack(enemy);
                    break;
                case "actualHp":
                    player.ActualHp += readAtribut.ReadNumber();
                    break;
                case "actualEnergy":
                    player.ActualEnergy += readAtribut.ReadNumber();
                    break;
                case "equip":
                    string equip = Console.ReadLine();
                    Equipment equipment = items.Find(i => i.Name == equip);
                    player.Equip(equipment);
                    break;
                case "unequip":
                    string unequip = Console.ReadLine();
                    Equipment equipment1 = items.Find(i => i.Name == unequip);
                    player.Unequip(equipment1);
                    break;
                case "items":
                    foreach (Equipment item in items)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case "getEquip":
                    Equipment[] equipments = player.GetEquipment();
                    foreach (Equipment item in equipments)
                    {
                        if (item != null)
                            Console.WriteLine(item.ToString());
                    }
                    break;
                case "cast":
                    string spell = Console.ReadLine();
                    Spell magic = player.SpellBook.Find(i => i.Name == spell);
                    player.CastSpell(enemy, magic);
                    break;
                case "exit":
                    return;
            }
        }


        
    }

  
}