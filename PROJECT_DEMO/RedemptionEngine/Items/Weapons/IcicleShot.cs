using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Items.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RedemptionEngine.Screens;

namespace RedemptionEngine.Items.Weapons
{
    public class IcicleShot : Weapon
    {
        

        public IcicleShot(ContentManager content)
            : base(content)
        {
            POWER = 7;
            name = "Icicle Shot";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//iciclespellbook");

            AddModifier("Magic + 15", "Magic", 15, false);
        }

        public override void Attack(Character owner)
        {
            IceShard iceShard = null;
            int power = POWER;

            owner.Mana.Value -= 4;

            if (owner.Dir == Direction.LEFT)
            {
                iceShard = new IceShard(owner, new Vector2(-1, 0), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(-1, -.1f)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(-1, .1f)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                iceShard = new IceShard(owner, new Vector2(1, 0), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(1, -.1f)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(1, .1f)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
            }
            else if (owner.Dir == Direction.DOWN)
            {
                iceShard = new IceShard(owner, new Vector2(0, 1), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(-.1f, 1)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(.1f, 1)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
            }
            else
            {
                iceShard = new IceShard(owner, new Vector2(0, -1), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(-.1f, -1)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
                iceShard = new IceShard(owner, Vector2.Normalize(new Vector2(.1f, -1)), owner.Magic.Value, power);
                owner.GameManager.AddEntity(iceShard);
            }
            
        }


        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            if (Controller.KeyIsDown(key))
            {
                active = true;
                visible = false;
            }

            if (Controller.OnKeyReleased(key))
            {
                if (owner.Mana.Value > 0)
                {
                    Attack(owner);
                    active = false;
                    visible = true;
                    rotation = 0;
                }
            }
        }
    }
}
