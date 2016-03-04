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
using Microsoft.Xna.Framework.Input;
using MapEditor.Engine;
using MapEditor.Controls;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Editor
{
    //EditorForm that displays the map panel, tile palette, and collision palette
    public partial class EditorForm : Form
    {
        public MapDisplay MapPanel { get { return mapDisplay1; } }
        public TilePalette TilePalette { get { return tilePalette1; } }
        public CollisionPalette CollisionPalette { get { return collisionPalette1; } }
        public NumericUpDown CamNumerator { get { return numericUpDown1; } }
        public ContextMenuStrip RightClickMenu { get { return contextMenuStrip1; } }

        private List<string> entities;

        //Determines if collision layer should be rendered
        public static bool ShowColLayer;

        //Selected layer enum
        public enum SelectedLayer
        {
            TERRAIN,
            COLLISION
        }

        //Current selected layer
        public static SelectedLayer LayerSelected;

        //Determines if this form has mouse focus
        public static bool HasFocus;
        
        //Constructor
        public EditorForm()
        {
            InitializeComponent();

            //Set tilePalette owner
            tilePalette1.Owner = this;
            //Set mapDisplay owner
            mapDisplay1.Owner = this;
            //Set collisionPalette owner
            collisionPalette1.Owner = this;

            ToolStripMenuItem addEntity = new ToolStripMenuItem("Add Entity");
            addEntity.Click += AddEntity;

            ToolStripMenuItem closeMenu = new ToolStripMenuItem("Cancel");
            closeMenu.Click += CloseMenu;

            contextMenuStrip1.Items.Add(addEntity);
            contextMenuStrip1.Items.Add(closeMenu);

            //Mouse.WindowHandle = mapDisplayPanel.Handle;

            //Create the entity list file if it doesn't already exist
            if (!File.Exists("Entities.txt"))
            {
                StreamWriter writer = new StreamWriter("Entities.txt");
            }
            //Otherwise, populate a List of string with the data from that text file
            else
            {
                StreamReader reader = new StreamReader("Entities.txt");

                entities = new List<string>();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(line))
                        entities.Add(line);
                }
                
            }
            
            
        }

        private void AddEntity(object sender, EventArgs e)
        {
            //Show entity creator
            EntityCreator entityCreator = new EntityCreator(entities);
            entityCreator.ShowDialog();
        }

        private void CloseMenu(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }

        private void mapDisplay1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void mapDisplay1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosLabel.Text = "MouseX: " + (Mouse.GetState().X + mapDisplay1.Cam.Position.X) + " MouseY: " + (Mouse.GetState().Y + mapDisplay1.Cam.Position.Y);
            int tileX = (int)(Mouse.GetState().X + mapDisplay1.Cam.Position.X) / 32;
            int tileY = (int)(Mouse.GetState().Y + mapDisplay1.Cam.Position.Y) / 32;
            mousePosLabel.Text += " TileX: " + tileX + " TileY: " + tileY;
        }

        private void addLayerBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TilePaletteForm tilePalette = new TilePaletteForm();
            //tilePalette.Show();

            MapCreator mapCreator = new MapCreator(this);
            mapCreator.ShowDialog();
        }

        private void mapDisplay1_MouseEnter(object sender, EventArgs e)
        {
            mapDisplay1.MouseFocus = true;
            Mouse.WindowHandle = mapDisplay1.Handle;
        }

        private void mapDisplay1_MouseLeave(object sender, EventArgs e)
        {
            mapDisplay1.MouseFocus = false;
        }

        private void tilePalette1_MouseEnter(object sender, EventArgs e)
        {
            tilePalette1.MouseFocus = true;
            Mouse.WindowHandle = tilePalette1.Handle;
        }

        private void tilePalette1_MouseLeave(object sender, EventArgs e)
        {
            tilePalette1.MouseFocus = false;
        }

        private void tilePalette1_MouseMove(object sender, MouseEventArgs e)
        {
            tilePaletteStatus.Text = "MouseX: " + Mouse.GetState().X +
                                   "MouseY: " + Mouse.GetState().Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openTex = new OpenFileDialog();

            DialogResult result = openTex.ShowDialog();

            if (result == DialogResult.OK)
            {
                //Cache name of file selected WITHOUT THE EXTENSION
                //the reason for this is that XNA Content Manager loads
                //content without extensions given
                string fileToLoad = Path.GetFileNameWithoutExtension(openTex.FileName);
                string file = openTex.FileName;
                selectedFileName.Text = fileToLoad;

                Bitmap bmp = new Bitmap(file);
                Texture2D tex = null;

                using (MemoryStream s = new MemoryStream())
                {
                    bmp.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                    s.Seek(0, SeekOrigin.Begin);
                    tex = Texture2D.FromStream(tilePalette1.GraphicsDevice, s);
                }

                bmp.Save("TileSets\\" + fileToLoad + ".png");

                //NOTE: TEXTURE MUST BE INSIDE THE CONTENT PROJECT
                //Texture2D tex = tilePalette1.Content.Load<Texture2D>(fileToLoad);

                if (mapDisplay1.Map != null)
                {
                    tilePalette1.TileSheet = new TileSheet(mapDisplay1.Map, tex, fileToLoad);
                    mapDisplay1.Map.TileSheet = tilePalette1.TileSheet;
                }
                else
                    errorStatusStip.Text = "Please create a map before adding a tileset";
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IF A MAP CURRENTLY EXISTS
            if (mapDisplay1.Map != null)
            {
                Stream fileStream;

                //Must save out the map to a text file
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Do something
                    if ((fileStream = saveFileDialog.OpenFile()) != null)
                    {
                        //Code write to stream
                        //.....
                        MapWriter writer = new MapWriter(fileStream, mapDisplay1.Map);

                        writer.Write();

                        fileStream.Close();
                    }
                }
            }
            else
            {
                errorStatusStip.Text = "Please create a map before trying to save!";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            //Show the dialog and check result
            //If the user presses ok
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Cache the file they chose
                string fileName = openFileDialog.FileName;
                Console.WriteLine(fileName);

                string filePath = fileName;

                //Create a map reader
                MapReader mapReader = new MapReader(this, filePath);

                mapDisplay1.Map = mapReader.Read();
                mapDisplay1.Map.ColTileSheet = new TileSheet(mapDisplay1.Map, collisionPalette1.Content.Load<Texture2D>("Collision"), "Collision");
                collisionPalette1.TileSheet = new TileSheet(mapDisplay1.Map, collisionPalette1.Content.Load<Texture2D>("Collision"), "Collision");

            }

        }

        private void terrainRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            LayerSelected = SelectedLayer.TERRAIN;
            selectedLayerStatus.Text = LayerSelected.ToString();
        }

        private void collisionRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            LayerSelected = SelectedLayer.COLLISION;
            selectedLayerStatus.Text = LayerSelected.ToString();
        }

        private void collisionPalette1_MouseEnter(object sender, EventArgs e)
        {
            collisionPalette1.MouseFocus = true;
            Mouse.WindowHandle = collisionPalette1.Handle;
        }

        private void collisionPalette1_MouseLeave(object sender, EventArgs e)
        {
            collisionPalette1.MouseFocus = false;
        }

        private void collisionPalette1_MouseMove(object sender, MouseEventArgs e)
        {
            collisionPaletteStatus.Text = "MouseX: " + Mouse.GetState().X +
                                        "MouseY: " + Mouse.GetState().Y;
        }

        private void collisionPalette1_MouseClick(object sender, MouseEventArgs e)
        {
            collisionRadioBtn.Checked = true;
            checkBox1.Checked = true;
        }

        private void tilePalette1_Click(object sender, EventArgs e)
        {
            terrainRadioBtn.Checked = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
                ShowColLayer = false;
            else
                ShowColLayer = true;
        }

        private void addEntityBtn_Click(object sender, EventArgs e)
        {
            EntityEditor editor = new EntityEditor();

            editor.Show();
        }

        private void allowFillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allowFillCheckBox.Checked == true)
            {
                if (mapDisplay1.Map != null)
                {
                    fillButton.Enabled = true;
                }
            }
            else
                fillButton.Enabled = false;
                

        }

        private void fillButton_Click(object sender, EventArgs e)
        {
            foreach (TerrainTile tile in mapDisplay1.Map.Layer.Tiles)
            {
                tile.ID = TilePalette.ID;
            }
        }


        private void numericUpDown1_Validated(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mapDisplay1.Cam.Speed = (int)numericUpDown1.Value;
        }


    }
}
