using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;

namespace UMLer.Paintables.LinkPainters
{
    public interface ILinkPainter
    {
        IPaintable Start { get; set; }
        IPaintable Finish { get; set; }
        float LineWidth { get; set; }
        Color Color { get; set; }
        Pen Pen { get; set; }
        Pen FocusOutlinePen { get; set; }
        IPaintable Supervisor { get; set; }
        Point ConnectionStart { get; set; }
        Point ConnectionFinish { get; set; }


        bool Contains(Point p);
        void Paint(Graphics g);

        bool IsFocused();
    }
}
