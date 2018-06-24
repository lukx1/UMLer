using UMLer.Controls;

namespace UMLer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.ToolsPanel = new System.Windows.Forms.Panel();
            this.ElementPanel = new UMLer.Controls.ElementPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.ToolsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PropertiesPanel);
            this.panel1.Controls.Add(this.ToolsPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 761);
            this.panel1.TabIndex = 0;
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesPanel.Location = new System.Drawing.Point(0, 100);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(200, 661);
            this.PropertiesPanel.TabIndex = 1;
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ToolsPanel.Controls.Add(this.label3);
            this.ToolsPanel.Controls.Add(this.label2);
            this.ToolsPanel.Controls.Add(this.label1);
            this.ToolsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolsPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.Size = new System.Drawing.Size(200, 100);
            this.ToolsPanel.TabIndex = 0;
            // 
            // ElementPanel
            // 
            this.ElementPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ElementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementPanel.Location = new System.Drawing.Point(200, 0);
            this.ElementPanel.Name = "ElementPanel";
            this.ElementPanel.Size = new System.Drawing.Size(584, 761);
            this.ElementPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.ElementPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ToolsPanel.ResumeLayout(false);
            this.ToolsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Panel ToolsPanel;
        private ElementPanel ElementPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

