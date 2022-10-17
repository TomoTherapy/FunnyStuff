namespace TimeLapseScreenCapturer
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Interval_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SavePath_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Interval_textBox
            // 
            this.Interval_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Interval_textBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Interval_textBox.Location = new System.Drawing.Point(73, 9);
            this.Interval_textBox.Name = "Interval_textBox";
            this.Interval_textBox.Size = new System.Drawing.Size(132, 25);
            this.Interval_textBox.TabIndex = 0;
            this.Interval_textBox.TextChanged += new System.EventHandler(this.Interval_textBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Save Path";
            // 
            // SavePath_textBox
            // 
            this.SavePath_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePath_textBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.SavePath_textBox.Location = new System.Drawing.Point(12, 63);
            this.SavePath_textBox.Multiline = true;
            this.SavePath_textBox.Name = "SavePath_textBox";
            this.SavePath_textBox.ReadOnly = true;
            this.SavePath_textBox.Size = new System.Drawing.Size(325, 93);
            this.SavePath_textBox.TabIndex = 3;
            this.SavePath_textBox.DoubleClick += new System.EventHandler(this.SavePath_textBox_DoubleClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 168);
            this.Controls.Add(this.SavePath_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Interval_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Interval_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SavePath_textBox;
    }
}

