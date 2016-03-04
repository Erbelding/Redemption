namespace MapEditor.Editor
{
    partial class MapCreator
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
            this.inputErrLabel = new System.Windows.Forms.Label();
            this.groupBoxMap = new System.Windows.Forms.GroupBox();
            this.txtMapHeight = new System.Windows.Forms.TextBox();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.txtMapWidth = new System.Windows.Forms.TextBox();
            this.groupBoxTile = new System.Windows.Forms.GroupBox();
            this.txtTileHeight = new System.Windows.Forms.TextBox();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.txtTileWidth = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.groupBoxMap.SuspendLayout();
            this.groupBoxTile.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputErrLabel
            // 
            this.inputErrLabel.AutoSize = true;
            this.inputErrLabel.Location = new System.Drawing.Point(18, 102);
            this.inputErrLabel.Name = "inputErrLabel";
            this.inputErrLabel.Size = new System.Drawing.Size(0, 13);
            this.inputErrLabel.TabIndex = 19;
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Controls.Add(this.txtMapHeight);
            this.groupBoxMap.Controls.Add(this.lblMapHeight);
            this.groupBoxMap.Controls.Add(this.lblMapWidth);
            this.groupBoxMap.Controls.Add(this.txtMapWidth);
            this.groupBoxMap.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(114, 83);
            this.groupBoxMap.TabIndex = 18;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "Map Size";
            // 
            // txtMapHeight
            // 
            this.txtMapHeight.Location = new System.Drawing.Point(62, 45);
            this.txtMapHeight.Name = "txtMapHeight";
            this.txtMapHeight.Size = new System.Drawing.Size(38, 20);
            this.txtMapHeight.TabIndex = 7;
            this.txtMapHeight.Text = "20";
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapHeight.Location = new System.Drawing.Point(6, 47);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(54, 18);
            this.lblMapHeight.TabIndex = 6;
            this.lblMapHeight.Text = "Height:";
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapWidth.Location = new System.Drawing.Point(6, 19);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(50, 18);
            this.lblMapWidth.TabIndex = 4;
            this.lblMapWidth.Text = "Width:";
            // 
            // txtMapWidth
            // 
            this.txtMapWidth.Location = new System.Drawing.Point(62, 19);
            this.txtMapWidth.Name = "txtMapWidth";
            this.txtMapWidth.Size = new System.Drawing.Size(38, 20);
            this.txtMapWidth.TabIndex = 5;
            this.txtMapWidth.Text = "20";
            // 
            // groupBoxTile
            // 
            this.groupBoxTile.Controls.Add(this.txtTileHeight);
            this.groupBoxTile.Controls.Add(this.lblTileHeight);
            this.groupBoxTile.Controls.Add(this.lblTileWidth);
            this.groupBoxTile.Controls.Add(this.txtTileWidth);
            this.groupBoxTile.Location = new System.Drawing.Point(153, 12);
            this.groupBoxTile.Name = "groupBoxTile";
            this.groupBoxTile.Size = new System.Drawing.Size(114, 83);
            this.groupBoxTile.TabIndex = 17;
            this.groupBoxTile.TabStop = false;
            this.groupBoxTile.Text = "Tile Size";
            // 
            // txtTileHeight
            // 
            this.txtTileHeight.Location = new System.Drawing.Point(62, 45);
            this.txtTileHeight.Name = "txtTileHeight";
            this.txtTileHeight.Size = new System.Drawing.Size(38, 20);
            this.txtTileHeight.TabIndex = 7;
            this.txtTileHeight.Text = "32";
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileHeight.Location = new System.Drawing.Point(6, 47);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(54, 18);
            this.lblTileHeight.TabIndex = 6;
            this.lblTileHeight.Text = "Height:";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileWidth.Location = new System.Drawing.Point(6, 19);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(50, 18);
            this.lblTileWidth.TabIndex = 4;
            this.lblTileWidth.Text = "Width:";
            // 
            // txtTileWidth
            // 
            this.txtTileWidth.Location = new System.Drawing.Point(62, 19);
            this.txtTileWidth.Name = "txtTileWidth";
            this.txtTileWidth.Size = new System.Drawing.Size(38, 20);
            this.txtTileWidth.TabIndex = 5;
            this.txtTileWidth.Text = "32";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(153, 118);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(109, 42);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(12, 118);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(109, 42);
            this.acceptButton.TabIndex = 15;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // MapCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 177);
            this.Controls.Add(this.inputErrLabel);
            this.Controls.Add(this.groupBoxMap);
            this.Controls.Add(this.groupBoxTile);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 215);
            this.MinimumSize = new System.Drawing.Size(300, 215);
            this.Name = "MapCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MapCreator";
            this.groupBoxMap.ResumeLayout(false);
            this.groupBoxMap.PerformLayout();
            this.groupBoxTile.ResumeLayout(false);
            this.groupBoxTile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputErrLabel;
        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.TextBox txtMapHeight;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.Label lblMapWidth;
        private System.Windows.Forms.TextBox txtMapWidth;
        private System.Windows.Forms.GroupBox groupBoxTile;
        private System.Windows.Forms.TextBox txtTileHeight;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.TextBox txtTileWidth;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
    }
}