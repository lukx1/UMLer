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
    public class Copier : AbstractTool
    {
        public override string Name => "Copier";

        public override string Description => "Copies any UML object";

        public override void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            base.ClickedOnPaintable(args, nullablePaintable);
            if(Diagram != null && nullablePaintable != null)
            {
                if (nullablePaintable is ISubordinate)
                {
                    Diagram.ClipBoard = ((ISubordinate)nullablePaintable).ParentPaintable;
                }
                else
                {
                    Diagram.ClipBoard = nullablePaintable;
                }
            }
        }
    }
}
