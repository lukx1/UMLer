using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UMLer.Controls;
using UMLer.Special;

namespace UMLer.Paintables
{
    [Serializable]
    public abstract class CoreClass : IPaintable
    {
        private string _Name;
        [Category("Info")]
        public virtual string Name { get => _Name; set {
                _Name = value;
                RaisePropertyChanged("Name");
            } }

        [ReadOnly(true)]
        public int ID { get; set; } = Diagram.GenerateID();

        private Point _Location;
        [Category("Info")]
        public virtual Point Location { get => _Location; set  
                {
                if(value == _Location)
                    return;
                _Location = value;
                RaisePropertyChanged("Location");
                LocationChanged?.Invoke(this, new EventArgs());
                }
        }

        private Size _Size;
        [Category("Info")]
        public virtual Size Size { get => _Size; set {
                _Size = value;
                RaisePropertyChanged("Size");
            } }

        private  Color _PrimaryColor;
        [XmlElement(Type = typeof(XmlColor))]
        [Category("Color")]
        public virtual Color PrimaryColor { get => _PrimaryColor; set {
                _PrimaryColor = value;
                RaisePropertyChanged("Primary Color");
            } }

        private Color _SecondaryColor;
        [XmlElement(Type = typeof(XmlColor))]
        [Category("Color")]
        public virtual Color SecondaryColor { get => _SecondaryColor; set {
                _SecondaryColor = value;
                RaisePropertyChanged("SecondaryColor");
            } }

        private Color _BackgroundColor = Color.White;
        [XmlElement(Type = typeof(XmlColor))]
        [Category("Color")]
        public virtual Color BackgroundColor { get => _BackgroundColor; set {
                _BackgroundColor = value;
                RaisePropertyChanged("BackgroundColor");
            } } 

        private Font _Font;
        [XmlIgnore]
        [Category("Misc")]
        public virtual Font Font { get => _Font; set {
                _Font = value;
                RaisePropertyChanged("Font");
            } }

        [BrowsableAttribute(false)]
        [XmlIgnore]
        public virtual ElementPanel Parent { get; set; }

        [Browsable(false)]
        protected virtual Rectangle DisplayRectangle => new Rectangle(Location, Size);
        [Category("Info")]
        public virtual int Width { get => Size.Width; set => Size = new Size(value,Size.Height); }
        [Category("Info")]
        public virtual int Height { get => Size.Height; set => Size = new Size(Size.Width, value); }

        private int _ZIndex = 0;
        public int ZIndex { get => _ZIndex; set
            {
                _ZIndex = value;
                RaisePropertyChanged("ZIndex");
            } }

        public virtual void Regenerate() { }

        public bool IsFocused() => Parent != null && this == Parent.FocusedElement;



        protected virtual void RaisePropertyChanged(string propName,bool invalidate = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            if (invalidate)
            {
                Parent?.Invalidate();
            }
        }

        public virtual bool Contains(Point p)
        {
            return new Rectangle(Location, Size).Contains(p);
        }

        public void Focus()
        {
            Parent.Focus(this);
        }

        public CoreClass(bool Bare = false)
        {
            if (Bare)
                return;
            Name = "Class";
            Location = new Point(0, 0);
            Size = new Size(100, 100);
            PrimaryColor = Color.Black;
            SecondaryColor = Color.Cyan;
            Font = SystemFonts.DefaultFont;
        }

        public CoreClass(ElementPanel Parent) : this()
        {
            this.Parent = Parent;         
        }

        public virtual void RaiseClicked(MouseEventArgs a)
        {
            Clicked?.Invoke(this, a);
        }

        public virtual void RaiseMouseMove(MouseEventArgs a)
        {
            MouseMove?.Invoke(this, a);
        }

        public virtual void RaiseMouseUp(MouseEventArgs a)
        {
            MouseUp?.Invoke(this, a);
        }

        public virtual void RaiseMouseDown(MouseEventArgs a)
        {
            MouseDown?.Invoke(this, a);
        }

        public event EventHandler LocationChanged;
        public event MouseEventHandler Clicked;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseDown;
        public event PropertyChangedEventHandler PropertyChanged;
        public event KeyEventHandler KeyUp;
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPressed;
        public event EventHandler FocusGained;
        public event EventHandler FocusLost;

        public virtual void Paint(Graphics g)
        {
        }

        public virtual void RaiseKeyDown(KeyEventArgs a)
        {
            KeyDown?.Invoke(this,a);
        }

        public virtual void RaiseKeyUp(KeyEventArgs a)
        {
            KeyUp?.Invoke(this, a);
        }

        public virtual void RaiseKeyPressed(KeyPressEventArgs a)
        {
            KeyPressed?.Invoke(this, a);
        }

        public virtual void RaiseFocusGained(EventArgs a)
        {
            FocusGained?.Invoke(this, a);
        }

        public virtual void RaiseFocusLost(EventArgs a)
        {
            FocusLost?.Invoke(this, a);
        }

        public virtual bool ShouldDrawFocusBox()
        {
            return true;
        }
    }
}
