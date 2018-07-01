using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;

namespace UMLer.Paintables
{
    public class SimpleClass : DraggableCoreClass
    {

        public SimpleClass() : base()
        {
        }

        public SimpleClass(ElementPanel Parent) : base(Parent)
        {
        }

        public override void Paint(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackgroundColor), DisplayRectangle);
            g.DrawRectangle(new Pen(new SolidBrush(PrimaryColor)), DisplayRectangle);
            g.DrawString(
                this.Name,
                SystemFonts.DefaultFont,
                new SolidBrush(this.PrimaryColor),
                this.Location.X + this.Width / 2,
                this.Location.Y + this.Height / 2,
                new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                }
                );
        }
    }
}
