using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using RedemptionEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.ObjectClasses
{
    public class Player: Character
    {
        



        #region LoadCharacter

        // the player class loads in the save file, which will also tell the level which map to load
        public static Player FromFile(Level level, string characterFile)
        {
            Player player = new Player();

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
                        }
                    }
                    else if (readingEquipment)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] EquipmentData = line.Split(' ');
                        }
                    }
                    else if (readingInventory)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get Location Data
                            string[] InventoryData = line.Split(' ');
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


            //level.LoadMap("WhateverMapWeGotFromThePlayerFile");
            return player;
        }

        #endregion

        #region SaveCharacter

        public void SaveCharacter()
        {

        }


        #endregion
    }
}
