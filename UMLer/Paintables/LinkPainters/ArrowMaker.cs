using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables.LinkPainters
{
    public class ArrowMaker
    {
        public int TriangleBaseWidth = 12;
        public int TriangleHeight = 20;
        public Color Color { get => Pen.Color; set
            {
                Pen.Color = value;
            }
        }
        public float LineWidth { get => Pen.Width; set
            {
                Pen.Width = LineWidth;
            } }
        public LinkType LinkType { get; set; }
        public Point ArrowTip { get; set; } = new Point();

        private Pen Pen = new Pen(new SolidBrush(Color.Black),Diagram.PenDefaultWidth);
        public double AngleRad { get => _AngleRad; set
            {
                RotateAround(TrianglePoints,AngleRad-value);
                _AngleRad = value % (Math.PI*2);
                
            } }
        private double _AngleRad = 0;
        private Point CenterPoint => new Point(ArrowTip.X-TriangleHeight/2);

        private Point[] TrianglePoints;

        public void GenerateArrow()
        {
            TrianglePoints = GenerateVPoints();
        }


        public ArrowMaker()
        {
            GenerateArrow();
        }



        private void RotateAround(Point[] points, double Angle)
        {
            for (int i = 0; i < points.Length; i++)
            {
                var pointToRotate = points[i];
                double cosTheta = Math.Cos(Angle);
                double sinTheta = Math.Sin(Angle);
                points[i] = new Point
                {
                    X =
                        (int)
                        (cosTheta * (pointToRotate.X - CenterPoint.X) -
                        sinTheta * (pointToRotate.Y - CenterPoint.Y) + CenterPoint.X),
                    Y =
                        (int)
                        (sinTheta * (pointToRotate.X - CenterPoint.X) +
                        cosTheta * (pointToRotate.Y - CenterPoint.Y) + CenterPoint.Y)
                };
            }
        }

        private Point[] GenerateVPoints()
        {
            var pts = new Point[]
            {
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y-TriangleBaseWidth/2),
                ArrowTip,
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y+TriangleBaseWidth/2),
            };
            RotateAround(pts,AngleRad);
            return pts;
        }

        private Point[] CreateCloseDoubleV()
        {
            Point[] CloneV = (Point[])TrianglePoints.Clone();
            Point[] RotPoint = new Point[] { TrianglePoints[1] };
            RotateAround(RotPoint, Math.PI);
            return new Point[]
            {
                RotPoint[0],CloneV[0],CloneV[1],CloneV[2],RotPoint[0]
            };
        }

        public void DrawArrow(Graphics gfx)
        {
            switch (LinkType)
            {
                default:
                case LinkType.NONE:
                    break;
                case LinkType.INHERITANCE:
                case LinkType.IMPLEMENTATION:
                case LinkType.DEPENDANCE:
                    gfx.DrawLines(Pen, TrianglePoints);
                    gfx.DrawLine(Pen, TrianglePoints[0], TrianglePoints[1]);
                    break;
                case LinkType.AGGREGATION:
                    lock (this)
                    {
                        gfx.DrawLine(Pen, TrianglePoints[0], TrianglePoints[1]);
                        AngleRad += Math.PI;
                        gfx.DrawLine(Pen, TrianglePoints[0], TrianglePoints[1]);
                        AngleRad -= Math.PI;
                    }
                    break;
                case LinkType.COMPOSITION:
                    gfx.FillPolygon(new SolidBrush(Color),CreateCloseDoubleV());
                    break;
            }
        }
    }
}
