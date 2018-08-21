using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
    public class Cutter : AbstractTool
    {
        public override string Name => "Cutter";

        public override string Description => "Cuts UML objects out of the diagram";

        public override void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            base.ClickedOnPaintable(args, nullablePaintable);
            if(Diagram != null && nullablePaintable != null)
            {
                Diagram.ClipBoard = nullablePaintable;
                Diagram.ElementPanel.Paintables.Remove(nullablePaintable);
            }
        }
    }
}
