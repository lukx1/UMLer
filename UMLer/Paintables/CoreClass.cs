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
    public abstract class CoreClass : IPaintable
    {
        public string Name { get; set; }
        private Point _Location;
        public Point Location { get => _Location; set  
                {
                _Location = value;
                LocationChanged?.Invoke(this, new EventArgs());
                }
        }
        public Size Size { get; set; }
        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Font Font { get; set; }
        public ElementPanel Parent { get; set; }
        public virtual Rectangle DisplayRectangle => new Rectangle(Location, Size); 
        public virtual int Width { get => Size.Width; set => Size = new Size(value,Size.Height); }
        public virtual int Height { get => Size.Height; set => Size = new Size(Size.Width, value); }

        public bool IsFocused() => this == Parent.FocusedElement; 

        

        public virtual bool Contains(Point p)
        {
            return new Rectangle(Location, Size).Contains(p);
        }

        public void Focus()
        {
            Parent.Focus(this);
        }

        public CoreClass(ElementPanel Parent)
        {
            Name = "Class";
            Location = new Point(0, 0);
            Size = new Size(100, 100);
            PrimaryColor = Color.Black;
            SecondaryColor = Color.Cyan;
            Font = SystemFonts.DefaultFont;
            this.Parent = Parent;
            
        }

        

        public virtual void RaiseClicked(MouseEventArgs a)
        {
            if (Clicked != null)
                Clicked(this, a);
        }



        public virtual void RaiseMouseMove(MouseEventArgs a)
        {
            if (MouseMove != null)
                MouseMove(this,a);
        }

        public void RaiseMouseUp(MouseEventArgs a)
        {
            if (MouseUp != null)
                MouseUp(this, a);
        }

        public void RaiseMouseDown(MouseEventArgs a)
        {
            if (MouseDown != null)
                MouseDown(this, a);
        }

        public event EventHandler LocationChanged;
        public event MouseEventHandler Clicked;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseDown;

        public abstract void Paint(Graphics g);

        
    }
}
