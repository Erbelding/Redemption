using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Items;

namespace RedemptionEngine.ObjectClasses
{
    public class Character: Entity
    {
        #region Constants
        private const int MAX_WEAPONS = 2;
        private const int MAX_ARMOR = 4;
        #endregion

        #region Attributes

        //Basic Character Information
        protected string name;//Character name

        //Character Stats

        private CharacterAttribute level;
        private CharacterAttribute experience;

        private CharacterAttribute health; //represents health bar values
        private CharacterAttribute stamina; //represents stamina bar values
        private CharacterAttribute mana; //represents mana bar values


        private CharacterAttribute defense; //modifies damage recieved
        private CharacterAttribute strength; //modifies physical damage output
        private CharacterAttribute magic; //modifies magic damage output

        private Weapon[] weapons = new Weapon[MAX_WEAPONS]; //what the character has equipped
        private Armor[] armor = new Armor[MAX_ARMOR]; //what the character is wearing
        private List<Item> inventory = new List<Item>();

        #endregion



        #region Methods

        public void DamageCharacter(int amount, bool ignoreDefense) //deal damage to character
        {
            int damage; //damage variable
            if (ignoreDefense) damage = amount; //damage if ignoring defense
            else if (amount < 0) //damage is negative; heal player instead
            {
                HealCharacter(amount);
                return; //don't do damage twice
            }
            else damage = amount - defense.Value; //use damage calculation
            health.Value -= damage; //apply damage
        }

        public void HealCharacter(int amount) //heal character
        {
            health.Value += amount;
        }

        public virtual void Attack(Weapon w)//character attacks
        {
            int damage = 0;
            Random rand = new Random();
            damage = (int)(strength.Value + strength.Value * rand.NextDouble() * rand.NextDouble());
            w.Attack(damage);
        }

        public void EquipWeapon(Weapon weapon, int slot)//put a weapon in the designated slot
        {
            slot = (int)MathHelper.Clamp(slot, 1, MAX_WEAPONS);
            if (weapons[slot - 1] != null)//move equipped item back to inventory
            {
                Weapon removedweapon = weapons[slot - 1];
                AddItem(removedweapon);

                for (int i = 0; i < removedweapon.Modifiers.Count; i++)
                {
                    removedweapon.Modifiers[i].UnModify();
                }
            }
            for (int i = 0; i < weapon.Modifiers.Count; i++)
            {
                weapon.Modifiers[i].Modify();
            }
            this.weapons[slot - 1] = weapon; //equip new item


        }

        public void EquipArmor(Armor armor, int slot)//put armor in the designated slot
        {
            slot = (int)MathHelper.Clamp(slot, 1, MAX_ARMOR);
            if (this.armor[slot - 1] != null)//move equipped item back to inventory
            {
                Armor removedArmor = this.armor[slot - 1];
                AddItem(removedArmor);

                for (int i = 0; i < removedArmor.Modifiers.Count; i++)
                {
                    removedArmor.Modifiers[i].UnModify();
                }
            }
            for (int i = 0; i < armor.Modifiers.Count; i++)
            {
                armor.Modifiers[i].Modify();
            }
            this.armor[slot - 1] = armor; //equip new item
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        #endregion
        //my oh my we have a lot of work to do here
    }
}
