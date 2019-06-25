using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Player : Fighter
    {
        private Equipment[] equipmentSlots;
        public Item[] Inventory { get; private set; } 

        public Player(string name, IFighterEffectManager fighterEffectManager, int maxHp=0, int maxEnergy = 0, int strength = 10, int agility = 10, int intellect = 10, int armor = 0) : base(name, fighterEffectManager ,maxHp, maxEnergy, strength, agility, intellect, armor)
        {
            int numSlots = Enum.GetNames(typeof(Equipment.Parts)).Length;
            equipmentSlots = new Equipment[numSlots];
            Inventory = new Item[12]; 
        }

        public Equipment[] GetEquipment()
        {
            return equipmentSlots;
        }

        public void AddToInventory(Item item)
        {
            for(int i = 0; i< Inventory.Length; i++)
            {
                if(Inventory[i] == null)
                {
                    Inventory[i] = item;
                    return;
                }
            }
        }
        public void OutFromInventory(Item item)
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == item)
                {
                    Inventory[i] = null;
                    return;
                }
            }
        }

        public void Equip(Equipment equipment)
        {
            int slotIndex = (int)equipment.Part;
            // Check for free hand for two handed weapon
            if (equipment.Part == Equipment.Parts.Weapon)
            {
                if (equipment is Weapon)
                {
                    if ((equipment as Weapon).Handle == Weapon.handle.TwoHand)
                    {
                        equipmentSlots[(int)Equipment.Parts.OffHand] = null;
                    }
                }
            }
            equipmentSlots[slotIndex] = equipment;
            AddItemBonuses();
        }

        public void Unequip (Equipment equipment)
        {
            foreach (Equipment equip in equipmentSlots)
            {
                if (equip != null)
                {
                    if (equip == equipment)
                    {
                        int slotIndex = (int)equipment.Part;
                        equipmentSlots[slotIndex] = null;
                        bonusAtributes.RemoveAll(item => item.Source == equipment);
                    }
                }   
            }
            ApplyBonusAtributes();
        }

        public void AddItemBonuses()
        {
            bonusAtributes.Clear();
            BonusStrength = 0;
            BonusAgility = 0;
            BonusIntellect = 0;
            BonusHp = 0;
            BonusEnergy = 0;
            BonusCriticalStrikeChance = 0;
            BonusDodgeChance = 0;
            
            if (equipmentSlots[6] is Weapon)
            {
                MaxDamage = (equipmentSlots[6] as Weapon).MaxDamage;
                MinDamage = (equipmentSlots[6] as Weapon).MinDamage;
            }

            foreach (Equipment equipment in equipmentSlots)
            {
                if (equipment != null)
                {
                    foreach (BonusAtribute bonusAtribute in equipment.BonusAtributes)
                    {
                        bonusAtributes.Add(bonusAtribute);
                    }
                }
            }
            ApplyBonusAtributes();
        }
    }
}
