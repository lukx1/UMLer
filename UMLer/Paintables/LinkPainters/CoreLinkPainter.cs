using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;

namespace UMLer.Paintables.LinkPainters
{
    public abstract class CoreLinkPainter : ILinkPainter
    {
        public IPaintable Start { get; set; }
        public IPaintable Finish { get; set; }
        public float LineWidth { get; set; } = 1f;
        public Pen Pen { get; set; } = new Pen(new SolidBrush(Color.Black),1);
        public virtual Pen FocusOutlinePen { get; set; } = new Pen(new SolidBrush(Color.Black), 1);
        public IPaintable Supervisor { get; set; }
        public Color Color { get => Pen.Color; set {
                if(Pen != null)
                {
                    Pen.Color = value;
                }
                if(FocusOutlinePen != null)
                {
                    FocusOutlinePen.Color = value;
                }
            } }

        public abstract Point ConnectionStart { get; set; }
        public abstract Point ConnectionFinish { get; set; }

        private static Pen CreateOutlinePen()
        {
            return new Pen(new SolidBrush(Color.Black), 1f)
            {
                DashPattern = Diagram.FocusDashPattern
            };
        }

        public CoreLinkPainter()
        {

        }

        public CoreLinkPainter(IPaintable Start,IPaintable Finish, IPaintable Supervisor)
        {
            this.Supervisor = Supervisor;
            this.Start = Start;
            this.Finish = Finish; 
        }

        public abstract bool Contains(Point p);

        public abstract bool IsFocused();

        public abstract void Paint(Graphics g);
    }
}
