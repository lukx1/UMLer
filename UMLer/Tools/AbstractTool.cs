using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
    public abstract class AbstractTool : ITool
    {
        [Category("Naming")]
        public abstract string Name { get; }
        [Category("Naming")]
        public abstract string Description { get; }

        [Browsable(false)]
        public Diagram Diagram { get; set; }

        public event PaintMouseHandler ClickedUsingTool;

        public virtual void Clicked(MouseEventArgs args)
        {
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(args));
        }

        public virtual void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(args, nullablePaintable));
        }
    }
}
