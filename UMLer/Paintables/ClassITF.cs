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
    public class ClassITF : InnerTextField
    {
        private IClazz RepresentingClass;
        private ClazzHelper helper = new ClazzHelper();
        private Rectangle ClassRect;

        public ClassITF(IPaintable ParentPaintable,IClazz RepresentingClass) : base(ParentPaintable)
        {
            this.RepresentingClass = RepresentingClass;
            this.TextWritten += ClassITF_TextWritten;
        }

        private void ClassITF_TextWritten(object sender, TextWrittenArgs e)
        {
            try
            {
                var res = helper.MakeClassFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingClass.AccessModifier = res.AccessModifier;
                this.RepresentingClass.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingClass.Name = res.Name;
                this.RepresentingClass.Fields = res.Fields;
                this.RepresentingClass.Methods = res.Methods;
                this.Text = RepresentingClass.ToSyntax();
            }
            catch (ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
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
            var drawLoc = this.Location;
            switch (RepresentingClass.ClassType)
            {
                default:
                case ClassType.CLASS:
                    g.DrawImage(Properties.Resources.iclass, drawLoc);
                    break;
                case ClassType.INTERFACE:
                    g.DrawImage(Properties.Resources.iinterface, drawLoc);
                    break;
                case ClassType.ENUM:
                    g.DrawImage(Properties.Resources.ienum, drawLoc);
                    break;
            }
            
        }

        private void PaintStaticSymbol(Graphics g)
        {
            if (!RepresentingClass.ExtraModifiers.Contains(ExtraModifier.STATIC))
                return;
            var drawLoc = this.Location + new Size(Diagram.ImageSize, 0); // Next to class symbol
            g.DrawImage(Properties.Resources.istatic, drawLoc);
        }

        private void PaintDisplayMode(Graphics g)
        {
            g.DrawString(RepresentingClass.Name,Font, new SolidBrush(PrimaryColor), new RectangleF(Location.X, Location.Y, Size.Width, Size.Height), StringFormat);
            PaintClassSymbol(g);
            PaintStaticSymbol(g);
        }
    }
}
