/************************************************************************/
/* David Erbelding
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using RedemptionEngine.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;
using RedemptionEngine.Items;
using System.Reflection;
using RedemptionEngine.ObjectClasses.Enemy;
using RedemptionEngine.Items.StatusEffects;

namespace RedemptionEngine.ObjectClasses
{
    public class Player: Character
    {
        private Weapon curWep;
        private Keys key;
        private Texture2D caveOverlay;
        private int invincibilityFrames = 0;
        private const int INVINCIBILITY_LENGTH = 30;
        private Item itemToPickup;

        public Player(ContentManager content, GameManager owner)
            : base(content)

        {
            //Set the player's texture
            texture = content.Load<Texture2D>("Characters//player");
            caveOverlay = content.Load<Texture2D>("TileSheets//CircleGradient3");

            //Set owner
            this.gameManager = owner;

            //Add the player animations
            AddAnimation("Idle_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            AddAnimation("Idle_Right",
                new Animation(new Point(0, 32), new Point(32, 32), 2, 200));
            AddAnimation("Idle_Left",
                new Animation(new Point(0, 64), new Point(32, 32), 2, 200));
            AddAnimation("Idle_Up",
                new Animation(new Point(0, 96), new Point(32, 32), 2, 200));
            AddAnimation("Walk_Down",
                new Animation(new Point(64, 0), new Point(32, 32), 4, 100));
            AddAnimation("Walk_Right",
                new Animation(new Point(64, 32), new Point(32, 32), 4, 100));
            AddAnimation("Walk_Left",
                new Animation(new Point(64, 64), new Point(32, 32), 4, 100));
            AddAnimation("Walk_Up",
                new Animation(new Point(64, 96), new Point(32, 32), 4, 100));


            CurrentAnimationKey = "Idle_Up";

            //Bounds offsets
            boundsXOffset = 5;
            boundsYOffset = 4;
            boundsWidthOffset = -10;
            boundsHeightOffset = -4;
            speed = 100f;
        }

        Rectangle oldRect;

        public override void Update(GameTime gameTime)
        {
            itemToPickup = null;
            GameManager.EquipBar.PickupItem = null;

            base.Update(gameTime);


            oldRect = bounds;

            velocity = Vector2.Zero;

            if (Controller.SingleKeyPress(Keys.X)) LevelUp(0);

            if (Controller.KeyIsDown(Keys.S) && !Controller.KeyIsDown(Keys.W) && !Controller.KeyIsDown(Keys.A) && !Controller.KeyIsDown(Keys.D))
            {
                if (CurrentAnimationKey != "Walk_Down")
                    CurrentAnimationKey = "Walk_Down";
            }
            if (Controller.KeyIsDown(Keys.W) && !Controller.KeyIsDown(Keys.S) && !Controller.KeyIsDown(Keys.A) && !Controller.KeyIsDown(Keys.D))
            {
                if (CurrentAnimationKey != "Walk_Up")
                    CurrentAnimationKey = "Walk_Up";
            }
            if (Controller.KeyIsDown(Keys.A) && !Controller.KeyIsDown(Keys.W) && !Controller.KeyIsDown(Keys.S) && !Controller.KeyIsDown(Keys.D))
            {
                if (CurrentAnimationKey != "Walk_Left")
                    CurrentAnimationKey = "Walk_Left";
            }
            if (Controller.KeyIsDown(Keys.D) && !Controller.KeyIsDown(Keys.W) && !Controller.KeyIsDown(Keys.A) && !Controller.KeyIsDown(Keys.S))
            {
                if (CurrentAnimationKey != "Walk_Right")
                    CurrentAnimationKey = "Walk_Right";
            }

            if (Controller.SingleKeyPress(Keys.S))
            {
                if (CurrentAnimationKey != "Walk_Down")
                    CurrentAnimationKey = "Walk_Down";
            }
            if (Controller.SingleKeyPress(Keys.W))
            {
                if (CurrentAnimationKey != "Walk_Up")
                    CurrentAnimationKey = "Walk_Up";
            }
            if (Controller.SingleKeyPress(Keys.A))
            {
                if (CurrentAnimationKey != "Walk_Left")
                    CurrentAnimationKey = "Walk_Left";
            }
            if (Controller.SingleKeyPress(Keys.D))
            {
                if (CurrentAnimationKey != "Walk_Right")
                    CurrentAnimationKey = "Walk_Right";
            }

            if(!Controller.KeyIsDown (Keys.S) && !Controller.KeyIsDown(Keys.W) && !Controller.KeyIsDown(Keys.A) && !Controller.KeyIsDown(Keys.D))
            {
                if (CurrentAnimationKey == "Walk_Down")
                    CurrentAnimationKey = "Idle_Down";
                if (CurrentAnimationKey == "Walk_Up")
                    CurrentAnimationKey = "Idle_Up";
                if (CurrentAnimationKey == "Walk_Left")
                    CurrentAnimationKey = "Idle_Left";
                if (CurrentAnimationKey == "Walk_Right")
                    CurrentAnimationKey = "Idle_Right";
            }

            switch (CurrentAnimationKey)
            {
                case "Walk_Down":
                    velocity.Y = 1;
                    break;
                case "Walk_Up":
                    velocity.Y = -1;
                    break;
                case "Walk_Right":
                    velocity.X = 1;
                    break;
                case "Walk_Left":
                    velocity.X = -1;
                    break;
            }

            if (velocity != Vector2.Zero)
            {
                position += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            speed = 100f;



            gameManager.camera.cameraPos.X = (Center.X - (gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width / 2));
            gameManager.camera.cameraPos.Y = (Center.Y - (gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height / 2));

            if (gameManager.CurrentMap != null)
            {
                if (gameManager.camera.cameraPos.X > gameManager.CurrentMap.MapWidthInPixels - gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width)
                    gameManager.camera.cameraPos.X = gameManager.CurrentMap.MapWidthInPixels - gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width;
                if (gameManager.camera.cameraPos.Y > gameManager.CurrentMap.MapHeightInPixels - gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height)
                    gameManager.camera.cameraPos.Y = gameManager.CurrentMap.MapHeightInPixels - gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height;
                if (gameManager.camera.cameraPos.X < 0)
                    gameManager.camera.cameraPos.X = 0;
                if (gameManager.camera.cameraPos.Y < 0)
                    gameManager.camera.cameraPos.Y = 0;
            }


            if (Controller.SingleKeyPress(Keys.J))
            {
                curWep = (Weapon)inventory.EquipmentLeft.Item;
                key = Keys.J;
            }
            else if (Controller.SingleKeyPress(Keys.K))
            {
                curWep = (Weapon)inventory.EquipmentRight.Item;
                key = Keys.K;
            }

            //if (inventory.EquipmentLeft.Item != null)(inventory.EquipmentLeft.Item as Weapon).Update(gameTime, this, Keys.J);
            //if (inventory.EquipmentRight.Item != null) (inventory.EquipmentRight.Item as Weapon).Update(gameTime, this, Keys.K);

            if (curWep != null) curWep.Update(gameTime, this, key);


            HandleCollision(gameManager.CurrentMap, oldRect);


            foreach (Entity e in gameManager.NPCS)
            {
                if (e is Hostile)
                {
                    if (CollidesWith(e))
                    {
                        bounds = oldRect;
                        position.X = oldRect.X;
                        position.Y = oldRect.Y;
                        Character collidedWith = (e as Character);
                        TakeDamage(collidedWith, collidedWith.Strength.Value, false);
                    }
                }
            }

            PickingUpItems();


            if (invincibilityFrames > 0) invincibilityFrames--;


        }



        private void PickingUpItems()
        {
            for (int i = GameManager.Items.Count - 1; i >= 0; i--)
            {
                if (GameManager.Items[i].CollidesWith(this))
                {
                    itemToPickup = (Item)GameManager.Items[i];
                    GameManager.EquipBar.PickupItem = itemToPickup;
                    if (itemToPickup != null)
                    {
                        break;
                    }
                }
            }

            if (itemToPickup != null)
            {
                if (Controller.SingleKeyPress(Keys.E))
                {
                    SplashText itemText = new SplashText(GameManager, Center, itemToPickup.Name, Color.White, 1.5f);
                    GameManager.AddEntity(itemText);
                    Inventory.AddItem(itemToPickup);
                    GameManager.RemoveEntity(itemToPickup);
                }
            }
        }

        #region LoadCharacter

        // the player class loads in the save file, which will also tell the level which map to load
        public static Player FromFile(ContentManager content, GameManager level, string characterFile)
        {
            //Create initial player object
            Player player = new Player(content, level);
            player.inventory = new Inventory(player);

            //Here we read in the player file
            try
            {
                //Try and load text file into reader
                StreamReader reader = new StreamReader(characterFile);

                bool readingLocation = false;
                bool readingAttributes = false;
                bool readingEquipment = false;
                bool readingInventory = false;
                bool readingQuests = false;

                //While reader hasn't reached end of file
                while (!reader.EndOfStream)
                {
                    //We want to read in all data to create a map object

                    //Get a line from file
                    string line = reader.ReadLine().Trim();

                    //If the line is null or empty we skip reading it
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[LOCATION]")) //We are reading location
                    {
                        readingLocation = true;
                        readingAttributes = false;
                        readingEquipment = false;
                        readingInventory = false;
                        readingQuests = false;
                    }
                    else if (line.Contains("[ATTRIBUTES]")) //We are reading attributes
                    {
                        readingLocation = false;
                        readingAttributes = true;
                        readingEquipment = false;
                        readingInventory = false;
                        readingQuests = false;
                    }
                    else if (line.Contains("[EQUIPMENT]")) //We are reading equipment
                    {
                        readingLocation = false;
                        readingAttributes = false;
                        readingEquipment = true;
                        readingInventory = false;
                        readingQuests = false;
                    }
                    else if (line.Contains("[INVENTORY]")) //We are reading inventory
                    {
                        readingLocation = false;
                        readingAttributes = false;
                        readingEquipment = false;
                        readingInventory = true;
                        readingQuests = false;
                    }
                    else if (line.Contains("[QUESTS]")) //We are reading quests
                    {
                        readingLocation = false;
                        readingAttributes = false;
                        readingEquipment = false;
                        readingInventory = false;
                        readingQuests = true;
                    }
                    else if(readingLocation)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] LocationData = line.Split(' ');
                            //Load the map
                            string map = LocationData[0];
                            level.LoadMap("MapFiles\\" + map + ".txt");

                            float x = float.Parse(LocationData[1]);
                            float y = float.Parse(LocationData[2]);



                            player.position = new Vector2(x, y);
                        }
                    }
                    else if (readingAttributes)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] AttributeData = line.Split(' ');

                            //set all the stats!
                            if (AttributeData[0].Contains("Level")) player.level = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Experience")) player.experience = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Health")) player.baseHealth = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Mana")) player.baseMana = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Stamina")) player.baseStamina = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Defense")) player.baseDefense = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Strength")) player.baseStrength = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            if (AttributeData[0].Contains("Magic")) player.baseMagic = new CharacterAttribute(AttributeData[0], int.Parse(AttributeData[1]), int.Parse(AttributeData[2]));
                            player.InitializeAttributes();
                        }
                    }
                    else if (readingEquipment)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] EquipmentData = line.Split(' ');

                            //Use reflection to find the assembly where the item classes exist
                            Assembly assembly = typeof(Items.Item).Assembly;
                            Type[] types = assembly.GetTypes();
                            Type t = null;

                                //then we build items from the types defined in the text file
                                foreach (Type type in types)
                                {
                                    
                                    if (EquipmentData[0].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentLeft.Item = (obj as Item);
                                        player.inventory.EquipmentLeft.Stack = 1;
                                        //break;
                                    }

                                    if (EquipmentData[1].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentRight.Item = (obj as Item);
                                        player.inventory.EquipmentRight.Stack = 1;
                                        //break;
                                    }

                                    if (EquipmentData[2].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentHead.Item = (obj as Item);
                                        player.inventory.EquipmentHead.Stack = 1;
                                    }
                                    if (EquipmentData[3].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentBody.Item = (obj as Item);
                                        player.inventory.EquipmentBody.Stack = 1;
                                    }
                                    if (EquipmentData[4].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentLegs.Item = (obj as Item);
                                        player.inventory.EquipmentLegs.Stack = 1;
                                    }
                                    if (EquipmentData[5].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        player.inventory.EquipmentFeet.Item = (obj as Item);
                                        player.inventory.EquipmentFeet.Stack = 1;
                                    }
                                }
                            
                            
                        }
                    }
                    else if (readingInventory)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] InventoryData = line.Split(' ');

                            Assembly assembly = typeof(Items.Item).Assembly;
                            Type[] types = assembly.GetTypes();
                            Type t = null;

                            for (int i = 0; i < InventoryData.Length; i++)
                            {
                                foreach (Type type in types)
                                {
                                    if (InventoryData[i].Equals(type.Name))
                                    {
                                        t = type;
                                        ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                        object obj = cInfo.Invoke(new object[] { level.gamePlayScreen.ScreenManager.Content });
                                        (obj as Item).GameManager = player.GameManager;
                                        player.inventory.AddItem(obj);
                                        break;
                                    }
                                }
                            }

                            //Console.WriteLine(obj.ToString());
                            Console.WriteLine(player.inventory.ToString());
                        }
                    }
                    else if (readingQuests)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] QuestData = line.Split(' ');
                        }
                    }
                }
                

                //Close reader
                reader.Close();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return player;
        }

        #endregion

       
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw whatever weapon is being used
            //if ((inventory.EquipmentLeft.Item as Weapon).Active) inventory.EquipmentLeft.Item.Draw(spriteBatch);
            //if ((inventory.EquipmentRight.Item as Weapon).Active) inventory.EquipmentRight.Item.Draw(spriteBatch);
            if (curWep != null)
            {
                if (curWep.Active) curWep.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);

            if (GameManager.CurrentMap.Terrain.TileSheet.Name == "CaveSheet")
            {
                spriteBatch.Draw(caveOverlay, new Vector2(Center.X - (gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width),
                    Center.Y - (gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height)), Color.White);
            }

           

            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }

        public override void Die(Character attacker)
        {
            gameManager.gamePlayScreen.LoadGame(GameManager.GAME);
        }


        public override void TakeDamage(Character attacker, int amount, bool ignoreDefense, Color textColor) //deal damage to character
        {
            int damage; //damage variable
            if (amount < 0) //damage is negative; heal player instead
            {
                HealCharacter(-amount);
                return; //don't do damage twice
            }
            else if (ignoreDefense)
            {
                damage = amount; //damage if ignoring defense (A Lot)
                health.Value -= damage; //apply damage
                GameManager.AddEntity(new SplashText(GameManager, Center, damage.ToString(), textColor, 1.5f));
            }
            else if (invincibilityFrames == 0)
            {
                invincibilityFrames += INVINCIBILITY_LENGTH;
                damage = amount / defense.Value; //use damage calculation
                health.Value -= damage; //apply damage
                GameManager.AddEntity(new SplashText(GameManager, Center, damage.ToString(), textColor, 1.5f));
            }


            if (health.Value <= 0)
            {
                Die(attacker);
            }
            //Console.WriteLine(health.Value);
        }
    }
}
