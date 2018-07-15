using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UMLer.Special;

namespace UMLer.Paintables
{
    [NoSerialize]
    public class InnerTextField : CoreClass,IParentPaintable
    {
        private StringFormat _StringFormat= new StringFormat() { };
        public StringFormat StringFormat { get => _StringFormat; set
            {
                _StringFormat = value;
                RaisePropertyChanged("StringFormat");
            } }

        private string _Text = "";
        public virtual string Text { get => _Text; set {
                if(value == null)
                {
                    value = "";
                }
                _Text = value;
                RaisePropertyChanged("Text");
            } }
        private bool _PaintBackground = false;
        public virtual bool PaintBackground
        {
            get => _PaintBackground;
            set
            {
                _PaintBackground = value;
                RaisePropertyChanged("PaintBackground");
            }
        }
        private IPaintable _ParentPaintable;
        [XmlIgnore]
        [Browsable(false)]
        public IPaintable ParentPaintable
        {
            get => _ParentPaintable;
            set
            {
                if (_ParentPaintable != null && _ParentPaintable != value)
                    throw new InvalidOperationException("Can't change Parent Paintable");
                _ParentPaintable = value;
                RaisePropertyChanged("ParentPaintable");
            }
        }

        [Browsable(false)]
        public override Point Location { get => base.Location; set => base.Location = value; }
        [Browsable(false)]
        public override Size Size { get => base.Size; set => base.Size = value; }
        [Browsable(false)]
        public override int Height { get => base.Height; set => base.Height = value; }
        [Browsable(false)]
        public override int Width { get => base.Width; set => base.Width = value; }

        public override Color PrimaryColor { get => ParentPaintable.PrimaryColor; set => ParentPaintable.PrimaryColor = value; }
        public override Color SecondaryColor { get => ParentPaintable.SecondaryColor; set => ParentPaintable.SecondaryColor = value; }
        public override Color BackgroundColor { get => ParentPaintable.BackgroundColor; set => ParentPaintable.BackgroundColor = value; }
        public override Font Font { get => ParentPaintable.Font; set => ParentPaintable.Font = value; }

        public InnerTextField(IPaintable ParentPaintable) : base(true)
        {
            _ParentPaintable = ParentPaintable;
           // FocusGained += (object o, EventArgs e) => Parent.ForceFocusAround(ParentPaintable);
           // FocusLost += (object o, EventArgs e) => Parent.ReleaseForcedFocusAround();
            KeyUp += (object o, KeyEventArgs e) => FieldKeyUp(e);
            KeyDown += (object o, KeyEventArgs e) => FieldKeyDown(e);
            KeyPressed += (object o, KeyPressEventArgs e) => FieldKeyPress(e);
            ParentPaintable.PropertyChanged += ParentPaintable_PropertyChanged;
        }

        private void ParentPaintable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {//TODO:Do only if that prop has been changed
            /*this.BackgroundColor = ParentPaintable.BackgroundColor;
            this.PrimaryColor = ParentPaintable.PrimaryColor;
            this.SecondaryColor = ParentPaintable.SecondaryColor;
            this.Font = ParentPaintable.Font;*/
        }

        public override void Paint(Graphics g)
        {
            if(PaintBackground)
                g.FillRectangle(new SolidBrush(BackgroundColor), DisplayRectangle);
            g.DrawString(Text,Font,new SolidBrush(PrimaryColor),new RectangleF(Location.X,Location.Y,Size.Width,Size.Height),StringFormat);
        }

        public virtual void FieldKeyUp(KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Back && Text.Length > 0)
                Text = Text.Substring(0, Text.Length - 1);
        }

        public virtual void FieldKeyPress(KeyPressEventArgs args)
        {
            if (Char.IsControl(args.KeyChar))
                return;
            Text += args.KeyChar;
        }


        public virtual void FieldKeyDown(KeyEventArgs args)
        {
            //Empty
        }

        public override bool ShouldDrawFocusBox()
        {
            return false;
        }

    }
}
