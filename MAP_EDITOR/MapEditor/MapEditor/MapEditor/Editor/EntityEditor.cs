using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Editor
{
    public partial class EntityEditor : Form
    {
        public EntityEditor()
        {
            InitializeComponent();
            entityView.Nodes.Add("Entities");
            entityView.Nodes[0].Nodes.Add("SpawnPoints").Nodes.Add("SpawnPoint_01");
            entityView.Nodes[0].Nodes.Add("Scenery").Nodes.Add("Tree_01");
        }
    }
}
