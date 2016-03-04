/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Controls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MapEditor.Editor;

namespace MapEditor.Engine
{
    //Represents a map made of a terrain and collision layer
    public class Map
    {
        //attributes
        private int tileWidth;          //tile width (in pixels)
        private int tileHeight;         //tile height (in pixels)
        private int mapWidth;           //map width (in tiles)
        private int mapHeight;          //map height (in tiles)
        private int mapWidthInPixels;    //map width (in pixels)
        private int mapHeightInPixels;   //map height (in pixels)
        private GridLayer grid;         //grid layer
        private TileSheet tileSheet;    //tile sheet
        private TileSheet colTileSheet; //collision tile sheet
        private TerrainLayer layer;     //terrain layer
        private CollisionLayer colLayer; //collision layer
        private Texture2D tileSheetTex;  //tileSheet texture
        private MapDisplay owner;       //map display that owns this map

        //properties
        public int TileWidth { get { return tileWidth; } }
        public int TileHeight{ get { return tileHeight; } }
        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }
        public int MapWidthInPixels { get { return mapWidthInPixels; } }
        public int MapHeightInPixels { get { return mapHeightInPixels; } }
        public TileSheet TileSheet { get { return tileSheet; } set { tileSheet = value; } }
        public TileSheet ColTileSheet { get { return colTileSheet; } set { colTileSheet = value; } }
        public Texture2D TileSheetTex { set { tileSheetTex = value; } }
        public MapDisplay Owner { get { return owner; } }
        public TerrainLayer Layer { get { return layer; } }
        public CollisionLayer ColLayer { get { return colLayer; } }

        //constructor
        public Map(MapDisplay owner, ContentManager content, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.owner = owner;
            mapWidthInPixels = mapWidth * tileWidth;
            mapHeightInPixels = mapHeight * tileHeight;
            grid = new GridLayer(this, content);
            layer = new TerrainLayer(this);
            colLayer = new CollisionLayer(this);

            Console.WriteLine(mapWidth);
            Console.WriteLine(mapHeight);
            Console.WriteLine(mapWidthInPixels);
            Console.WriteLine(mapHeightInPixels);
        }

        //Updates the map
        public void Update(Camera camera)
        {
            //Check what layer is currently selected and only
            //update that layer
            if(EditorForm.LayerSelected == EditorForm.SelectedLayer.TERRAIN)
                layer.Update(camera);
            else if(EditorForm.LayerSelected == EditorForm.SelectedLayer.COLLISION)
                colLayer.Update(camera);
        }

        //Draw the map
        public void Draw(Camera camera, SpriteBatch spriteBatch)
        {
            
            grid.Draw(camera, spriteBatch);

            if (tileSheet != null)
            {
                //if(EditorForm.LayerSelected == EditorForm.SelectedLayer.TERRAIN)
                    layer.Draw(camera, spriteBatch);
            }

            if (colTileSheet != null)
            {
                //if(EditorForm.LayerSelected == EditorForm.SelectedLayer.COLLISION)
                if(EditorForm.ShowColLayer)
                    colLayer.Draw(camera, spriteBatch);
            }
            
        }

    }
}
