using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Paintables
{
    public class ClassITF : InnerTextField
    {
        private Clazz clazz;

        public ClassITF(IPaintable ParentPaintable,Clazz clazz) : base(ParentPaintable)
        {
            this.clazz = clazz;
        }

        public override void Paint(Graphics g)
        {
            if (this.IsFocused())
            {
                base.Paint(g);
            }
            else
            {
                PaintDisplayMode(g);
            }
        }

        private void PaintClassSymbol(Graphics g)
        {
            switch (clazz.AccessModifier)
            {
                case AccessModifier.PUBLIC:
                    break;
                case AccessModifier.PRIVATE:
                    break;
                case AccessModifier.PROTECTED:
                    break;
                case AccessModifier.PRIVATE_PROTECTED:
                    break;
                case AccessModifier.INTERNAL:
                    break;
                case AccessModifier.PROTECTED_INTERNAL:
                    break;
            }
            
        }

        private void PaintStaticSymbol(Graphics g)
        {

        }

        private void PaintDisplayMode(Graphics g)
        {
            g.DrawString(clazz.Name,Font, new SolidBrush(PrimaryColor), new RectangleF(Location.X, Location.Y, Size.Width, Size.Height), StringFormat);
            PaintClassSymbol(g);
            PaintStaticSymbol(g);
        }

    }
}
