using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items.Projectiles;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Items.Weapons.Special
{
    public class ThrowingKnife: Weapon
    {
        public ThrowingKnife(ContentManager content)
            : base(content)
        {
            POWER = 5;
            name = "Throwing Knife";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//Throwing Knife");
            

            AddAnimation("Inventory", new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            CurrentAnimationKey = "Inventory";
        }

        public override void Attack(Character owner)
        {

            Knife knife = null;

            if (owner.Dir == Direction.LEFT)
            {
                knife = new Knife(owner, new Vector2(-1, 0), 0, POWER);
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                knife = new Knife(owner, new Vector2(1, 0), 0, POWER);
            }
            else if (owner.Dir == Direction.DOWN)
            {
                knife = new Knife(owner, new Vector2(0, 1), 0, POWER);
            }
            else knife = new Knife(owner, new Vector2(0, -1), 0, POWER);



            owner.GameManager.AddEntity(knife);
        }



        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);

            if (Controller.OnKeyReleased(key))
            {
                Attack(owner);
            }
        }
    }
}
