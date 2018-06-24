using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables
{
    public interface IDrag
    {
        event EventHandler DraggingStarted;
        event EventHandler DraggingStopped;

        void RaiseDraggingStarted(EventArgs a);
        void RaiseDraggingStopped(EventArgs a);
    }
}
