using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Tools
{
    public class Pointer : ITool
    {
        public string Name => "Pointer";

        public string Description => "Used for selecting elements";

        public void Clicked(MouseEventArgs args)
        {

        }

        public void ClickedOnPaintable(MouseEventArgs a, IPaintable paintable)
        {
            paintable.Focus();
        }
    }
}
