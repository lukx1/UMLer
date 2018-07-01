using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    public class LinkMath
    {
        // Calculate the distance between
        // point pt and the segment p1 --> p2.
        public static double FindDistanceToSegment(
            Point pt, Point p1, Point p2, out Point closest)
        {
            var cclosest = new PointF();
            var dist = FindDistanceToSegment(
                new PointF(pt.X, pt.Y),
                new PointF(p1.X, p1.Y),
                new PointF(p2.X, p2.Y),
                out cclosest
                );

            closest = new Point((int)cclosest.X, (int)cclosest.Y);
            return dist;
        }

        // Calculate the distance between
        // point pt and the segment p1 --> p2.
        public static double FindDistanceToSegment(
            PointF pt, PointF p1, PointF p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
                (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
