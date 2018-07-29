using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLer.Paintables
{
    public class DragCore : IDrag
    {
        private IPaintable paintable;
        protected Point LatchPoint = new Point(0, 0);
        protected bool IsDragged = false;

        public event EventHandler DraggingStarted;
        public event EventHandler DraggingStopped;

        public DragCore(IPaintable paintable)
        {
            this.paintable = paintable;
            Bind();
        }

        private bool IsDragValid(Size dragAmount)
        {

            var curLoc = paintable.Location;
            var afterMoveLoc = curLoc + dragAmount;
            return !(
                afterMoveLoc.X < 0 ||
                afterMoveLoc.Y < 0 || 
                afterMoveLoc.X + paintable.Size.Width > paintable.Parent.Width ||
                (afterMoveLoc.Y + paintable.Size.Height) > paintable.Parent.Height
                );
        }

        private void Bind()
        {
            paintable.MouseMove += (object o, MouseEventArgs e) => DragController(e);
            paintable.MouseUp += (object o, MouseEventArgs e) => DragStopController(e);
            paintable.MouseDown += (object o, MouseEventArgs e) => LatchPoint = new Point(e.X, e.Y);
        }

        private void DragStopController(MouseEventArgs e)
        {
            if (IsDragged)
                RaiseDraggingStopped(e);
            IsDragged = false;
        }

        protected virtual void DragController(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (!IsDragged)
                    RaiseDraggingStarted(e);

                paintable.Focus();

                IsDragged = true;

                var moveAmount = new Size(e.X - LatchPoint.X, e.Y - LatchPoint.Y);
                if (IsDragValid(moveAmount))
                    DoMove(moveAmount);
                LatchPoint = e.Location;
                paintable.Parent.Invalidate();
            }
        }

        protected virtual void DoMove(Size moveAmount)
        {
            paintable.Location += moveAmount;
        }

        public void RaiseDraggingStarted(EventArgs a)
        {
            DraggingStarted?.Invoke(paintable, a);
        }

        public void RaiseDraggingStopped(EventArgs a)
        {
            DraggingStopped?.Invoke(paintable, a);
        }
    }
}
