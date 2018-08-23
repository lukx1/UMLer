using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;
using UMLer.Special;

namespace UMLer.Paintables
{
    public class Comment : DraggableCoreClass
    {

        private InnerTextField itf;

        public string Text { get => itf == null ? "" : itf.Text; set { if (itf != null) itf.Text = value; else TextToSet = value; } }
        private string TextToSet = null;

        public Comment()
        {
            init();
        }

        public override void OnDeleted()
        {
            Parent.Paintables.SilentRemove(itf);
            Parent.Paintables.SilentRemove(this);
        }

        private void PaintShape(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackgroundColor), DisplayRectangle);
            g.DrawRectangle(Pens.Black,DisplayRectangle);
            g.DrawLine(Pens.Black, this.Location + new Size(0, Height / 4), this.Location + new Size(Width / 4, 0));
        }

        private void PaintText(Graphics g)
        {
            
        }

        public override void Paint(Graphics g)
        {
            PaintShape(g);
            if(itf == null)
            {
                itf = new InnerTextField(this);
                if (TextToSet != null)
                {
                    itf.Text = TextToSet;
                    TextToSet = null;
                }
                itf.Location = this.Location;
                itf.StringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisWord
                };
                itf.Parent = this.Parent;
                itf.Size = this.Size;
                itf.PaintBackground = false;
                itf.ZIndex = this.ZIndex + 1;
                this.Parent.Paintables.Add(itf);
            }
        }

        private void init()
        {
            this.PropertyChanged += (object o, PropertyChangedEventArgs a) => { if (itf == null) return; itf.Location = this.Location;itf.Size = this.Size; };
        }

        public override object SafeDeepClone()
        {
            var c = new Comment(this.Parent);
            DeepCloner.CopyIPaintable(this, c);
            c.Text = this.Text;
            return c;
        }

        public Comment(ElementPanel Parent) : base(Parent)
        {
            init();
        }
    }
}
