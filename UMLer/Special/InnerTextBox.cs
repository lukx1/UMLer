using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLer.Special
{
    public class InnerTextBox
    {
        public string Name { get; set; } = "NoName";
        public Point Location { get; set; }
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public Color Color { get; set; } = Color.Black;
        public Size Size { get; set; }
        public StringFormat StringFormat { get; set; } = new StringFormat() { };
        public string Text { get; set; }

        public virtual bool Contains(Point p)
        {
            return new Rectangle(Location,Size).Contains(p);
        }

        public virtual void Paint(Graphics g)
        {
            g.DrawString(Text,Font,new SolidBrush(Color),new RectangleF(Location.X,Location.Y,Size.Width,Size.Height),StringFormat);
        }

        public virtual void KeyUp(KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Back)
                Text = Text.Substring(0, Text.Length - 1);
        }

        public virtual void KeyPress(KeyPressEventArgs args)
        {
            if (Char.IsControl(args.KeyChar))
                return;
            Text += args.KeyChar;
        }

    }
}
