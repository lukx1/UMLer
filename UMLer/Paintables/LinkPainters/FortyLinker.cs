using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Special;

namespace UMLer.Paintables.LinkPainters
{
    public class FortyLinker<T> : CoreLinkPainter where T : ILink, IPaintable
    {
        public override Point ConnectionStart { get; set; }
        public override Point ConnectionFinish { get; set; }
        private const double LineSwapThreshold = 20;
        private Point midPointS;
        private Point midPointF;

        public static DirectLinker<T1> CreateDefault<T1>(T1 link) where T1 : ILink, IPaintable
        {
            var linker = new DirectLinker<T1>(link);
            return linker;
        }

        public FortyLinker() : base()
        {
            FocusOutlinePen = new Pen(new SolidBrush(Color.Black), LineWidth * Diagram.FocusPenWidthMultiplier);
        }

        private void Init()
        {
            FocusOutlinePen = new Pen(new SolidBrush(Color.Black), LineWidth * Diagram.FocusPenWidthMultiplier);
            Start.PropertyChanged += Supervisor_PropertyChanged;
            Finish.PropertyChanged += Supervisor_PropertyChanged;
            Supervisor.PropertyChanged += Supervisor_PropertyChanged;
            CalculateLine();
        }

        public FortyLinker(T link, IPaintable Start, IPaintable Finish) : base(Start,Finish,link)
        {
            this.Start = Start;
            this.Finish = Finish;
            Init();
        }

        public FortyLinker(T link) : base(link.Start, link.Finish, link) 
        {
            this.Start = Start;
            this.Finish = Finish;
            Init();
        }

        private void CalculateLine()
        {
            var conPoints = FindClosestPointsOnSides();
            ConnectionStart = conPoints[0];
            ConnectionFinish = conPoints[1];
            midPointS = new Point(ConnectionStart.X, (ConnectionStart.Y + ConnectionFinish.Y) / 2);
            midPointF = new Point(ConnectionFinish.X, (ConnectionStart.Y + ConnectionFinish.Y) / 2);
        }

        private void Supervisor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CalculateLine();
            Supervisor.Parent.Invalidate();
        }

        private bool AreConPointsValid()
        {//TODO:Test this
            return ConnectionFinish != null && ConnectionStart != null;
        }

        private void SetAngles(Point bestPtA, Point bestPtB, Point[] Points)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] == bestPtA)
                {
                    ((ILink)Supervisor).AngleStart = i * (Math.PI / 2);
                }
                if (Points[i] == bestPtB)
                {
                    ((ILink)Supervisor).AngleFinish = (i % 4) * (Math.PI / 2);
                }
            }
        }

        public override bool Contains(Point p)
        {
            if (!AreConPointsValid())
                return false;

            var closest = new Point();
            var distance1 = LinkMath.FindDistanceToSegment(p, ConnectionStart, midPointS, out closest);
            var distance2 = LinkMath.FindDistanceToSegment(p, midPointS, midPointF, out closest);
            var distance3 = LinkMath.FindDistanceToSegment(p, midPointF, ConnectionFinish, out closest);
            return 
                distance1 <= Diagram.DistFromLinkClickAccept ||
                distance2 <= Diagram.DistFromLinkClickAccept ||
                distance3 <= Diagram.DistFromLinkClickAccept;
        }

        public override bool IsFocused()
        {
            return Supervisor.IsFocused();
        }

        /// <summary>
        /// Determines which sides of two
        /// rectangles are closest to eachother
        /// and gets two points in the middle of 
        /// those sides
        /// </summary>
        /// <returns>Closest points on sides of rectangles</returns>
        private Point[] FindClosestPointsOnSides()
        {
            var pts = new Point[8];
            for (int i = 0; i < 8; i++)
            {
                var paintable = i < 4 ? Start : Finish;
                var mathI = i % 4;
                var x = paintable.Location.X;
                var y = paintable.Location.Y;
                var w = paintable.Width;
                var h = paintable.Height;
                var n = ((mathI % 3) + mathI / 3);
                var m = (mathI + 1) % 2 + (mathI / 3) * 2;
                pts[i] = new Point(
                    (int)(x + w * (n / 2.0)),
                    (int)(y + h * (m / 2.0))
                    );
            }

            var bestDist = double.MaxValue;
            Point bestPtA = Point.Empty;
            Point bestPtB = Point.Empty;

            for (int i = 0; i < 4; i++)
            {
                var startPt = pts[i];
                for (int y = 0; y < 4; y++)
                {
                    var endPt = pts[4 + y];
                    var dist = Distance(startPt, endPt);
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        bestPtA = startPt;
                        bestPtB = endPt;
                    }
                }
            }
            SetAngles(bestPtA, bestPtB, pts);
            return new Point[2] { bestPtA, bestPtB };
        }

        private double Distance(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public override void Paint(Graphics g)
        {
            if (!AreConPointsValid())
                return;

            lock (Pen)
            {
                bool focused = false;
                if (IsFocused())
                {
                    Pen.Width *= Diagram.FocusPenWidthMultiplier;
                    focused = true;
                }
                
                g.DrawLines(
                    Pen,
                    new Point[] {
                        ConnectionStart,
                        midPointS,
                        midPointF,
                        ConnectionFinish
                    });

                if (focused)
                {
                    Pen.Width /= Diagram.FocusPenWidthMultiplier;
                }
            }
        }
    }
}
