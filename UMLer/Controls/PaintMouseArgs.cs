using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Controls
{
    public class PaintMouseArgs : EventArgs
    {
        public IPaintable Paintable { get; private set; }

        public MouseButtons Button { get; private set; }
        //
        // Summary:
        //     Gets the number of times the mouse button was pressed and released.
        //
        // Returns:
        //     An System.Int32 that contains the number of times the mouse button was pressed
        //     and released.
        public int Clicks  { get; private set; }
        //
        // Summary:
        //     Gets the x-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The x-coordinate of the mouse, in pixels.
        public int X  { get; private set; }
        //
        // Summary:
        //     Gets the y-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The y-coordinate of the mouse, in pixels.
        public int Y  { get; private set; }
        //
        // Summary:
        //     Gets a signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.
        //
        // Returns:
        //     A signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant.
        public int Delta  { get; private set; }
        //
        // Summary:
        //     Gets the location of the mouse during the generating mouse event.
        //
        // Returns:
        //     A System.Drawing.Point that contains the x- and y- mouse coordinates, in pixels,
        //     relative to the upper-left corner of the form.
        public Point Location  { get; private set; }

        public PaintMouseArgs(MouseEventArgs MouseEventArgs,IPaintable Paintable)
        {
            this.Button = MouseEventArgs.Button;
            this.Clicks = MouseEventArgs.Clicks;
            this.X = MouseEventArgs.X;
            this.Y = MouseEventArgs.Y;
            this.Delta = MouseEventArgs.Delta;
            this.Location = MouseEventArgs.Location;
            this.Paintable = Paintable;
        }

        public PaintMouseArgs(MouseEventArgs MouseEventArgs) : this(MouseEventArgs,null)
        {
            
        }
    }
}
