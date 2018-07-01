using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Controls;

namespace UMLer.Paintables
{
    public abstract class DraggableCoreClass : CoreClass , IDrag
    {
        private DragCore DragCore;

        public event EventHandler DraggingStarted;
        public event EventHandler DraggingStopped;

        public virtual void RaiseDraggingStopped(EventArgs e)
        {
            if (DraggingStopped != null)
                DraggingStopped(this, e);
        }

        public virtual void RaiseDraggingStarted(EventArgs e)
        {
            if (DraggingStarted != null)
                DraggingStarted(this, e);
        }

        public DraggableCoreClass() : base()
        {
            Init();
        }

        private void Init()
        {
            this.Name = "Draggable Class";
            DragCore = new DragCore(this);

            DragCore.DraggingStarted += DraggingStarted;
            DragCore.DraggingStopped += DraggingStopped;
        }

        public DraggableCoreClass(ElementPanel Parent) : base(Parent)
        {
            Init();
        }

       
    }
}
