namespace GameOfLife
{
    partial class MainForm
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
            this.RowsSelector = new System.Windows.Forms.NumericUpDown();
            this.ColumnsSelector = new System.Windows.Forms.NumericUpDown();
            this.RowsLabel = new System.Windows.Forms.Label();
            this.ColumnsLabel = new System.Windows.Forms.Label();
            this.PlanetRadio = new System.Windows.Forms.RadioButton();
            this.SpaceRadio = new System.Windows.Forms.RadioButton();
            this.SmallRadio = new System.Windows.Forms.RadioButton();
            this.MediumRadio = new System.Windows.Forms.RadioButton();
            this.LargeRadio = new System.Windows.Forms.RadioButton();
            this.StyleGroup = new System.Windows.Forms.GroupBox();
            this.SizeGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.InfoButton = new System.Windows.Forms.Button();
            this.StepButton = new System.Windows.Forms.Button();
            this.AutoStepButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RowsSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsSelector)).BeginInit();
            this.StyleGroup.SuspendLayout();
            this.SizeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // RowsSelector
            // 
            this.RowsSelector.Location = new System.Drawing.Point(154, 19);
            this.RowsSelector.Name = "RowsSelector";
            this.RowsSelector.Size = new System.Drawing.Size(48, 20);
            this.RowsSelector.TabIndex = 3;
            this.RowsSelector.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // ColumnsSelector
            // 
            this.ColumnsSelector.Location = new System.Drawing.Point(154, 56);
            this.ColumnsSelector.Name = "ColumnsSelector";
            this.ColumnsSelector.Size = new System.Drawing.Size(48, 20);
            this.ColumnsSelector.TabIndex = 4;
            this.ColumnsSelector.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // RowsLabel
            // 
            this.RowsLabel.AutoSize = true;
            this.RowsLabel.Location = new System.Drawing.Point(114, 21);
            this.RowsLabel.Name = "RowsLabel";
            this.RowsLabel.Size = new System.Drawing.Size(34, 13);
            this.RowsLabel.TabIndex = 5;
            this.RowsLabel.Text = "Rows";
            this.RowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColumnsLabel
            // 
            this.ColumnsLabel.AutoSize = true;
            this.ColumnsLabel.Location = new System.Drawing.Point(101, 58);
            this.ColumnsLabel.Name = "ColumnsLabel";
            this.ColumnsLabel.Size = new System.Drawing.Size(47, 13);
            this.ColumnsLabel.TabIndex = 6;
            this.ColumnsLabel.Text = "Columns";
            this.ColumnsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlanetRadio
            // 
            this.PlanetRadio.AutoSize = true;
            this.PlanetRadio.Checked = true;
            this.PlanetRadio.Location = new System.Drawing.Point(6, 19);
            this.PlanetRadio.Name = "PlanetRadio";
            this.PlanetRadio.Size = new System.Drawing.Size(55, 17);
            this.PlanetRadio.TabIndex = 10;
            this.PlanetRadio.TabStop = true;
            this.PlanetRadio.Text = "Planet";
            this.PlanetRadio.UseVisualStyleBackColor = true;
            // 
            // SpaceRadio
            // 
            this.SpaceRadio.AutoSize = true;
            this.SpaceRadio.Location = new System.Drawing.Point(6, 42);
            this.SpaceRadio.Name = "SpaceRadio";
            this.SpaceRadio.Size = new System.Drawing.Size(56, 17);
            this.SpaceRadio.TabIndex = 11;
            this.SpaceRadio.Text = "Space";
            this.SpaceRadio.UseVisualStyleBackColor = true;
            // 
            // SmallRadio
            // 
            this.SmallRadio.AutoSize = true;
            this.SmallRadio.Location = new System.Drawing.Point(6, 19);
            this.SmallRadio.Name = "SmallRadio";
            this.SmallRadio.Size = new System.Drawing.Size(50, 17);
            this.SmallRadio.TabIndex = 12;
            this.SmallRadio.Text = "Small";
            this.SmallRadio.UseVisualStyleBackColor = true;
            // 
            // MediumRadio
            // 
            this.MediumRadio.AutoSize = true;
            this.MediumRadio.Checked = true;
            this.MediumRadio.Location = new System.Drawing.Point(6, 42);
            this.MediumRadio.Name = "MediumRadio";
            this.MediumRadio.Size = new System.Drawing.Size(62, 17);
            this.MediumRadio.TabIndex = 13;
            this.MediumRadio.TabStop = true;
            this.MediumRadio.Text = "Medium";
            this.MediumRadio.UseVisualStyleBackColor = true;
            // 
            // LargeRadio
            // 
            this.LargeRadio.AutoSize = true;
            this.LargeRadio.Location = new System.Drawing.Point(6, 65);
            this.LargeRadio.Name = "LargeRadio";
            this.LargeRadio.Size = new System.Drawing.Size(52, 17);
            this.LargeRadio.TabIndex = 14;
            this.LargeRadio.Text = "Large";
            this.LargeRadio.UseVisualStyleBackColor = true;
            // 
            // StyleGroup
            // 
            this.StyleGroup.Controls.Add(this.PlanetRadio);
            this.StyleGroup.Controls.Add(this.SpaceRadio);
            this.StyleGroup.Enabled = false;
            this.StyleGroup.Location = new System.Drawing.Point(345, 488);
            this.StyleGroup.Name = "StyleGroup";
            this.StyleGroup.Size = new System.Drawing.Size(129, 89);
            this.StyleGroup.TabIndex = 15;
            this.StyleGroup.TabStop = false;
            this.StyleGroup.Text = "Style";
            // 
            // SizeGroup
            // 
            this.SizeGroup.Controls.Add(this.SmallRadio);
            this.SizeGroup.Controls.Add(this.MediumRadio);
            this.SizeGroup.Controls.Add(this.LargeRadio);
            this.SizeGroup.Controls.Add(this.ColumnsLabel);
            this.SizeGroup.Controls.Add(this.RowsSelector);
            this.SizeGroup.Controls.Add(this.ColumnsSelector);
            this.SizeGroup.Controls.Add(this.RowsLabel);
            this.SizeGroup.Location = new System.Drawing.Point(12, 485);
            this.SizeGroup.Name = "SizeGroup";
            this.SizeGroup.Size = new System.Drawing.Size(327, 92);
            this.SizeGroup.TabIndex = 16;
            this.SizeGroup.TabStop = false;
            this.SizeGroup.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 26);
            this.label1.TabIndex = 19;
            this.label1.Text = "Generation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(180, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 26);
            this.label2.TabIndex = 20;
            this.label2.Text = "12";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GameOfLife.Properties.Resources.generation;
            this.pictureBox2.Location = new System.Drawing.Point(9, 9);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(9, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(899, 419);
            this.panel1.TabIndex = 17;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox4, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox5, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox7, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox8, 6, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(319, 96);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(267, 232);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ResetButton
            // 
            this.ResetButton.Image = global::GameOfLife.Properties.Resources.reset;
            this.ResetButton.Location = new System.Drawing.Point(691, 497);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 80);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset";
            this.ResetButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ResetButton.UseVisualStyleBackColor = true;
            // 
            // InfoButton
            // 
            this.InfoButton.Image = global::GameOfLife.Properties.Resources.help;
            this.InfoButton.Location = new System.Drawing.Point(772, 497);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(75, 80);
            this.InfoButton.TabIndex = 2;
            this.InfoButton.Text = "Help";
            this.InfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.InfoButton.UseVisualStyleBackColor = true;
            // 
            // StepButton
            // 
            this.StepButton.Image = global::GameOfLife.Properties.Resources.step;
            this.StepButton.Location = new System.Drawing.Point(599, 497);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(80, 80);
            this.StepButton.TabIndex = 1;
            this.StepButton.Text = "Step";
            this.StepButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StepButton.UseVisualStyleBackColor = true;
            // 
            // AutoStepButton
            // 
            this.AutoStepButton.Image = global::GameOfLife.Properties.Resources.autostep;
            this.AutoStepButton.Location = new System.Drawing.Point(518, 497);
            this.AutoStepButton.Name = "AutoStepButton";
            this.AutoStepButton.Size = new System.Drawing.Size(75, 80);
            this.AutoStepButton.TabIndex = 0;
            this.AutoStepButton.Text = "Autostep";
            this.AutoStepButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AutoStepButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox3.Location = new System.Drawing.Point(133, 67);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox4.Location = new System.Drawing.Point(133, 100);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox5.Location = new System.Drawing.Point(166, 67);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox6.Location = new System.Drawing.Point(34, 166);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(32, 32);
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox7.Location = new System.Drawing.Point(67, 166);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(32, 32);
            this.pictureBox7.TabIndex = 5;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox8.Location = new System.Drawing.Point(199, 1);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(32, 32);
            this.pictureBox8.TabIndex = 6;
            this.pictureBox8.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 589);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SizeGroup);
            this.Controls.Add(this.StyleGroup);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.AutoStepButton);
            this.Name = "MainForm";
            this.Text = "Game of Life";
            ((System.ComponentModel.ISupportInitialize)(this.RowsSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsSelector)).EndInit();
            this.StyleGroup.ResumeLayout(false);
            this.StyleGroup.PerformLayout();
            this.SizeGroup.ResumeLayout(false);
            this.SizeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AutoStepButton;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Button InfoButton;
        private System.Windows.Forms.NumericUpDown RowsSelector;
        private System.Windows.Forms.NumericUpDown ColumnsSelector;
        private System.Windows.Forms.Label RowsLabel;
        private System.Windows.Forms.Label ColumnsLabel;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.RadioButton PlanetRadio;
        private System.Windows.Forms.RadioButton SpaceRadio;
        private System.Windows.Forms.RadioButton SmallRadio;
        private System.Windows.Forms.RadioButton MediumRadio;
        private System.Windows.Forms.RadioButton LargeRadio;
        private System.Windows.Forms.GroupBox StyleGroup;
        private System.Windows.Forms.GroupBox SizeGroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;

        public void CreateTable()
        {

        }

        public void DestroyTable()
        {

        }

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;

        
    }
}

