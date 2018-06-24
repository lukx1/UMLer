using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables;

namespace UMLer.Controls
{
    public class PaintableEventArgs
    {
        public IPaintable IPaintable { get; private set;}

        public PaintableEventArgs(IPaintable paintable)
        {
            this.IPaintable = paintable;
        }
    }
}
