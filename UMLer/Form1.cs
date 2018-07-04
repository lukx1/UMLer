using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
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
            //ElementPanel.Controls.Add(new InvisTextBox() {Location = new Point(400,400),Size = new Size(20,13) });
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
            //this.Controls.Add(new InvisTextBox() {Location = new Point(200,200),Size = new Size(100,10) });
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

        private ImageFormat ParseImageFormat(string ext)
        {
            switch (ext)
            {
                case "BMP":
                    return ImageFormat.Bmp;
                case "GIF":
                    return ImageFormat.Gif;
                default:
                case "JPEG":
                    return ImageFormat.Jpeg;
                case "PNG":
                    return ImageFormat.Png;
                case "TIFF":
                    return ImageFormat.Tiff;
                case "WMF":
                    return ImageFormat.Wmf;
            }
        }

        private void labelSSave_Click(object sender, EventArgs e)
        {
            var bitmap = ElementPanel.CreateBitmap();
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf"
            };
            sf.ShowDialog();
            
            var path = sf.FileName;
            try
            {
                bitmap.Save(path, ParseImageFormat(Path.GetExtension(path).ToUpper()));
            }
            catch (ExternalException ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }
    }
}
