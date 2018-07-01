using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Loading;
using UMLer.Paintables;
using UMLer.Tools;

namespace UMLer
{
    public partial class Form1 : Form
    {

        private Diagram diagram = new Diagram();

        private void TestBoot()
        {
            var e1 = new LargeClass(ElementPanel) { Location = new Point(100, 100), Properties = new List<string>() { "Tohle", "Je", "Test" } };
            var e2 = new LargeClass(ElementPanel) { Location = new Point(500, 100) };
            ElementPanel.Paintables.Add(e1);
            ElementPanel.Paintables.Add(e2);
            ElementPanel.Paintables.Add(new Link(e1, e2));
            ElementPanel.Paintables.Add(new SimpleClass(ElementPanel) { Location = new Point(200, 200) });
        }

        private void LoadBoot()
        {
            Saver saver = new Saver()
            {
                PathToFile = @"C:\Out\UMLer.xml"
            };
            var r = saver.LoadDiagram();
            saver.LinkPaintables(r,diagram);
            diagram.ElementPanel.Paintables = r;
        }

        private void Init()
        {
            diagram.BindProperty(propertyGrid);
            diagram.BindStatusLabel(labelStatus);
            diagram.BindElementPanel(ElementPanel);
            diagram.SelectedTool = new Pointer();
            ElementPanel.BindDiagram(diagram);
        }

        public Form1()
        {
            InitializeComponent();
            Init();
            TestBoot();
            //LoadBoot();
            
        }

        private void LabelPointer_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Pointer();
        }

        private void LabelLinker_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Linker();
        }

        private void LabelAddComplex_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Saver saver = new Saver()
            {
                PathToFile = @"C:\Out\UMLer.xml"
            };
            saver.SaveDiagram(diagram);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saver saver = new Saver()
            {
                PathToFile = @"C:\Out\UMLer.xml"
            };
            var r = saver.LoadDiagram();
            saver.LinkPaintables(r, diagram);
            diagram.ElementPanel.Paintables = r;
        }
    }
}
