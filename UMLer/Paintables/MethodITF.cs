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
    public class MethodITF : InnerTextField, IClassing
    {
        public IMethod RepresentingMethod;
        private ClazzHelper helper = new ClazzHelper();
        [Browsable(false)]
        public IClazz RepresentingClass { get; set; }
        public bool IsInitialized = true;

        public event EventHandler Initialized;

        public MethodITF(IPaintable ParentPaintable,IMethod RepresentingMethod, Clazz clazz, bool IsInitialized = true) : base(ParentPaintable)
        {
            this.RepresentingMethod = RepresentingMethod;
            TextWritten += MethodITF_TextWritten;
            this.RepresentingClass = clazz;
            this.IsInitialized = IsInitialized;
            if (!IsInitialized)
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
            }
            if (!IsInitialized && !this.IsFocused())
            {
                g.DrawString("...", Font, Brushes.Black, DisplayRectangle, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            else if (this.IsFocused())
            {
                if (Text.Trim().Contains("...") && !this.IsInitialized)
                    Text = "";
                base.Paint(g);
            }
            else
            {
                NoFocusPaint(g);
            }
        }

        private void PaintAccessMod(Graphics g)
        {
            var drawLoc = this.Location + new Size(2,1);
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
            var yLoc = this.Location.Y+1;
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
                if(e.NewText == "" && this.IsInitialized)
                {
                    RepresentingClass.Methods.Remove((Clazz.Method)this.RepresentingMethod);
                    if(ParentPaintable is DiagramClass)
                    {
                        ((DiagramClass)ParentPaintable).RemoveMethod((Clazz.Method)this.RepresentingMethod);
                        ParentPaintable.Parent.Diagram.RemoveWithAsocSilent(this);
                    }
                    this.Parent.Paintables.SilentRemove(this);
                    return;
                }
                var res = helper.MakeMethodFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingMethod.AccessModifier = res.AccessModifier;
                this.RepresentingMethod.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingMethod.Name = res.Name;
                this.RepresentingMethod.Parameters = res.Parameters;
                this.RepresentingMethod.ReturnType = res.ReturnType;
                this.Text = RepresentingMethod.ToSyntax();
                if (!IsInitialized)
                {
                    IsInitialized = true;
                    Initialized?.Invoke(this, new EventArgs());
                    RepresentingClass.Methods.Add((Clazz.Method)this.RepresentingMethod);
                }
            }
            catch (ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
