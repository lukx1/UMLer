using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.Special;

namespace UMLer.Paintables.LinkPainters
{
    public class LinkPainterBuilder
    {
        public Link Link { get; set; }
        public BendStyle BendStyle { get; set; } = BendStyle.DIRECT;
        public LinkType LinkType { get; set; } = LinkType.NONE;
        public IPaintable Supervisor { get; set; }
        public IPaintable Start { get; set; }
        public IPaintable Finish { get; set; }
        public Pen Pen { get; set; }
        public Pen FocusOutlinePen { get; set; }
        public float LineWidth { get; set; } = 1f;
        public float[] DashPattern { get; set; } = Diagram.FocusDashPattern;
        public Color LinkColor { get; set; } = Color.Black;


        public LinkPainterBuilder()
        {
            Pen = new Pen(new SolidBrush(Color.Black));
            FocusOutlinePen = new Pen(new SolidBrush(Color.Black), LineWidth) { DashPattern = DashPattern };
        }

        private void CheckValues()
        {
            if (Link == null)
                throw new NullReferenceException("Link can't be null");
            if (Supervisor == null)
                throw new NullReferenceException("Supervisor can't be null");
            if (Start == null)
                throw new NullReferenceException("Start can't be null");
            if (Finish == null)
                throw new NullReferenceException("Supervisor can't be null");
            if (Start == Finish)
                throw new InvalidOperationException("Start can't be finish");
            if (Supervisor.Parent == null)
                throw new NullReferenceException("Supervisor must have a parent");
            if (!(Supervisor is Link))
                throw new InvalidOperationException("Supervisor must be link");
        }

        private ILinkPainter CreatePainter(Link link)
        {

            switch (BendStyle)
            {
                default:
                case BendStyle.DIRECT:
                    return new DirectLinker<Link>(link);
                case BendStyle.FORTY_FIVE:
                    return new FortyLinker<Link>(link);
                    //case BendStyle.FORTY_FIVE:
                    //   break;
            }
        }

        public ILinkPainter Build()
        {
            CheckValues();
            var painter = CreatePainter(Link);
            painter.Start = Start;
            painter.Finish = Finish;
            painter.Color = Supervisor.PrimaryColor;
            painter.FocusOutlinePen = FocusOutlinePen;
            painter.Supervisor = Supervisor;
            painter.LineWidth = LineWidth;
            painter.Pen = Pen;
            return painter;
        }
    }
}