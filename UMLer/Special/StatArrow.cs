using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables.LinkPainters;

namespace UMLer.Special
{
    public static class StatArrow
    {
        private const int TriangleBaseWidth = 12;
        private const int TriangleHeight = 12;

        private static Point[] GenerateVPoints(Point ArrowTip, double RotateAngle)
        {
            var pts = new Point[]
            {
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y-TriangleBaseWidth/2),
                ArrowTip,
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y+TriangleBaseWidth/2),
            };
            RotateAround(pts, new Point(ArrowTip.X, ArrowTip.Y), RotateAngle);
            return pts;
        }

        private static void RotateAround(Point[] points, Point CenterPoint, double Angle)
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

        private static Point[] CreateCloseDoubleV(Point ArrowTip, double angle)
        {
            var pts =  new Point[]
            {
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y-TriangleBaseWidth/2),
                ArrowTip,
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y+TriangleBaseWidth/2),
                new Point(ArrowTip.X-TriangleBaseWidth*2,ArrowTip.Y),
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y-TriangleBaseWidth/2)
            };
            RotateAround(pts, ArrowTip, angle);
            return pts;
        }

        private static Point[] CreateCloseDoubleVPoly(Point ArrowTip,double angle)
        {
            var pts =  new Point[]
            {
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y-TriangleBaseWidth/2),
                ArrowTip,
                new Point(ArrowTip.X-TriangleHeight,ArrowTip.Y+TriangleBaseWidth/2),
                new Point(ArrowTip.X-TriangleBaseWidth*2,ArrowTip.Y)
            };
            RotateAround(pts, ArrowTip, angle);
            return pts;
        }

        public static void DrawArrow(Graphics gfx, LinkType linkType, Color color, float linewidth,
            double angle, Point arrowTip, bool IsFocused)
        {
            var Pen = new Pen(new SolidBrush(color), linewidth * (IsFocused ? Diagram.FocusPenWidthMultiplier : 1));
            var TrianglePoints = GenerateVPoints(arrowTip, angle);
            switch (linkType)
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
                    gfx.DrawLines(Pen, CreateCloseDoubleV(arrowTip,angle));
                    break;
                case LinkType.COMPOSITION:
                    gfx.FillPolygon(new SolidBrush(color), CreateCloseDoubleVPoly(arrowTip,angle));
                    break;
            }
        }
    }
}
