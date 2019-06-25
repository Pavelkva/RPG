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

        public PlayerController(Player player, EnemyNpc enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }


        public void PlayerTurn()
        {
            string switcher = Console.ReadLine();
            switch (switcher)
            {
                case "getStats":
                    Console.WriteLine("Armor: " + player.Armor);
                    Console.WriteLine("Strength: " + player.Strength);
                    Console.WriteLine("Agility: " + player.Agility);
                    Console.WriteLine("Intelllect: " + player.Intellect);
                    break;
                case "equip":
                    string equip = Console.ReadLine();
                    Item equipment = Array.Find(player.Inventory, i => i.Name == equip);
                    if (equipment is Equipment)
                    {
                        player.Equip(equipment as Equipment);
                    }
                    break;
                case "unequip":
                    string unequip = Console.ReadLine();
                    Item unequipment = Array.Find(player.Inventory, i => i.Name == unequip);
                    if (unequipment is Equipment)
                    {
                        player.Equip(unequipment as Equipment);
                    }
                    break;
                case "items":
                    foreach (Item item in player.Inventory)
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
                case "attack":
                    player.Attack(enemy);
                    break;
                case "exit":
                    return;
            }
        }


        
    }

  
}