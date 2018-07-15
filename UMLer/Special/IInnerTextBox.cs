using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Special
{
    public interface IInnerTextBox
    {
        string Name { get; set; }
        Point Location { get; set; }
        Font Font { get; set; }
        Color Color { get; set; } 
        Size Size { get; set; }
        StringFormat StringFormat { get; set; } 
        string Text { get; set; }
        IPaintable Parent { get; set; }

        void Paint(Graphics g);
        bool Contains(Point p);
        void KeyUp(KeyEventArgs args);
        void KeyDown(KeyEventArgs args);
        void KeyPress(KeyPressEventArgs args);
    }
}
