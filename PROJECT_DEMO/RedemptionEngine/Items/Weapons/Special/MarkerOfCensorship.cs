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
using RedemptionEngine.Items.StatusEffects;


namespace RedemptionEngine.Items.Weapons.Special
{

    public class MarkerOfCensorship : Weapon
    {

        public MarkerOfCensorship(ContentManager content)
            : base(content)
        {
            POWER = 1;
            name = "Marker of Censorship";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.MELEE;
            texture = content.Load<Texture2D>("Items//Weapons//marker of censorship");

            AddAnimation("Inventory", new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            AddAnimation("WeaponSwing", new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            CurrentAnimationKey = "Inventory";
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
            damage = (int)(owner.Strength.Value * POWER * (1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble()));

            WeaponSwing swing = null;

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

            if (swing.Character is Player)
            {
                foreach (NPC n in owner.GameManager.NPCS)
                {
                    if (swing.CollidesWith(n))
                    {
                        n.TakeDamage(swing.Character, damage, false);

                        n.AddEffect(new Censor("Censor",n , owner, 10000));
                    }
                }
            }
            if (swing.Character is NPC)
            {
                if (swing.CollidesWith(gameManager.Player))
                {
                    gameManager.Player.TakeDamage(swing.Character, damage, false);
                    gameManager.Player.AddEffect(new Censor("Censor", gameManager.Player, owner, 10000));
                }
            }

        }
    }
}
