﻿using System;
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
        public string Name { get => _Name; set {
                _Name = value;
                RaisePropertyChanged("Name");
            } }

        public int ID { get; set; } = Diagram.GenerateID();

        private Point _Location;
        public Point Location { get => _Location; set  
                {
                if(value == _Location)
                    return;
                _Location = value;
                RaisePropertyChanged("Location");
                LocationChanged?.Invoke(this, new EventArgs());
                }
        }

        private Size _Size;
        public Size Size { get => _Size; set {
                _Size = value;
                RaisePropertyChanged("Size");
            } }

        private Color _PrimaryColor;
        [XmlElement(Type = typeof(XmlColor))]
        public Color PrimaryColor { get => _PrimaryColor; set {
                _PrimaryColor = value;
                RaisePropertyChanged("Primary Color");
            } }

        private Color _SecondaryColor;
        [XmlElement(Type = typeof(XmlColor))]
        public Color SecondaryColor { get => _SecondaryColor; set {
                _SecondaryColor = value;
                RaisePropertyChanged("SecondaryColor");
            } }

        private Color _BackgroundColor = Color.White;
        [XmlElement(Type = typeof(XmlColor))]
        public Color BackgroundColor { get => _BackgroundColor; set {
                _BackgroundColor = value;
                RaisePropertyChanged("BackgroundColor");
            } } 

        private Font _Font;
        [XmlIgnore]
        public Font Font { get => _Font; set {
                _Font = value;
                RaisePropertyChanged("Font");
            } }

        [BrowsableAttribute(false)]
        [XmlIgnore]
        public ElementPanel Parent { get; set; }

        protected virtual Rectangle DisplayRectangle => new Rectangle(Location, Size); 
        public virtual int Width { get => Size.Width; set => Size = new Size(value,Size.Height); }
        public virtual int Height { get => Size.Height; set => Size = new Size(Size.Width, value); }

        public virtual void Regenerate() { }

        public bool IsFocused() => this == Parent.FocusedElement; 

        private void RaisePropertyChanged(string propName,bool invalidate = true)
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

        public CoreClass()
        {
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

        public void RaiseMouseUp(MouseEventArgs a)
        {
            MouseUp?.Invoke(this, a);
        }

        public void RaiseMouseDown(MouseEventArgs a)
        {
            MouseDown?.Invoke(this, a);
        }

        public event EventHandler LocationChanged;
        public event MouseEventHandler Clicked;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseDown;
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract void Paint(Graphics g);

        
    }
}