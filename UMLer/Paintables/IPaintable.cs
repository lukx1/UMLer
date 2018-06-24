using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;

namespace UMLer.Paintables
{
    public interface IPaintable
    {
        string Name { get; set; }
        Point Location { get; set; }
        Size Size { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Color PrimaryColor { get; set; }
        Color SecondaryColor { get; set; }
        ElementPanel Parent { get; set; }
        

        event EventHandler LocationChanged;
        event MouseEventHandler Clicked;
        event MouseEventHandler MouseUp;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseMove;
        

        void RaiseClicked(MouseEventArgs a);
        void RaiseMouseMove(MouseEventArgs a);
        void RaiseMouseUp(MouseEventArgs a);
        void RaiseMouseDown(MouseEventArgs a);
        


        bool Contains(Point p);
        
        void Paint(Graphics g);

        void Focus();
        bool IsFocused();
    }
}
