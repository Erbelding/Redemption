/**
 * David Erbelding
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Items;
using Microsoft.Xna.Framework.Content;

namespace RedemptionEngine.ObjectClasses
{
    public class Character: Entity
    {
        #region Attributes

        //Basic Character Information
        protected string name; //Character name // currently unused
        public ContentManager Content { get { return content; } }

        //Character Stats

        protected CharacterAttribute level; //represents the level of this character
        protected CharacterAttribute experience; //represents the experience of this character

        //base values used for calculation
        protected CharacterAttribute baseHealth; //represents health bar values
        protected CharacterAttribute baseStamina; //represents stamina bar values
        protected CharacterAttribute baseMana; //represents mana bar values
        protected CharacterAttribute baseDefense; //modifies damage received
        protected CharacterAttribute baseStrength; //modifies physical damage output
        protected CharacterAttribute baseMagic; //modifies magic damage output


        //values actually used in-game
        protected CharacterAttribute health; //represents health bar values
        protected CharacterAttribute stamina; //represents stamina bar values
        protected CharacterAttribute mana; //represents mana bar values
        protected CharacterAttribute defense; //modifies damage received
        protected CharacterAttribute strength; //modifies physical damage output
        protected CharacterAttribute magic; //modifies magic damage output


        protected Inventory inventory; //inventory


        //status effects
        protected List<StatusEffect> statusEffects;
        protected List<StatusEffect> statusEffectsToRemove;


        private ContentManager content;

        //Gets or sets the characters inventory
        public Inventory Inventory { get { return inventory; } set { inventory = value; } }

        

        //base stats
        public virtual CharacterAttribute Level { get { return level; } set { level = value; } }
        public CharacterAttribute Experience { get { return experience; } set { experience = value; } }
        //used stats
        public CharacterAttribute Health { get { return health; } set { health = value; } }
        public CharacterAttribute Stamina { get { return stamina; } set { stamina = value; } }
        public CharacterAttribute Mana { get { return mana; } set { mana = value; } }
        public CharacterAttribute Defense { get { return defense; } set { defense = value; } }
        public CharacterAttribute Strength { get { return strength; } set { strength = value; } }
        public CharacterAttribute Magic { get { return magic; } set { magic = value; } }

        public CharacterAttribute BaseHealth { get { return baseHealth; } }
        public CharacterAttribute BaseStamina { get { return baseStamina; } }
        public CharacterAttribute BaseMana { get { return baseMana; } }
        public CharacterAttribute BaseDefense { get { return baseDefense; } }
        public CharacterAttribute BaseStrength { get { return baseStrength; } }
        public CharacterAttribute BaseMagic { get { return baseMagic; } }
        #endregion


        #region Constructor

        public Character(ContentManager content)
            :base(content)
        {
            statusEffects = new List<StatusEffect>();
            statusEffectsToRemove = new List<StatusEffect>();
            this.content = content;
        }

        

        #endregion

        #region Methods

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //stat changes based on items are applied here
            ApplyAllModifiers(gameTime);
            ApplyAllStatusEffects(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void InitializeAttributes()
        {
            health = new CharacterAttribute("Health", 0, 0);
            if (baseHealth != null)
            {
                health.MaxValue = baseHealth.MaxValue;
                health.Value = baseHealth.Value;
            }
            stamina = new CharacterAttribute("Stamina", 0, 0);
            if (baseStamina != null)
            {
                stamina.MaxValue = baseStamina.MaxValue;
                stamina.Value = baseStamina.Value;
            }
            mana = new CharacterAttribute("Mana", 0, 0);
            if (baseMana != null)
            {
                mana.MaxValue = baseMana.MaxValue;
                mana.Value = baseMana.Value;
            }
            defense = new CharacterAttribute("Defense", 0, 0);
            if (baseDefense != null)
            {
                defense.MaxValue = baseDefense.MaxValue;
                defense.Value = baseDefense.Value;
            }
            strength = new CharacterAttribute("Strength", 0, 0);
            if (baseStrength != null)
            {
                strength.MaxValue = baseStrength.MaxValue;
                strength.Value = baseStrength.Value;
            }
            magic = new CharacterAttribute("Magic", 0, 0);
            if (baseMagic != null)
            {
                magic.MaxValue = baseMagic.MaxValue;
                magic.Value = baseMagic.Value;
            }
        }

        #region Experience/Leveling
        public void AddExperience(int exp)
        {
            if (Level.Value >= 100) return;
            int expLeft = experience.MaxValue - experience.Value;//how much till exp is full?
            expLeft -= exp;//this will be negative if you gained more than enough exp to level up

            experience.Value += exp;//adds to exp until it's full

            if (experience.Value == experience.MaxValue)
            {
                LevelUp( 0 - expLeft ); //the passed in value is added on extra
            }
        }

        public void LevelUp(int exp)
        {
            if (Level.Value >= 100) return;
            Level.Value++;//increment level
            experience.Value = 0;
            experience.MaxValue = Level.Value * 10 + (int)(10 * Math.Pow(1.2, Level.Value));
            //apply stat changes
            baseHealth.MaxValue += (5 + DropItems.Rand.Next(6));
            baseHealth.Value = baseHealth.MaxValue;

            baseStamina.MaxValue += (2 + DropItems.Rand.Next(4));
            baseStamina.Value = baseStamina.MaxValue;

            baseMana.MaxValue += (2 + DropItems.Rand.Next(4));
            baseMana.Value = baseMana.MaxValue;

            baseDefense.Value += (1 + DropItems.Rand.Next(2));
            baseStrength.Value += (1 + DropItems.Rand.Next(2));
            baseMagic.Value += (1 + DropItems.Rand.Next(2));
            //add excess exp
            AddExperience(exp);
            InitializeAttributes();


            SplashText levelUp = new SplashText(gameManager, new Vector2(center.X, center.Y) , "LEVEL UP", Color.Cyan, 2.5f);
            gameManager.AddEntity(levelUp);

        }
        #endregion

        #region Modifiers

       

        public void ApplyAllModifiers(GameTime gameTime) //includes time effects
        {
            //set original stats
            health.MaxValue = baseHealth.MaxValue;
            stamina.MaxValue = baseStamina.MaxValue;
            mana.MaxValue = baseMana.MaxValue;

            defense.MaxValue = baseDefense.MaxValue;
            defense.Value = baseDefense.Value;
            strength.MaxValue = baseStrength.MaxValue;            
            strength.Value = baseStrength.Value;
            magic.MaxValue = baseMagic.MaxValue;
            magic.Value = baseMagic.Value;

            //apply modifiers for stats
            if (Inventory.EquipmentLeft.Item != null) foreach (Modifier mod in Inventory.EquipmentLeft.Item.Modifiers) mod.Modify(this, gameTime);
            if (Inventory.EquipmentRight.Item != null) foreach (Modifier mod in Inventory.EquipmentRight.Item.Modifiers) mod.Modify(this, gameTime);
            if (Inventory.EquipmentHead.Item != null) foreach (Modifier mod in Inventory.EquipmentHead.Item.Modifiers) mod.Modify(this, gameTime);
            if (Inventory.EquipmentBody.Item != null) foreach (Modifier mod in Inventory.EquipmentBody.Item.Modifiers) mod.Modify(this, gameTime);
            if (Inventory.EquipmentLegs.Item != null) foreach (Modifier mod in Inventory.EquipmentLegs.Item.Modifiers) mod.Modify(this, gameTime);
            if (Inventory.EquipmentFeet.Item != null) foreach (Modifier mod in Inventory.EquipmentFeet.Item.Modifiers) mod.Modify(this, gameTime);

            //make sure all stats are correct by calling set (clamps value)
            health.Value = health.Value;
            stamina.Value = stamina.Value;
            mana.Value = mana.Value;
            defense.Value = defense.Value;
            strength.Value = strength.Value;
            magic.Value = magic.Value;
            
        }

        #endregion

        #region Damage Calculation
        public virtual void TakeDamage(Character attacker, int amount, bool ignoreDefense) //deal damage to character
        {
            TakeDamage(attacker, amount, ignoreDefense, Color.White);
        }

        public virtual void TakeDamage(Character attacker, int amount, bool ignoreDefense, Color textColor) //deal damage to character
        {
            int damage; //damage variable
            if (amount < 0) //damage is negative; heal player instead
            {
                HealCharacter(-amount);
                return; //don't do damage twice
            }

            if (ignoreDefense) damage = amount; //damage if ignoring defense (A Lot)

            else damage = amount / (int)MathHelper.Clamp(defense.Value, 1, defense.MaxValue); //use damage calculation
            health.Value -= damage; //apply damage
            if (health.Value <= 0)
            {
                Die(attacker);
            }

            GameManager.AddEntity(new SplashText(GameManager, Center, damage.ToString(), textColor, 1.5f));
        }

        public void HealCharacter(int amount) //heal character
        {
            if (health.Value < health.MaxValue)
            {
                health.Value += amount;
                GameManager.AddEntity(new SplashText(GameManager, Center, "+ " + amount.ToString(), Color.Magenta, 1.5f));
            }
        }
        #endregion

        # region Status Effects
        public void ApplyAllStatusEffects(GameTime gameTime)
        {
            //loop through and apply effects
            foreach (StatusEffect effect in statusEffects)
            {
                effect.Update(gameTime);
            }
            //remove effects
            foreach (StatusEffect effect in statusEffectsToRemove)
            {
                statusEffects.Remove(effect);
            }
            //clear list
            statusEffectsToRemove.Clear();
        }

        public void AddEffect(StatusEffect effect)
        {
            statusEffects.Add(effect);
        }

        public void RemoveEffect(StatusEffect effect)
        {
            statusEffectsToRemove.Add(effect);
        }

        #endregion

        public virtual void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)Math.Pow(1.25, level);
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
        }

        public virtual void Attack(Weapon w)//character attacks
        {
            w.Alpha = 1f;
            w.Attack(this);
        }

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        

        #endregion
    }
}
