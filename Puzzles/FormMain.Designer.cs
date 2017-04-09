namespace Puzzles
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonOpen = new System.Windows.Forms.Button();
            this.panelImages = new System.Windows.Forms.Panel();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.panelHint = new System.Windows.Forms.Panel();
            this.labelHint = new System.Windows.Forms.Label();
            this.buttonSolve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            this.panelHint.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(73, 41);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open Picture";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // panelImages
            // 
            this.panelImages.BackColor = System.Drawing.Color.White;
            this.panelImages.Location = new System.Drawing.Point(12, 77);
            this.panelImages.MinimumSize = new System.Drawing.Size(100, 100);
            this.panelImages.Name = "panelImages";
            this.panelImages.Size = new System.Drawing.Size(500, 500);
            this.panelImages.TabIndex = 1;
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.Location = new System.Drawing.Point(91, 5);
            this.numericUpDownSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownSize.TabIndex = 4;
            this.numericUpDownSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.Checked = true;
            this.checkBoxRotate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRotate.Location = new System.Drawing.Point(91, 31);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(75, 17);
            this.checkBoxRotate.TabIndex = 9;
            this.checkBoxRotate.Text = "with rotate";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            // 
            // panelHint
            // 
            this.panelHint.Controls.Add(this.labelHint);
            this.panelHint.Location = new System.Drawing.Point(371, 10);
            this.panelHint.Name = "panelHint";
            this.panelHint.Size = new System.Drawing.Size(138, 38);
            this.panelHint.TabIndex = 19;
            this.panelHint.MouseEnter += new System.EventHandler(this.panelHint_MouseEnter);
            this.panelHint.MouseLeave += new System.EventHandler(this.panelHint_MouseLeave);
            // 
            // labelHint
            // 
            this.labelHint.AutoSize = true;
            this.labelHint.Location = new System.Drawing.Point(13, 13);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new System.Drawing.Size(115, 13);
            this.labelHint.TabIndex = 0;
            this.labelHint.Text = "Hover for show correct";
            this.labelHint.MouseEnter += new System.EventHandler(this.labelHint_MouseEnter);
            this.labelHint.MouseLeave += new System.EventHandler(this.labelHint_MouseLeave);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(172, 3);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(76, 41);
            this.buttonSolve.TabIndex = 20;
            this.buttonSolve.Text = "AutoSolve";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 589);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.panelHint);
            this.Controls.Add(this.checkBoxRotate);
            this.Controls.Add(this.numericUpDownSize);
            this.Controls.Add(this.panelImages);
            this.Controls.Add(this.buttonOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(539, 627);
            this.MinimumSize = new System.Drawing.Size(539, 627);
            this.Name = "FormMain";
            this.Text = "Puzzle";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            this.panelHint.ResumeLayout(false);
            this.panelHint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Panel panelImages;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.CheckBox checkBoxRotate;
        private System.Windows.Forms.Panel panelHint;
        private System.Windows.Forms.Label labelHint;
        private System.Windows.Forms.Button buttonSolve;
    }
}

