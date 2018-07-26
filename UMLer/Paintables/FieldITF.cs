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
    public class FieldITF : InnerTextField
    {
        public override string Text { get => base.Text; set => base.Text = value; }
        private IField RepresentingField;
        private ClazzHelper helper = new ClazzHelper();

        public FieldITF(IPaintable ParentPaintable,IField RepresentingField) : base(ParentPaintable)
        {
            TextWritten += FieldITF_TextWritten;
            this.RepresentingField = RepresentingField;
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

        private void PaintAccessMod(Graphics g)
        {
            var drawLoc = this.Location;
            switch (RepresentingField.AccessModifier)
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
            int offset = Diagram.IconSize;
            var yLoc = this.Location.Y;
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.ABSTRACT))
            {
                g.DrawImage(Properties.Resources.iabstract, new Point(offset, yLoc));
                offset += Diagram.IconSize;
            }
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.OVERRIDE))
            {
                g.DrawImage(Properties.Resources.ioverride, new Point(offset, yLoc));
                offset += Diagram.IconSize;
            }
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.STATIC))
            {
                g.DrawImage(Properties.Resources.istatic, new Point(offset, yLoc));
                offset += Diagram.IconSize;
            }
        }

        private void PaintDisplayMode(Graphics g)
        {
            int offset = Diagram.IconSize; // Text is pushed by access mod img
            PaintAccessMod(g);
            offset += Diagram.IconSize * RepresentingField.ExtraModifiers.Count(); // Pushed by each additional image
            g.DrawString(
                RepresentingField.Type + " " + RepresentingField.Name,
                Font, new SolidBrush(PrimaryColor),
                new Point(this.Location.X+offset,
                this.Location.Y)
                );
        }

        private void FieldITF_TextWritten(object sender, TextWrittenArgs e)
        {
            try
            {
                var res = helper.MakeFieldFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingField.AccessModifier = res.AccessModifier;
                this.RepresentingField.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingField.Name = res.Name;
                this.RepresentingField.Type = res.Type;
                this.Text = RepresentingField.ToSyntax();
            }
            catch(ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
