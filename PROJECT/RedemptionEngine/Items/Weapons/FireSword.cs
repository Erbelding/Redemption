/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Items.Weapons
{
    public class FireSword : Weapon
    {
        private const float BURN_CHANCE = .2f;
        
        public FireSword(ContentManager content)
            : base(content)
        {
            POWER = 7;
            name = "Fire Sword";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.MELEE;
            texture = content.Load<Texture2D>("Items//Weapons//FireSword");

            AddModifier("Magic + 15", "Magic", 15, false);
        }

        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            

            if (Controller.KeyIsDown(key))
            {
                
            }

            if (Controller.SingleKeyPress(key))
            {
                Attack(owner);
                
            }
        }

        public override void Attack(Character owner)
        {
            gameManager = owner.GameManager;
            int damage = 0;
            double crit = 1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble();
            damage = (int)(owner.Strength.Value * POWER * crit);

            WeaponSwing swing = null;

            if (owner.Dir == Direction.LEFT)
            {
                swing = new WeaponSwing(this, owner, Origin, new Vector2(-owner.Origin.X/2, 0), 10f, Vector2.Normalize(new Vector2(-.5f, (float)Math.Sqrt(.75))), 2*MathHelper.Pi/3);
                owner.GameManager.AddEntity(swing);
                
            }
            if (owner.Dir == Direction.UP)
            {
                swing = new WeaponSwing(this, owner, Origin, new Vector2(0, -owner.Origin.Y / 2), 10f, Vector2.Normalize(new Vector2(-(float)Math.Sqrt(.75), -.5f)), 2 * MathHelper.Pi / 3);
                owner.GameManager.AddEntity(swing);

                
                
            }
            if (owner.Dir == Direction.RIGHT)
            {
                swing = new WeaponSwing(this, owner, Origin, new Vector2(owner.Origin.X / 2, 0), 10f, Vector2.Normalize(new Vector2(.5f, -(float)Math.Sqrt(.75))), 2 * MathHelper.Pi / 3);
                owner.GameManager.AddEntity(swing);
                
            }
            if (owner.Dir == Direction.DOWN)
            {
                swing = new WeaponSwing(this, owner, Origin, new Vector2(0, owner.Origin.Y / 2), 10f, Vector2.Normalize(new Vector2((float)Math.Sqrt(.75), .5f)), 2 * MathHelper.Pi / 3);
                owner.GameManager.AddEntity(swing);
                
            }

            if (swing.Character is Player)
            {
                foreach (NPC n in owner.GameManager.NPCS)
                {
                    if (swing.CollidesWith(n))
                    {
                        if(crit < 1.5) n.TakeDamage(swing.Character, damage, false);
                        else n.TakeDamage(swing.Character, damage, false, Color.Red);
                        if (DropItems.Rand.NextDouble() < BURN_CHANCE)
                        {
                            n.AddEffect(new StatusEffects.Burn("Fire", n, owner, damage / n.Defense.Value/2, 400, 4000));
                        }
                    }
                }
            }
            if (swing.Character is NPC)
            {
                if (swing.CollidesWith(gameManager.Player))
                {
                    if (crit < 1.5) gameManager.Player.TakeDamage(swing.Character, damage, false);
                    else gameManager.Player.TakeDamage(swing.Character, damage, false, Color.Red);
                    if (DropItems.Rand.NextDouble() < BURN_CHANCE)
                    {
                        gameManager.Player.AddEffect(new StatusEffects.Burn("Fire", gameManager.Player, owner, damage / gameManager.Player.Defense.Value / 2, 400, 4000));
                    }
                }
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
