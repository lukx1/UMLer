using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;

namespace UMLer.Paintables
{
    public class Link : CoreClass
    {
        
        public IPaintable Start { get; private set; }
        public IPaintable Finish { get; private set; }

        public Link(ElementPanel Parent,IPaintable Start,IPaintable Finish) : base(Parent)
        {
            this.Start = Start;
            this.Finish = Finish;
        }

        public override void Paint(Graphics g)
        {
            g.DrawLine(new Pen(new SolidBrush(PrimaryColor)), Start.Location, Finish.Location);
        }
    }
}
