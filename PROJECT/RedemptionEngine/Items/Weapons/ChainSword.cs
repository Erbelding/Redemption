/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;


namespace RedemptionEngine.Items.Weapons.Special
{

    public class ChainSword : Weapon
    {

        private WeaponSwing swing;
        private int coolDown = 0;

        public ChainSword(ContentManager content)
            : base(content)
        {
            POWER = 1;
            name = "Chain Sword";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.MELEE;
            texture = content.Load<Texture2D>("Items//Weapons//ChainSword");

            AddAnimation("Inventory", new Animation(new Point(0, 0), new Point(32, 32), 2, 50));
            AddAnimation("WeaponSwing", new Animation(new Point(0, 0), new Point(32, 32), 2, 50));
            CurrentAnimationKey = "Inventory";

            AddModifier("Defense + 5", "Defense", 5, false);
        }

        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);

            if (Controller.SingleKeyPress(key))
            {
                Attack(owner);
                coolDown = 12;
            }

            if (Controller.KeyIsDown(key) & coolDown > 0)
            {
                coolDown--;
                int damage = 0;
                double crit = 1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble();
                damage = (int)(owner.Strength.Value * POWER * crit);

                if (swing.Character is Player)
                {
                    foreach (NPC n in owner.GameManager.NPCS)
                    {
                        if (swing.CollidesWith(n))
                        {
                            if (swing.CollidesWith(n))
                            {
                                if (crit < 1.5) n.TakeDamage(swing.Character, damage, false);
                                else n.TakeDamage(swing.Character, damage, false, Color.Red);
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
                    }
                }
            }

            

        }

        public override void Attack(Character owner)
        {
            gameManager = owner.GameManager;
            

            swing = null;

            if (owner.Dir == Direction.LEFT)
            {
                swing = new WeaponSwing(this, owner, Origin, new Vector2(-owner.Origin.X / 2, 0), 10f, Vector2.Normalize(new Vector2(-.5f, (float)Math.Sqrt(.75))), 2 * MathHelper.Pi / 3);
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


            
        }
    }
}
