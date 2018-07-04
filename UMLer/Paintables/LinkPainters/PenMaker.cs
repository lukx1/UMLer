using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables.LinkPainters
{
    public class PenMaker
    {
        public void ChangeTo(Pen pen,LinkType linkType)
        {
            pen.Width = Diagram.PenDefaultWidth;
            switch (linkType)
            {
                default:
                case LinkType.NONE:
                    pen.DashPattern = null;
                    break;
                case LinkType.INHERITANCE:
                    pen.DashPattern = new float[] { 4, 2 };
                    break;
                case LinkType.IMPLEMENTATION:
                    pen.DashPattern = new float[] { 2, 4 };
                    break;
                case LinkType.AGGREGATION:
                    pen.DashPattern = new float[] { 4, 2 };
                    break;
                case LinkType.COMPOSITION:
                    pen.DashPattern = new float[] { 4, 2 };
                    break;
                case LinkType.DEPENDANCE:
                    pen.DashPattern = new float[] { 2, 4 };
                    break;
            }
        }
    }
}
