/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Engine
{
    //Represents a tile sheet object
    public class TileSheet
    {
        //Attributes
        private Texture2D tileSheetTexture; //texture
        private string fileName;
        private int tilesWide;
        private int tilesHigh;

        //Properties

        public Texture2D Texture
        {
            get { return tileSheetTexture; }
            set { tileSheetTexture = value; }
        }

        public int TilesWide
        {
            get { return tilesWide; }
        }

        public int TilesHigh
        {
            get { return tilesHigh; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        //Constructor
        public TileSheet()
        {
            this.tileSheetTexture = null;
            this.tilesWide = 0;
            this.tilesHigh = 0;
        }

        //Constructor
        public TileSheet(Map owner, Texture2D tex, string fileName)
        {
           
            this.tileSheetTexture = tex;
            this.fileName = fileName;
            this.tilesWide = tileSheetTexture.Width / owner.TileWidth;
            this.tilesHigh = tileSheetTexture.Height / owner.TileHeight;
        }
    }
}
