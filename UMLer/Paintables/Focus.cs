using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;
using UMLer.Special;

namespace UMLer.Paintables
{
    [NoSerialize]
    public class Focus : CoreClass
    {
        private IPaintable _FocusAround { get; set; }
        public IPaintable FocusAround { get => _FocusAround; set
            {
                _FocusAround = value;
                FocusAroundChanged();
            }
        }
        public int DistanceFromBox = Diagram.FocusDistanceFromBox;
        public Pen Pen = new Pen(Brushes.Black) { DashPattern = Diagram.FocusDashPattern };

        public Focus(ElementPanel panel) : base(panel)
        {
            this.FocusAround = FocusAround;
        }

        private void FocusAroundChanged()
        {

        }

        public override void Paint(Graphics g)
        {
            if (FocusAround == null)
                return;

            g.DrawLines(Pen, new Point[]
            {
                new Point(FocusAround.Location.X-DistanceFromBox,FocusAround.Location.Y-DistanceFromBox),
                new Point(FocusAround.Location.X+FocusAround.Width+ DistanceFromBox,FocusAround.Location.Y-DistanceFromBox),
                new Point(FocusAround.Location.X+FocusAround.Width+ DistanceFromBox,FocusAround.Location.Y+ FocusAround.Height+DistanceFromBox),
                new Point(FocusAround.Location.X-DistanceFromBox,FocusAround.Location.Y+ FocusAround.Height+DistanceFromBox),
                new Point(FocusAround.Location.X-DistanceFromBox,FocusAround.Location.Y-DistanceFromBox),
            });
        }
    }
}
