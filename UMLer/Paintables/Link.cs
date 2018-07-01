using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.Controls;
using UMLer.Paintables.LinkPainters;

namespace UMLer.Paintables
{
    public class Link : LinkCore, ILink
    {
        [Browsable(false)]
        [XmlIgnore]
        public IPaintable Start { get => LinkPainter.Start; set
            {
                LinkPainter.Start = value;
                StartID = value.ID;
                RaisePropertyChanged("Start");
            } }
        public int StartID { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IPaintable Finish { get => LinkPainter.Finish; set
            {
                LinkPainter.Finish = value;
                FinishID = value.ID;
                RaisePropertyChanged("Finish");
            } }

        public int FinishID { get; set; }

        public float LineWidth { get => LinkPainter.LineWidth; set
            {
                LinkPainter.LineWidth = value;
                RaisePropertyChanged("LineWidth");
            }
        }
        private ILinkPainter LinkPainter;
        private BendStyle _BendStyle = BendStyle.DIRECT;
        public BendStyle BendStyle { get => _BendStyle; set
            {
                _BendStyle = value;
                if(!Diagram.Deserializing)
                    ChangeLinkPainter();
                RaisePropertyChanged("BendStyle");
            }
        }

        public override void Regenerate()
        {
            if (!Diagram.Deserializing)
                ChangeLinkPainter();
        }

        private void ChangeLinkPainter()
        {

            LinkPainter.FocusOutlinePen.DashPattern = Diagram.FocusDashPattern;
            var linkBuilder = new LinkPainterBuilder()
            {
                Link = this,
                BendStyle = this.BendStyle,
                Finish = Finish,
                Start = Start,
                DashPattern = LinkPainter.FocusOutlinePen.DashPattern,
                FocusOutlinePen = LinkPainter.FocusOutlinePen,
                Pen = LinkPainter.Pen,
                LineWidth = LineWidth,
                LinkColor = LinkPainter.Color,
                Supervisor = this
            };
            LinkPainter = linkBuilder.Build();
        }

        public override Color PrimaryColor
        {
            get => LinkPainter.Color; set
            {
                LinkPainter.Color = value;
                RaisePropertyChanged("PrimaryColor");
            }
        }

        public override bool Contains(Point p)
        {
            return LinkPainter.Contains(p);
        }

        public Link() : base()
        {
            LinkPainter = new DirectLinker<Link>();
        }

        public Link(IPaintable Start,IPaintable Finish) : base(Start.Parent)
        {
            LinkPainter = new DirectLinker<Link>(this,Start,Finish);
            this.Start = Start;
            this.Finish = Finish;
        }

        public override void Paint(Graphics g)
        {
            LinkPainter.Paint(g);
            
        }
    }
}
