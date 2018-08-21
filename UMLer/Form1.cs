using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Coder;
using UMLer.Controls;
using UMLer.DiagramData;
using UMLer.Loading;
using UMLer.Paintables;
using UMLer.Special;
using UMLer.Tools;
using static UMLer.DiagramData.Clazz;

namespace UMLer
{
    public partial class Form1 : Form
    {
        private ModLoader ModLoader = new ModLoader();
        private Diagram diagram = new Diagram();
        private History history = new History();
        private static string FileBeingWorkedOn = null;

        private static Clazz CreateTestClass()
        {
            ClazzHelper helper = new ClazzHelper();
            Method test0 = helper.MakeMethodFromSyntax("private int X(int Y, bool b)");
            Method test1 = helper.MakeMethodFromSyntax("private bool Y(int Y, Helper boom)");
            Method test2 = helper.MakeMethodFromSyntax("private Dog X(int Y, bool b)");
            Field field0 = helper.MakeFieldFromSyntax("public int Jooo");
            Field field1 = helper.MakeFieldFromSyntax("public string Name");
            Field field2 = helper.MakeFieldFromSyntax("protected Dog Pet");
            return new Clazz()
            {
                Name = "TestClass",
                Methods = new List<Method>() {test0,test1,test2 },
                Fields = new List<Field>() { field0,field2,field1}
            }
            ;
        }

        private void TestBoot()
        {
            var e1 = new LargeClass(ElementPanel) { Location = new Point(100, 100), Properties = new List<string>() { "Tohle", "Je", "Test" } };
            var e2 = new LargeClass(ElementPanel) { Location = new Point(500, 100) };
            ElementPanel.Paintables.Add(e1);
            ElementPanel.Paintables.Add(e2);
            ElementPanel.Paintables.Add(new Comment(ElementPanel) { Location = new Point(500, 100), Size = new Size(100, 100) });
            ElementPanel.Paintables.Add(new Link(e1, e2) {BendStyle = Paintables.LinkPainters.BendStyle.FORTY_FIVE,LinkTypeFinish = Paintables.LinkPainters.LinkType.AGGREGATION,LinkTypeStart = Paintables.LinkPainters.LinkType.AGGREGATION });
            ElementPanel.Paintables.Add(new SimpleClass(ElementPanel) { Location = new Point(200, 200) });
            ElementPanel.Paintables.Add(new DiagramClass() { Parent = ElementPanel,Location = new Point(300,300), Size = new Size(100,100),RepresentingClass = CreateTestClass()});
            //ElementPanel.Paintables.Add(new InnerTextField() { Location = new Point(300,300),Size = new Size(100,13),Parent = ElementPanel});
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
            diagram.ElementPanel.Paintables = new ControledHashSet(r);
        }

        private void Init()
        {
            diagram.Mods = ModLoader.Mods;
            diagram.BindProperty(propertyGrid);
            diagram.BindStatusLabel(labelStatus);
            diagram.BindElementPanel(ElementPanel);
            diagram.SelectedTool = new Tools.Pointer();
            ElementPanel.BindDiagram(diagram);
        }

        private class Test
        {

        }

