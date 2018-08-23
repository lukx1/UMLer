using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.DiagramData;
using UMLer.Special;

namespace UMLer.Paintables
{
    public class FieldITF : InnerTextField, IClassing
    {
        public override string Text { get => base.Text; set => base.Text = value; }
        public IField RepresentingField;
        private ClazzHelper helper = new ClazzHelper();
        [Browsable(false)]
        public bool LastField { get; set; } = false;
        [Browsable(false)]
        public IClazz RepresentingClass { get; set; }
        private bool IsInitialized = true;

        public event EventHandler Initialized;

        public FieldITF(IPaintable ParentPaintable,IField RepresentingField, Clazz clazz, bool Initialized = true) : base(ParentPaintable)
        {
            TextWritten += FieldITF_TextWritten;
            this.RepresentingField = RepresentingField;
            this.RepresentingClass = clazz;
            this.IsInitialized = Initialized;
            if (!Initialized)
            {
                this.Text = "...";
            }
        }

        public override void Paint(Graphics g)
        {
            if (PaintBackground)
            {
                g.FillRectangle(new SolidBrush(BackgroundColor), DisplayRectangle);
                g.DrawRectangle(Pens.Black, DisplayRectangle);
                if (LastField)
                {
                    g.DrawLine(Pens.Black, this.Location + new Size(0, this.Size.Height - 2), this.Location + new Size(this.Size.Width, this.Size.Height - 2));
                }
            }
            if (!IsInitialized && !this.IsFocused())
            {
                g.DrawString("...", Font, Brushes.Black, DisplayRectangle, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            else if (this.IsFocused())
            {
                if (Text.Trim().EndsWith("...") && !this.IsInitialized)
                    Text = "";
                base.Paint(g);
            }
            else
            {
                PaintDisplayMode(g);
            }
        }

        private void PaintAccessMod(Graphics g)
        {
            var drawLoc = this.Location + new Size(2, 1);
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
            int offset = Diagram.ImageSize;
            var yLoc = this.Location.Y + 1;
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.ABSTRACT))
            {
                g.DrawImage(Properties.Resources.iabstract, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.OVERRIDE))
            {
                g.DrawImage(Properties.Resources.ioverride, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
            if (RepresentingField.ExtraModifiers.Contains(ExtraModifier.STATIC))
            {
                g.DrawImage(Properties.Resources.istatic, new Point(offset, yLoc));
                offset += Diagram.ImageSize;
            }
        }

        private void PaintDisplayMode(Graphics g)
        {
            int offset = Diagram.ImageSize; // Text is pushed by access mod img
            PaintAccessMod(g);
            offset += Diagram.ImageSize * RepresentingField.ExtraModifiers.Count(); // Pushed by each additional image
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
                if (e.NewText == "" && this.IsInitialized)
                {
                    RepresentingClass.Fields.Remove((Clazz.Field)this.RepresentingField);
                    if (ParentPaintable is DiagramClass)
                    {
                        ((DiagramClass)ParentPaintable).RemoveField((Clazz.Field)this.RepresentingField);
                        ParentPaintable.Parent.Diagram.RemoveWithAsocSilent(this);
                    }
                    this.Parent.Paintables.SilentRemove(this);
                    return;
                }
                var res = helper.MakeFieldFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingField.AccessModifier = res.AccessModifier;
                this.RepresentingField.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingField.Name = res.Name;
                this.RepresentingField.Type = res.Type;
                this.Text = RepresentingField.ToSyntax();
                if (!IsInitialized)
                {
                    IsInitialized = true;
                    Initialized?.Invoke(this, new EventArgs());
                    RepresentingClass.Fields.Add((Clazz.Field)this.RepresentingField);
                }
            }
            catch(ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
