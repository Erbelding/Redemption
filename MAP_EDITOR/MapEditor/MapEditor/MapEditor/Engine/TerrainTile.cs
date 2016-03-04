/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Controls;

namespace MapEditor.Engine
{
    //Represents a terrain tile
    public class TerrainTile : Tile
    {
        //Constructor
        public TerrainTile(int id, Vector2 pos, int tileWidth, int tileHeight)
            : base(id, pos, tileWidth, tileHeight)
        {
        }

        //Event when tile is clicked by user
        protected override void TileClicked(object sender, EventArgs e)
        {
            //Sets the tile's id to that of the tile palette
            tileID = TilePalette.ID;
        }
    }
}
