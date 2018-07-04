using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Color BackgroundColor { get; set; }       
        ElementPanel Parent { get; set; }
        int ID { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
        event EventHandler LocationChanged;
        event MouseEventHandler Clicked;
        event MouseEventHandler MouseUp;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseMove;
        event KeyEventHandler KeyUp;
        event KeyEventHandler KeyDown;
        event KeyPressEventHandler KeyPressed;

        void RaiseKeyDown(KeyEventArgs a);
        void RaiseKeyUp(KeyEventArgs a);
        void RaiseKeyPressed(KeyPressEventArgs a);
        void RaiseClicked(MouseEventArgs a);
        void RaiseMouseMove(MouseEventArgs a);
        void RaiseMouseUp(MouseEventArgs a);
        void RaiseMouseDown(MouseEventArgs a);

        bool Contains(Point p);
        
        void Paint(Graphics g);

        void Regenerate();

        void Focus();
        bool IsFocused();
    }
}
