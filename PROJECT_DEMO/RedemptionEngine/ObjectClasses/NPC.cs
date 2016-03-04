using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.TileEngine;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.ObjectClasses
{
    public class NPC : Character
    {
        //Defines npc states
        public enum State
        {
            IDLE,
            MOVING,
            PATROLLING,
            CHASING,
            ATTACKING,
            HIDING
        }

        //Attributes

        protected int MELEE_DAMAGE = 1;
        protected Queue<Vector2> paths;
        protected State state = State.IDLE;
        public Vector2 target;
        public Character pTarget;
        public Path path;
        protected Queue<PathNode> nextNode;
        protected DropPool dropPool;

        public GameManager Owner 
        { 
            set 
            { 
                gameManager = value;
            } 
        }

        

        //Constructor
        public NPC(ContentManager content)
            : base(content)
        {
            state = State.IDLE;
            velocity = Vector2.Zero;
            colTex = content.Load<Texture2D>("Debug//collisionSquare");

            this.level = new CharacterAttribute("Level", 1, 100);
            this.experience = new CharacterAttribute("Experience", 0, 100);
            this.baseHealth = new CharacterAttribute("Health", 100, 100);
            this.baseStamina = new CharacterAttribute("Stamina", 20, 20);
            this.baseMana = new CharacterAttribute("Mana", 20, 20);
            this.baseDefense = new CharacterAttribute("Defense", 0, 100);
            this.baseStrength = new CharacterAttribute("Strength", 0, 100);
            this.baseMagic = new CharacterAttribute("Magic", 0, 100);
            this.inventory = new Inventory(this);

            dropPool = new DropPool(content);
            
        }




        protected virtual void MoveRight()
        {
            velocity.X = 1;
            velocity.Y = 0;
        }

        protected virtual void MoveLeft()
        {
            velocity.X = -1;
            velocity.Y = 0;
        }

        protected virtual void MoveUp()
        {
            velocity.X = 0;
            velocity.Y = -1;
        }

        protected virtual void MoveDown()
        {
            velocity.X = 0;
            velocity.Y = 1;
        }

        protected virtual void Stop()
        {
            velocity = Vector2.Zero;
        }

        protected void RecalculatePath()
        {
            path.ClearLogic();
            nextNode = path.FindPath(this, pTarget, gameManager.CurrentMap);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle oldRect = bounds;

            

            if (velocity != Vector2.Zero && nextNode != null && nextNode.Count != 0)
            {

                int deltaX = (int)MathHelper.Distance(nextNode.Peek().Pos.X, Bounds.X + Bounds.Width/2);
                int deltaY = (int)MathHelper.Distance(nextNode.Peek().Pos.Y, Bounds.Y + Bounds.Height/2);

                int d = (int)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));


                if (velocity.X == 0)
                {
                    if (deltaY > 2) position += velocity * speed * dt;
                    else if (deltaY != 0) position += velocity;
                    else { Stop(); }
                }
                if (velocity.Y == 0)
                {
                    if (deltaX > 2) position += velocity * speed * dt;
                    else if (deltaX != 0) position += velocity;
                    else { Stop(); }
                }
            }
            else if (velocity != Vector2.Zero) position += velocity;

            if (CollidesWith(gameManager.Player))
            {
                if (state == State.ATTACKING)
                {
                    int damage = 0;
                    double crit = 1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble();
                    damage = (int)(Strength.Value * MELEE_DAMAGE * crit);
                    if (crit > 1.5) gameManager.Player.TakeDamage(this, damage, false, Color.Red);
                    else gameManager.Player.TakeDamage(this, damage, false);
                }
                Stop();
                position.X = oldRect.X;
                position.Y = oldRect.Y;
            }

            


            HandleCollision(gameManager.CurrentMap, oldRect);
        }


        private int searchDelay = 40;


        public void MoveTowardsTarget()
        {
            searchDelay++;
            if (searchDelay > 40)
            {
                searchDelay = 20;
                RecalculatePath();
                if (nextNode.Count == 0)
                {
                    searchDelay = 0;
                    Stop();
                    return;
                }
            }
            if (nextNode == null)
            {
                Stop();
                return;
            }
            if (nextNode.Count == 0)
            {
                Stop();
                return;
            }



            int dY = (int)MathHelper.Distance(nextNode.Peek().Pos.Y, Bounds.Y + Bounds.Height/2);
            int dX = (int)MathHelper.Distance(nextNode.Peek().Pos.X, Bounds.X + Bounds.Width/2);
            //Console.WriteLine(dY + "  " + dX);


            if (dY == 0 && dX == 0)
            {
                nextNode.Dequeue();
                Stop();
            }
            if (Velocity != Vector2.Zero && (dY == 0 || dX == 0) && (dY != 0 || dX != 0))
            {
                Stop();
            }
            if (Velocity == Vector2.Zero)
            {
                if (dY > 0)
                {
                    if (Bounds.Y + Bounds.Height / 2 < nextNode.Peek().Pos.Y) MoveDown();
                    else if (Bounds.Y + Bounds.Height / 2 > nextNode.Peek().Pos.Y) MoveUp();
                }
                if (dX > 0)
                {
                    if (Bounds.X + Bounds.Width / 2 < nextNode.Peek().Pos.X) MoveRight();
                    else if (Bounds.X + Bounds.Width / 2 > nextNode.Peek().Pos.X) MoveLeft();
                }
            }
        }


        protected bool correctDirection = false;
        public void LineUpWithTarget()
        {
            nextNode = null;

            int dY = (int)MathHelper.Distance(pTarget.Bounds.Y + pTarget.Bounds.Height / 2, Bounds.Y + Bounds.Height / 2);
            int dX = (int)MathHelper.Distance(pTarget.Bounds.X + pTarget.Bounds.Width / 2, Bounds.X + Bounds.Width / 2);



            if (Velocity != Vector2.Zero && (dY == 0 || dX == 0) && (dY != 0 || dX != 0))
            {
                //already lined up
                if (correctDirection) Stop();
                else
                {
                    correctDirection = true;
                    FaceTarget();
                }
                return;
            }
            else
            {
                correctDirection = false;

                if (dY < dX)
                {
                    if (Bounds.Y + Bounds.Height / 2 < pTarget.Bounds.Y + pTarget.Bounds.Height / 2) MoveDown();
                    else if (Bounds.Y + Bounds.Height / 2 > pTarget.Bounds.Y + pTarget.Bounds.Height / 2) MoveUp();
                }
                else if (dX < dY)
                {
                    if (Bounds.X + Bounds.Width / 2 < pTarget.Bounds.X + pTarget.Bounds.Width / 2) MoveRight();
                    else if (Bounds.X + Bounds.Width / 2 > pTarget.Bounds.X + pTarget.Bounds.Width / 2) MoveLeft();
                }
            }
        }

        public void FaceTarget()
        {
            int dY = (int)MathHelper.Distance(pTarget.Bounds.Y + pTarget.Bounds.Height / 2, Bounds.Y + Bounds.Height / 2);
            int dX = (int)MathHelper.Distance(pTarget.Bounds.X + pTarget.Bounds.Width / 2, Bounds.X + Bounds.Width / 2);

            if (dY > dX)
            {
                if (Bounds.Y + Bounds.Height / 2 < pTarget.Bounds.Y + pTarget.Bounds.Height / 2) MoveDown();
                else if (Bounds.Y + Bounds.Height / 2 > pTarget.Bounds.Y + pTarget.Bounds.Height / 2) MoveUp();
            }
            if (dX > dY)
            {
                if (Bounds.X + Bounds.Width / 2 < pTarget.Bounds.X + pTarget.Bounds.Width / 2) MoveRight();
                else if (Bounds.X + Bounds.Width / 2 > pTarget.Bounds.X + pTarget.Bounds.Width / 2) MoveLeft();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(colTex, Bounds, Color.White);

            if (nextNode != null)
            {
                //if (nextNode.Count != 0) nextNode.Peek().Draw(spriteBatch);
            }
        }

        protected void DropItem(float chance)
        {
            Item drop = dropPool.RandomItem(chance);
            if (drop == null) return;
            drop.Position = this.Position;
            GameManager.AddEntity(drop);
        }
        
    }
}
