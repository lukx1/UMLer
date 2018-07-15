using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Paintables
{
    public class SClassITF : InnerTextField
    {
        private Clazz clazz;

        public SClassITF(IPaintable ParentPaintable, Clazz clazz) : base(ParentPaintable)
        {
            this.clazz = clazz;
        }

        private string GetClassFullString()
        {
            string str = "";
            if (clazz.IsAbstract)
                str += "absract ";
            else if (clazz.IsStatic)
                str += "static ";
            else if (clazz.IsSealed)
                str += "sealed ";
            if (clazz.AccessModifier == AccessModifier.NOTHING) { }
            else
                str += clazz.AccessModifier.ToString().Replace('_', ' ').ToLower()+" ";
            if (clazz.IsEnum)
                str += "enum";
            else if (clazz.IsInterface)
                str += "interface";
            else
                str += "class";
            return str;
        }

        public override void Paint(Graphics g)
        {
            if (this.IsFocused())
            {
                var printStr = GetClassFullString();
                g.DrawString(printStr, Font, new SolidBrush(PrimaryColor), new RectangleF(Location.X, Location.Y, Size.Width, Size.Height), StringFormat);
            }
            else
            {
                string printStr = clazz.IsInterface ? "<<" + clazz.Name + ">>" : clazz.IsEnum ? "["+clazz.Name+"]" : clazz.Name;
                g.DrawString(printStr, Font, new SolidBrush(PrimaryColor), new RectangleF(Location.X, Location.Y, Size.Width, Size.Height), StringFormat);
            }
        }
    }
}
