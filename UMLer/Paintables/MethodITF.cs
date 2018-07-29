using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.DiagramData;
using UMLer.Special;

namespace UMLer.Paintables
{
    public class MethodITF : InnerTextField
    {
        private IMethod RepresentingMethod;
        private ClazzHelper helper = new ClazzHelper();

        public MethodITF(IPaintable ParentPaintable,IMethod RepresentingMethod) : base(ParentPaintable)
        {
            this.RepresentingMethod = RepresentingMethod;
            TextWritten += MethodITF_TextWritten;
        }

        public override void Paint(Graphics g)
        {
            if (this.IsFocused())
            {
                base.Paint(g);
            }
            else
            {
                NoFocusPaint(g);
            }
        }

        private void PaintAccessMod(Graphics g)
        {
            var drawLoc = this.Location;
            switch (RepresentingMethod.AccessModifier)
            {
                default:
                case AccessModifier.PUBLIC:
                    g.DrawImage(Properties.Resources.ipublic, drawLoc);
                    break;
                case AccessModifier.PRIVATE:
                    g.DrawImage(Properties.Resources.iprivate, drawLoc);
                    break;
                case AccessModifier.PROTECTED:
                    g.DrawImage(Properties.Resources.iprotected, drawLoc);
                    break;
            }

        }

        private void PaintExtras(Graphics g)
        {
            int offset = Diagram.ImageSize;
            var yLoc = this.Location.Y;
            if (RepresentingMethod.ExtraModifiers.Contains(ExtraModifier.ABSTRACT))
            {
                g.DrawImage(Properties.Resources.iabstract, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
            if (RepresentingMethod.ExtraModifiers.Contains(ExtraModifier.OVERRIDE))
            {
                g.DrawImage(Properties.Resources.ioverride, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
            if (RepresentingMethod.ExtraModifiers.Contains(ExtraModifier.STATIC))
            {
                g.DrawImage(Properties.Resources.istatic, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
        }

        private void NoFocusPaint(Graphics g)
        {
            var offset = Diagram.ImageSize;
            PaintAccessMod(g);
            PaintExtras(g);
            offset += Diagram.ImageSize * RepresentingMethod.ExtraModifiers.Count();
            g.DrawString(
                RepresentingMethod.ReturnType + " " +
                RepresentingMethod.Name + "(" +
                RepresentingMethod.ParametersToSyntax() + ")",
                Font,
                new SolidBrush(PrimaryColor),
                new RectangleF(Location.X+offset,Location.Y,DisplayRectangle.Width-offset,DisplayRectangle.Height),
                new StringFormat()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    Trimming = StringTrimming.EllipsisCharacter
                }
                );
        }

        private void MethodITF_TextWritten(object sender, TextWrittenArgs e)
        {
            try
            {
                var res = helper.MakeMethodFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingMethod.AccessModifier = res.AccessModifier;
                this.RepresentingMethod.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingMethod.Name = res.Name;
                this.RepresentingMethod.Parameters = res.Parameters;
                this.RepresentingMethod.ReturnType = res.ReturnType;
                this.Text = RepresentingMethod.ToSyntax();
            }
            catch (ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
