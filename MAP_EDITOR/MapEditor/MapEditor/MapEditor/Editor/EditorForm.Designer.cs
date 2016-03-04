namespace MapEditor.Editor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapDisplayPanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mousePosLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tilePaletteStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.collisionPaletteStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectedFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorStatusStip = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectedLayerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.layerGrpBox = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.objLayerRdBtn = new System.Windows.Forms.RadioButton();
            this.collisionRadioBtn = new System.Windows.Forms.RadioButton();
            this.terrainRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.fillButton = new System.Windows.Forms.Button();
            this.allowFillCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.collisionPalette1 = new MapEditor.Controls.CollisionPalette();
            this.tilePalette1 = new MapEditor.Controls.TilePalette();
            this.mapDisplay1 = new MapEditor.Controls.MapDisplay();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.mapDisplayPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.layerGrpBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1338, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freeToolStripMenuItem,
            this.lockedToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.cameraToolStripMenuItem.Text = "Camera";
            // 
            // freeToolStripMenuItem
            // 
            this.freeToolStripMenuItem.Name = "freeToolStripMenuItem";
            this.freeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.freeToolStripMenuItem.Text = "Free";
            // 
            // lockedToolStripMenuItem
            // 
            this.lockedToolStripMenuItem.Name = "lockedToolStripMenuItem";
            this.lockedToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.lockedToolStripMenuItem.Text = "Locked";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            // 
            // mapDisplayPanel
            // 
            this.mapDisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapDisplayPanel.Controls.Add(this.mapDisplay1);
            this.mapDisplayPanel.Location = new System.Drawing.Point(274, 29);
            this.mapDisplayPanel.MaximumSize = new System.Drawing.Size(750, 600);
            this.mapDisplayPanel.MinimumSize = new System.Drawing.Size(750, 600);
            this.mapDisplayPanel.Name = "mapDisplayPanel";
            this.mapDisplayPanel.Size = new System.Drawing.Size(750, 600);
            this.mapDisplayPanel.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mousePosLabel,
            this.tilePaletteStatus,
            this.collisionPaletteStatus,
            this.selectedFileName,
            this.errorStatusStip,
            this.selectedLayerStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1338, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mousePosLabel
            // 
            this.mousePosLabel.Name = "mousePosLabel";
            this.mousePosLabel.Size = new System.Drawing.Size(118, 17);
            this.mousePosLabel.Text = "toolStripStatusLabel1";
            // 
            // tilePaletteStatus
            // 
            this.tilePaletteStatus.Name = "tilePaletteStatus";
            this.tilePaletteStatus.Size = new System.Drawing.Size(118, 17);
            this.tilePaletteStatus.Text = "toolStripStatusLabel1";
            // 
            // collisionPaletteStatus
            // 
            this.collisionPaletteStatus.Name = "collisionPaletteStatus";
            this.collisionPaletteStatus.Size = new System.Drawing.Size(118, 17);
            this.collisionPaletteStatus.Text = "toolStripStatusLabel1";
            // 
            // selectedFileName
            // 
            this.selectedFileName.Name = "selectedFileName";
            this.selectedFileName.Size = new System.Drawing.Size(118, 17);
            this.selectedFileName.Text = "toolStripStatusLabel1";
            // 
            // errorStatusStip
            // 
            this.errorStatusStip.Name = "errorStatusStip";
            this.errorStatusStip.Size = new System.Drawing.Size(118, 17);
            this.errorStatusStip.Text = "toolStripStatusLabel1";
            // 
            // selectedLayerStatus
            // 
            this.selectedLayerStatus.Name = "selectedLayerStatus";
            this.selectedLayerStatus.Size = new System.Drawing.Size(118, 17);
            this.selectedLayerStatus.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tilePalette1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(1030, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 600);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TilePalette";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 571);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(269, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add TileSet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // layerGrpBox
            // 
            this.layerGrpBox.Controls.Add(this.groupBox4);
            this.layerGrpBox.Location = new System.Drawing.Point(12, 196);
            this.layerGrpBox.Name = "layerGrpBox";
            this.layerGrpBox.Size = new System.Drawing.Size(256, 152);
            this.layerGrpBox.TabIndex = 7;
            this.layerGrpBox.TabStop = false;
            this.layerGrpBox.Text = "Layers";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.objLayerRdBtn);
            this.groupBox4.Controls.Add(this.collisionRadioBtn);
            this.groupBox4.Controls.Add(this.terrainRadioBtn);
            this.groupBox4.Location = new System.Drawing.Point(7, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(243, 119);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SelectedLayer";
            // 
            // objLayerRdBtn
            // 
            this.objLayerRdBtn.AutoSize = true;
            this.objLayerRdBtn.Location = new System.Drawing.Point(18, 86);
            this.objLayerRdBtn.Name = "objLayerRdBtn";
            this.objLayerRdBtn.Size = new System.Drawing.Size(56, 17);
            this.objLayerRdBtn.TabIndex = 2;
            this.objLayerRdBtn.Text = "Object";
            this.objLayerRdBtn.UseVisualStyleBackColor = true;
            // 
            // collisionRadioBtn
            // 
            this.collisionRadioBtn.AutoSize = true;
            this.collisionRadioBtn.Location = new System.Drawing.Point(18, 53);
            this.collisionRadioBtn.Name = "collisionRadioBtn";
            this.collisionRadioBtn.Size = new System.Drawing.Size(63, 17);
            this.collisionRadioBtn.TabIndex = 1;
            this.collisionRadioBtn.Text = "Collision";
            this.collisionRadioBtn.UseVisualStyleBackColor = true;
            this.collisionRadioBtn.CheckedChanged += new System.EventHandler(this.collisionRadioBtn_CheckedChanged);
            // 
            // terrainRadioBtn
            // 
            this.terrainRadioBtn.AutoSize = true;
            this.terrainRadioBtn.Checked = true;
            this.terrainRadioBtn.Location = new System.Drawing.Point(18, 19);
            this.terrainRadioBtn.Name = "terrainRadioBtn";
            this.terrainRadioBtn.Size = new System.Drawing.Size(58, 17);
            this.terrainRadioBtn.TabIndex = 0;
            this.terrainRadioBtn.TabStop = true;
            this.terrainRadioBtn.Text = "Terrain";
            this.terrainRadioBtn.UseVisualStyleBackColor = true;
            this.terrainRadioBtn.CheckedChanged += new System.EventHandler(this.terrainRadioBtn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.collisionPalette1);
            this.groupBox2.Location = new System.Drawing.Point(12, 401);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 228);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CollisionPalette";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Location = new System.Drawing.Point(19, 354);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 41);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CollisionType";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(7, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(230, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 173);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "ShowCollisionLayer";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.fillButton);
            this.groupBox5.Controls.Add(this.allowFillCheckBox);
            this.groupBox5.Location = new System.Drawing.Point(12, 58);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 47);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fill";
            // 
            // fillButton
            // 
            this.fillButton.Enabled = false;
            this.fillButton.Location = new System.Drawing.Point(80, 14);
            this.fillButton.Name = "fillButton";
            this.fillButton.Size = new System.Drawing.Size(144, 23);
            this.fillButton.TabIndex = 1;
            this.fillButton.Text = "Fill";
            this.fillButton.UseVisualStyleBackColor = true;
            this.fillButton.Click += new System.EventHandler(this.fillButton_Click);
            // 
            // allowFillCheckBox
            // 
            this.allowFillCheckBox.AutoSize = true;
            this.allowFillCheckBox.Location = new System.Drawing.Point(7, 20);
            this.allowFillCheckBox.Name = "allowFillCheckBox";
            this.allowFillCheckBox.Size = new System.Drawing.Size(66, 17);
            this.allowFillCheckBox.TabIndex = 0;
            this.allowFillCheckBox.Text = "Allow Fill";
            this.allowFillCheckBox.UseVisualStyleBackColor = true;
            this.allowFillCheckBox.CheckedChanged += new System.EventHandler(this.allowFillCheckBox_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numericUpDown1);
            this.groupBox6.Location = new System.Drawing.Point(13, 112);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(243, 55);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Camera Speed";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(86, 19);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown1.Validated += new System.EventHandler(this.numericUpDown1_Validated);
            // 
            // collisionPalette1
            // 
            this.collisionPalette1.Location = new System.Drawing.Point(7, 20);
            this.collisionPalette1.Name = "collisionPalette1";
            this.collisionPalette1.Size = new System.Drawing.Size(243, 202);
            this.collisionPalette1.TabIndex = 0;
            this.collisionPalette1.Text = "collisionPalette1";
            this.collisionPalette1.TileSheet = null;
            this.collisionPalette1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.collisionPalette1_MouseClick);
            this.collisionPalette1.MouseEnter += new System.EventHandler(this.collisionPalette1_MouseEnter);
            this.collisionPalette1.MouseLeave += new System.EventHandler(this.collisionPalette1_MouseLeave);
            this.collisionPalette1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.collisionPalette1_MouseMove);
            // 
            // tilePalette1
            // 
            this.tilePalette1.Location = new System.Drawing.Point(7, 20);
            this.tilePalette1.Name = "tilePalette1";
            this.tilePalette1.Size = new System.Drawing.Size(275, 545);
            this.tilePalette1.TabIndex = 6;
            this.tilePalette1.Text = "tilePalette1";
            this.tilePalette1.TileSheet = null;
            this.tilePalette1.Click += new System.EventHandler(this.tilePalette1_Click);
            this.tilePalette1.MouseEnter += new System.EventHandler(this.tilePalette1_MouseEnter);
            this.tilePalette1.MouseLeave += new System.EventHandler(this.tilePalette1_MouseLeave);
            this.tilePalette1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tilePalette1_MouseMove);
            // 
            // mapDisplay1
            // 
            this.mapDisplay1.Location = new System.Drawing.Point(-2, -2);
            this.mapDisplay1.Map = null;
            this.mapDisplay1.MaximumSize = new System.Drawing.Size(750, 600);
            this.mapDisplay1.MinimumSize = new System.Drawing.Size(750, 600);
            this.mapDisplay1.MouseFocus = false;
            this.mapDisplay1.Name = "mapDisplay1";
            this.mapDisplay1.Owner = null;
            this.mapDisplay1.Size = new System.Drawing.Size(750, 600);
            this.mapDisplay1.TabIndex = 0;
            this.mapDisplay1.Text = "mapDisplay1";
            this.mapDisplay1.MouseEnter += new System.EventHandler(this.mapDisplay1_MouseEnter);
            this.mapDisplay1.MouseLeave += new System.EventHandler(this.mapDisplay1_MouseLeave);
            this.mapDisplay1.MouseHover += new System.EventHandler(this.mapDisplay1_MouseHover);
            this.mapDisplay1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapDisplay1_MouseMove);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 682);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.layerGrpBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mapDisplayPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "EditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditorForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mapDisplayPanel.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.layerGrpBox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Panel mapDisplayPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel mousePosLabel;
        private Controls.MapDisplay mapDisplay1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.TilePalette tilePalette1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel tilePaletteStatus;
        private System.Windows.Forms.ToolStripStatusLabel selectedFileName;
        private System.Windows.Forms.ToolStripStatusLabel errorStatusStip;
        private System.Windows.Forms.GroupBox layerGrpBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private Controls.CollisionPalette collisionPalette1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton collisionRadioBtn;
        private System.Windows.Forms.RadioButton terrainRadioBtn;
        private System.Windows.Forms.ToolStripStatusLabel selectedLayerStatus;
        private System.Windows.Forms.ToolStripStatusLabel collisionPaletteStatus;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton objLayerRdBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button fillButton;
        private System.Windows.Forms.CheckBox allowFillCheckBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    }
}