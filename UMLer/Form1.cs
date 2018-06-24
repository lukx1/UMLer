using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var e1 = new LargeClass(ElementPanel) { Location = new Point(100, 100) };
            var e2 = new LargeClass(ElementPanel) { Location = new Point(500, 100) };
            ElementPanel.Paintables.Add(e1);
            ElementPanel.Paintables.Add(e2);
            ElementPanel.Paintables.Add(new Link(ElementPanel, e1, e2));
        }
    }
}
