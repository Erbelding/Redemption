/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Editor
{
    //MapCreator form where user defines map and tile dimensions
    public partial class MapCreator : Form
    {
        private EditorForm owner;

        public MapCreator(EditorForm owner)
        {
            InitializeComponent();
            this.owner = owner;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Close form
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            //Build map with parameters
            //and close

            //We only want to allow users to accept map creation if they
            //actually input something into all text boxes
            if (txtMapWidth.Text.Length > 0 && txtMapHeight.Text.Length > 0
                && txtTileWidth.Text.Length > 0 && txtTileHeight.Text.Length > 0)
            {
                try
                {
                    //Try to parse values of text boxes into new map class
                    int mapWidth = int.Parse(txtMapWidth.Text);
                    int mapHeight = int.Parse(txtMapHeight.Text);
                    int tileWidth = int.Parse(txtTileWidth.Text);
                    int tileHeight = int.Parse(txtTileHeight.Text);

                    owner.MapPanel.Map = new Map(owner.MapPanel, owner.MapPanel.Content, mapWidth, mapHeight, tileWidth, tileHeight);
                    owner.MapPanel.Map.ColTileSheet = new TileSheet(owner.MapPanel.Map, owner.CollisionPalette.Content.Load<Texture2D>("Collision"), "Collision");
                    owner.CollisionPalette.TileSheet = new TileSheet(owner.MapPanel.Map, owner.CollisionPalette.Content.Load<Texture2D>("Collision"), "Collision");

                    this.Close();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    inputErrLabel.Text = "Input was in wrong format, please input integers";
                }
            }

        }
    }
}
