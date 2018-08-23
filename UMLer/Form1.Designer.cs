using System.Windows.Forms;
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
            this.elementPanel1 = new UMLer.Controls.ElementPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.ToolsPanel = new System.Windows.Forms.Panel();
            this.labelNoter = new System.Windows.Forms.Label();
            this.labelPaster = new System.Windows.Forms.Label();
            this.labelCopier = new System.Windows.Forms.Label();
            this.labelDeleter = new System.Windows.Forms.Label();
            this.labelCreater = new System.Windows.Forms.Label();
            this.labelLinker = new System.Windows.Forms.Label();
            this.labelPointer = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.javaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.copierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutModdingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ElementPanel = new UMLer.Controls.ElementPanel();
            this.panel1.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.ToolsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PropertiesPanel);
            this.panel1.Controls.Add(this.ToolsPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 725);
            this.panel1.TabIndex = 0;
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.AutoSize = true;
            this.PropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PropertiesPanel.Controls.Add(this.elementPanel1);
            this.PropertiesPanel.Controls.Add(this.propertyGrid);
            this.PropertiesPanel.Location = new System.Drawing.Point(0, 100);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(205, 649);
            this.PropertiesPanel.TabIndex = 1;
            // 
            // elementPanel1
            // 
            this.elementPanel1.Diagram = null;
            this.elementPanel1.Location = new System.Drawing.Point(190, 23);
            this.elementPanel1.Name = "elementPanel1";
            this.elementPanel1.Size = new System.Drawing.Size(8, 8);
            this.elementPanel1.TabIndex = 1;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(201, 645);
            this.propertyGrid.TabIndex = 0;
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ToolsPanel.Controls.Add(this.labelNoter);
            this.ToolsPanel.Controls.Add(this.labelPaster);
            this.ToolsPanel.Controls.Add(this.labelCopier);
            this.ToolsPanel.Controls.Add(this.labelDeleter);
            this.ToolsPanel.Controls.Add(this.labelCreater);
            this.ToolsPanel.Controls.Add(this.labelLinker);
            this.ToolsPanel.Controls.Add(this.labelPointer);
            this.ToolsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolsPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.Size = new System.Drawing.Size(200, 100);
            this.ToolsPanel.TabIndex = 0;
            // 
            // labelNoter
            // 
            this.labelNoter.AutoSize = true;
            this.labelNoter.Location = new System.Drawing.Point(10, 70);
            this.labelNoter.Name = "labelNoter";
            this.labelNoter.Size = new System.Drawing.Size(60, 13);
            this.labelNoter.TabIndex = 6;
            this.labelNoter.Text = "Commenter";
            this.labelNoter.Click += new System.EventHandler(this.labelNoter_Click);
            // 
            // labelPaster
            // 
            this.labelPaster.AutoSize = true;
            this.labelPaster.Location = new System.Drawing.Point(130, 30);
            this.labelPaster.Name = "labelPaster";
            this.labelPaster.Size = new System.Drawing.Size(37, 13);
            this.labelPaster.TabIndex = 5;
            this.labelPaster.Text = "Paster";
            this.labelPaster.Click += new System.EventHandler(this.labelPaster_Click);
            // 
            // labelCopier
            // 
            this.labelCopier.AutoSize = true;
            this.labelCopier.Location = new System.Drawing.Point(130, 10);
            this.labelCopier.Name = "labelCopier";
            this.labelCopier.Size = new System.Drawing.Size(37, 13);
            this.labelCopier.TabIndex = 4;
            this.labelCopier.Text = "Copier";
            this.labelCopier.Click += new System.EventHandler(this.labelCopier_Click);
            // 
            // labelDeleter
            // 
            this.labelDeleter.AutoSize = true;
            this.labelDeleter.Location = new System.Drawing.Point(130, 50);
            this.labelDeleter.Name = "labelDeleter";
            this.labelDeleter.Size = new System.Drawing.Size(41, 13);
            this.labelDeleter.TabIndex = 3;
            this.labelDeleter.Text = "Deleter";
            this.labelDeleter.Click += new System.EventHandler(this.labelDeleter_Click);
            // 
            // labelCreater
            // 
            this.labelCreater.AutoSize = true;
            this.labelCreater.Location = new System.Drawing.Point(10, 50);
            this.labelCreater.Name = "labelCreater";
            this.labelCreater.Size = new System.Drawing.Size(41, 13);
            this.labelCreater.TabIndex = 2;
            this.labelCreater.Text = "Creater";
            this.labelCreater.Click += new System.EventHandler(this.labelCreater_Click);
            // 
            // labelLinker
            // 
            this.labelLinker.AutoSize = true;
            this.labelLinker.Location = new System.Drawing.Point(10, 30);
            this.labelLinker.Name = "labelLinker";
            this.labelLinker.Size = new System.Drawing.Size(36, 13);
            this.labelLinker.TabIndex = 1;
            this.labelLinker.Text = "Linker";
            this.labelLinker.Click += new System.EventHandler(this.LabelLinker_Click);
            // 
            // labelPointer
            // 
            this.labelPointer.AutoSize = true;
            this.labelPointer.Location = new System.Drawing.Point(10, 10);
            this.labelPointer.Name = "labelPointer";
            this.labelPointer.Size = new System.Drawing.Size(40, 13);
            this.labelPointer.TabIndex = 0;
            this.labelPointer.Text = "Pointer";
            this.labelPointer.Click += new System.EventHandler(this.LabelPointer_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelStatus);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(200, 730);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.codeToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // codeToolStripMenuItem
            // 
            this.codeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.javaToolStripMenuItem,
            this.cToolStripMenuItem,
            this.cToolStripMenuItem1});
            this.codeToolStripMenuItem.Name = "codeToolStripMenuItem";
            this.codeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.codeToolStripMenuItem.Text = "Code";
            this.codeToolStripMenuItem.Click += new System.EventHandler(this.codeToolStripMenuItem_Click);
            // 
            // javaToolStripMenuItem
            // 
            this.javaToolStripMenuItem.Name = "javaToolStripMenuItem";
            this.javaToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.javaToolStripMenuItem.Text = "Java";
            this.javaToolStripMenuItem.Click += new System.EventHandler(this.javaToolStripMenuItem_Click);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.cToolStripMenuItem.Text = "C#";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
            // 
            // cToolStripMenuItem1
            // 
            this.cToolStripMenuItem1.Name = "cToolStripMenuItem1";
            this.cToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.cToolStripMenuItem1.Text = "C++";
            this.cToolStripMenuItem1.Click += new System.EventHandler(this.cToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointerToolStripMenuItem,
            this.linkerToolStripMenuItem,
            this.creatorToolStripMenuItem,
            this.noterToolStripMenuItem,
            this.toolStripMenuItem4,
            this.copierToolStripMenuItem,
            this.pasterToolStripMenuItem,
            this.deleterToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // pointerToolStripMenuItem
            // 
            this.pointerToolStripMenuItem.Name = "pointerToolStripMenuItem";
            this.pointerToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.pointerToolStripMenuItem.Text = "Pointer";
            // 
            // linkerToolStripMenuItem
            // 
            this.linkerToolStripMenuItem.Name = "linkerToolStripMenuItem";
            this.linkerToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.linkerToolStripMenuItem.Text = "Linker";
            // 
            // creatorToolStripMenuItem
            // 
            this.creatorToolStripMenuItem.Name = "creatorToolStripMenuItem";
            this.creatorToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.creatorToolStripMenuItem.Text = "Creator";
            // 
            // noterToolStripMenuItem
            // 
            this.noterToolStripMenuItem.Name = "noterToolStripMenuItem";
            this.noterToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.noterToolStripMenuItem.Text = "Noter";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(110, 6);
            // 
            // copierToolStripMenuItem
            // 
            this.copierToolStripMenuItem.Name = "copierToolStripMenuItem";
            this.copierToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.copierToolStripMenuItem.Text = "Copier";
            // 
            // pasterToolStripMenuItem
            // 
            this.pasterToolStripMenuItem.Name = "pasterToolStripMenuItem";
            this.pasterToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.pasterToolStripMenuItem.Text = "Paster";
            // 
            // deleterToolStripMenuItem
            // 
            this.deleterToolStripMenuItem.Name = "deleterToolStripMenuItem";
            this.deleterToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.deleterToolStripMenuItem.Text = "Deleter";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutModdingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // aboutModdingToolStripMenuItem
            // 
            this.aboutModdingToolStripMenuItem.Name = "aboutModdingToolStripMenuItem";
            this.aboutModdingToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.aboutModdingToolStripMenuItem.Text = "About Modding";
            // 
            // ElementPanel
            // 
            this.ElementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ElementPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ElementPanel.Diagram = null;
            this.ElementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementPanel.Location = new System.Drawing.Point(200, 24);
            this.ElementPanel.Name = "ElementPanel";
            this.ElementPanel.Size = new System.Drawing.Size(584, 725);
            this.ElementPanel.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ElementPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Unsaved UML Project - UMLer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PropertiesPanel.ResumeLayout(false);
            this.ToolsPanel.ResumeLayout(false);
            this.ToolsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Panel ToolsPanel;
        private ElementPanel ElementPanel;
        private System.Windows.Forms.Label labelLinker;
        private System.Windows.Forms.Label labelPointer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private ElementPanel elementPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private ToolStripMenuItem codeToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem pointerToolStripMenuItem;
        private ToolStripMenuItem linkerToolStripMenuItem;
        private ToolStripMenuItem creatorToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem aboutModdingToolStripMenuItem;
        private Label labelPaster;
        private Label labelCopier;
        private Label labelDeleter;
        private Label labelCreater;
        private Label labelNoter;
        private ToolStripMenuItem noterToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem copierToolStripMenuItem;
        private ToolStripMenuItem pasterToolStripMenuItem;
        private ToolStripMenuItem deleterToolStripMenuItem;
        private ToolStripMenuItem javaToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem1;
    }
}

