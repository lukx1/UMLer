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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ToolsPanel = new System.Windows.Forms.Panel();
            this.labelAddComplex = new System.Windows.Forms.Label();
            this.labelLinker = new System.Windows.Forms.Label();
            this.labelPointer = new System.Windows.Forms.Label();
            this.ElementPanel = new UMLer.Controls.ElementPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.ToolsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.PropertiesPanel.Controls.Add(this.propertyGrid);
            this.PropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesPanel.Location = new System.Drawing.Point(0, 100);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(200, 661);
            this.PropertiesPanel.TabIndex = 1;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(196, 657);
            this.propertyGrid.TabIndex = 0;
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ToolsPanel.Controls.Add(this.button2);
            this.ToolsPanel.Controls.Add(this.button1);
            this.ToolsPanel.Controls.Add(this.labelAddComplex);
            this.ToolsPanel.Controls.Add(this.labelLinker);
            this.ToolsPanel.Controls.Add(this.labelPointer);
            this.ToolsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolsPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.Size = new System.Drawing.Size(200, 100);
            this.ToolsPanel.TabIndex = 0;
            // 
            // labelAddComplex
            // 
            this.labelAddComplex.AutoSize = true;
            this.labelAddComplex.Location = new System.Drawing.Point(11, 45);
            this.labelAddComplex.Name = "labelAddComplex";
            this.labelAddComplex.Size = new System.Drawing.Size(69, 13);
            this.labelAddComplex.TabIndex = 2;
            this.labelAddComplex.Text = "Add Complex";
            this.labelAddComplex.Click += new System.EventHandler(this.LabelAddComplex_Click);
            // 
            // labelLinker
            // 
            this.labelLinker.AutoSize = true;
            this.labelLinker.Location = new System.Drawing.Point(11, 28);
            this.labelLinker.Name = "labelLinker";
            this.labelLinker.Size = new System.Drawing.Size(36, 13);
            this.labelLinker.TabIndex = 1;
            this.labelLinker.Text = "Linker";
            this.labelLinker.Click += new System.EventHandler(this.LabelLinker_Click);
            // 
            // labelPointer
            // 
            this.labelPointer.AutoSize = true;
            this.labelPointer.Location = new System.Drawing.Point(11, 11);
            this.labelPointer.Name = "labelPointer";
            this.labelPointer.Size = new System.Drawing.Size(40, 13);
            this.labelPointer.TabIndex = 0;
            this.labelPointer.Text = "Pointer";
            this.labelPointer.Click += new System.EventHandler(this.LabelPointer_Click);
            // 
            // ElementPanel
            // 
            this.ElementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ElementPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ElementPanel.Diagram = null;
            this.ElementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementPanel.Location = new System.Drawing.Point(200, 0);
            this.ElementPanel.Name = "ElementPanel";
            this.ElementPanel.Size = new System.Drawing.Size(584, 761);
            this.ElementPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelStatus);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(200, 742);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 19);
            this.panel2.TabIndex = 2;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelStatus.Location = new System.Drawing.Point(0, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(16, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "SAve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(103, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ElementPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.PropertiesPanel.ResumeLayout(false);
            this.ToolsPanel.ResumeLayout(false);
            this.ToolsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Panel ToolsPanel;
        private ElementPanel ElementPanel;
        private System.Windows.Forms.Label labelAddComplex;
        private System.Windows.Forms.Label labelLinker;
        private System.Windows.Forms.Label labelPointer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

