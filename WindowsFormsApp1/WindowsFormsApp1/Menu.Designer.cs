namespace WindowsFormsApp1
{
    partial class Menu
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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.changeOrderStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cangeStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listStockToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 65);
            this.button1.TabIndex = 3;
            this.button1.Text = "Change Order Status";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 241);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 66);
            this.button3.TabIndex = 5;
            this.button3.Text = "Search Customer";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 169);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(155, 66);
            this.button7.TabIndex = 9;
            this.button7.Text = "List Stock";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeOrderStatusToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cangeStatusToolStripMenuItem,
            this.listStockToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // changeOrderStatusToolStripMenuItem
            // 
            this.changeOrderStatusToolStripMenuItem.Name = "changeOrderStatusToolStripMenuItem";
            this.changeOrderStatusToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.changeOrderStatusToolStripMenuItem.Text = "Change Order Status";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 313);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 66);
            this.button2.TabIndex = 12;
            this.button2.Text = "Change Stock Volume";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 24);
            this.toolStripMenuItem1.Text = "Search Customer";
            // 
            // cangeStatusToolStripMenuItem
            // 
            this.cangeStatusToolStripMenuItem.Name = "cangeStatusToolStripMenuItem";
            this.cangeStatusToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.cangeStatusToolStripMenuItem.Text = "Cange Status";
            // 
            // listStockToolStripMenuItem1
            // 
            this.listStockToolStripMenuItem1.Name = "listStockToolStripMenuItem1";
            this.listStockToolStripMenuItem1.Size = new System.Drawing.Size(83, 24);
            this.listStockToolStripMenuItem1.Text = "List Stock";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Menu";
            this.Text = "Menu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeOrderStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cangeStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listStockToolStripMenuItem1;
        private System.Windows.Forms.Button button2;
    }
}