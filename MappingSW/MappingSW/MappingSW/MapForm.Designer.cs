namespace MappingSW
{
    partial class Map
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
            this.xText = new System.Windows.Forms.TextBox();
            this.yText = new System.Windows.Forms.TextBox();
            this.updateMap = new System.Windows.Forms.Button();
            this.exBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // xText
            // 
            this.xText.Location = new System.Drawing.Point(35, 415);
            this.xText.Name = "xText";
            this.xText.Size = new System.Drawing.Size(100, 20);
            this.xText.TabIndex = 0;
            // 
            // yText
            // 
            this.yText.Location = new System.Drawing.Point(35, 441);
            this.yText.Name = "yText";
            this.yText.Size = new System.Drawing.Size(100, 20);
            this.yText.TabIndex = 1;
            // 
            // updateMap
            // 
            this.updateMap.Location = new System.Drawing.Point(141, 427);
            this.updateMap.Name = "updateMap";
            this.updateMap.Size = new System.Drawing.Size(75, 23);
            this.updateMap.TabIndex = 3;
            this.updateMap.Text = "Update";
            this.updateMap.UseVisualStyleBackColor = true;
            this.updateMap.Click += new System.EventHandler(this.updateMap_Click);
            // 
            // exBut
            // 
            this.exBut.Location = new System.Drawing.Point(637, 427);
            this.exBut.Name = "exBut";
            this.exBut.Size = new System.Drawing.Size(75, 23);
            this.exBut.TabIndex = 4;
            this.exBut.Text = "Export";
            this.exBut.UseVisualStyleBackColor = true;
            this.exBut.Click += new System.EventHandler(this.exBut_Click);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 471);
            this.Controls.Add(this.exBut);
            this.Controls.Add(this.updateMap);
            this.Controls.Add(this.yText);
            this.Controls.Add(this.xText);
            this.Name = "Map";
            this.Text = "Map Creation Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xText;
        private System.Windows.Forms.TextBox yText;
        private System.Windows.Forms.Button updateMap;
        private System.Windows.Forms.Button exBut;
    }
}

