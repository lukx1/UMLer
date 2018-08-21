using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Paintables;

namespace UMLer.Tools
{
    public class Pointer : ITool
    {
        public string Name => "Pointer";

        public string Description => "Used for selecting elements";

        public Diagram Diagram { get; set; }

        public event PaintMouseHandler ClickedUsingTool;

        public void Clicked(MouseEventArgs args)
        {
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(args));
        }

        public void ClickedOnPaintable(MouseEventArgs a, IPaintable paintable)
        {
            paintable.Focus();
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(a,paintable));
        }
    }
}