        private void LoadMods()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                var modsDir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Mods");
                ModLoader.LoadMods(modsDir);
            }
            else
            {
                ModLoader.LoadMods(Path.Combine(Directory.GetCurrentDirectory(), "mods"));
            }
            try
            {
                ModLoader.AppStarting();
            }
            catch(TimeoutException e)
            {
                MessageBox.Show(this, e.Message, "Mod load error");
            }
        }

        public Form1()
        {
            LoadMods();
            InitializeComponent();
            Init();

            ModLoader.EnvironmentLoaded(diagram);

            TestBoot();
            //this.Controls.Add(new InvisTextBox() {Location = new Point(200,200),Size = new Size(100,10) });
            //LoadBoot();
            
        }

        private void LabelPointer_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Tools.Pointer();
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
            diagram.ElementPanel.Paintables = new ControledHashSet(r);
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

        private void ExportAsImage()
        {
            var bitmap = ElementPanel.CreateBitmap();
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf"
            };
            var res = sf.ShowDialog();

            if (res == DialogResult.OK)
            {

                var path = sf.FileName;
                if (path == null)
                    return;
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    return;
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

        private void labelSSave_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ElementPanel.KeyDownE(e);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ElementPanel.KeyPressE(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ElementPanel.KeyUpE(e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ModLoader.AppShuttingDown();
        }

        private void labelCopier_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Copier();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAsImage();
        }

        private DialogResult ConfirmationDialog(string text, string title)
        {
            var msg = MessageBox.Show(this,text,title,MessageBoxButtons.YesNoCancel);
            return msg;
        }

        private void DoSaveProject(string fileName)
        {
            Saver saver = new Saver()
            {
                PathToFile = fileName
            };
            try
            {
                saver.SaveDiagram(diagram);
                FileBeingWorkedOn = Path.GetFileNameWithoutExtension(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }

        private void ProjectSaveAs()
        {
            var sf = new SaveFileDialog();
            if(FileBeingWorkedOn != null)
            {
                sf.FileName = FileBeingWorkedOn;
            }
            
            sf.Filter = "UMLer project (.ump)|*.ump|All files (*.*)|*.*";
            sf.FilterIndex = 1;
            sf.RestoreDirectory = true;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                DoSaveProject(sf.FileName);
            }
        }

        private void ProjectSave()
        {
            if(File.Exists(FileBeingWorkedOn))
            {
                DoSaveProject(FileBeingWorkedOn);
            }
            else
            {
                ProjectSaveAs();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var shouldSave = ConfirmationDialog("Do you want to save changes?", "UMLer");
            if (shouldSave == DialogResult.Cancel)
            {
                return;
            }
            else if (shouldSave == DialogResult.Yes)
            {
                ProjectSave();
            }
            diagram.ElementPanel.Paintables.Clear();
        }

        private void OpenFileDialog()
        {
            var sf = new OpenFileDialog();
            sf.CheckFileExists = true;
            sf.CheckPathExists = true;
            sf.Filter = "UMLer project (.ump)|*.ump|All files (*.*)|*.*";
            sf.FilterIndex = 1;
            sf.RestoreDirectory = true;

            if (sf.ShowDialog() == DialogResult.OK)
            {

                Saver saver = new Saver()
                {
                    PathToFile = sf.FileName
                };
                var r = saver.LoadDiagram();
                saver.LinkPaintables(r, diagram);
                diagram.ElementPanel.Paintables = new ControledHashSet(r);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var shouldSave = ConfirmationDialog("Do you want to save changes?", "UMLer");
            if (shouldSave == DialogResult.Cancel)
            {
                return;
            }
            else if (shouldSave == DialogResult.Yes)
            {
                ProjectSave();
            }
            OpenFileDialog();
        }

        

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSave();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSaveAs();
        }

        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Path to directory or null</returns>
        private string CodeExportDialog()
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                return fbd.SelectedPath;
            }
            else
            {
                return null;
            }
        }

        private void ExportCodeAs(Language language)
        {
            var coder = new CoderFactory() { Language = language }.CreateCoder();
            coder.Diagram = diagram;
            if (!coder.AreClazzesValid())
            {
                MessageBox.Show(
                    this,
                    "Classes can't be converted into selected language",
                    "Export Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            var path = CodeExportDialog();
            if (path == null)
            {
                return;
            }
            coder.OutputDirectory = path;
            try
            {
                coder.CreateCode();
                MessageBox.Show("Code successfully exported");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }

        private void javaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportCodeAs(Language.JAVA);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportCodeAs(Language.CSHARP);
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExportCodeAs(Language.CPP);
        }

        private void labelCreater_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Creater();
        }

        private void labelNoter_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Commenter();
        }

        private void labelPaster_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Paster();
        }

        private void labelDeleter_Click(object sender, EventArgs e)
        {
            diagram.SelectedTool = new Deleter();
        }
    }
}
