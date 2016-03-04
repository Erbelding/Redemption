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
    public partial class EntityCreator : Form
    {
        public EntityCreator(List<string> entities)
        {
            InitializeComponent();

            foreach (string s in entities)
                entitiesBox.Items.Add(s);
        }
    }
}
