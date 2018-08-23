using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
    public class Deleter : AbstractTool
    {
        public override string Name => "Deleter";

        public override string Description => "Deletes objects";

        public override void Clicked(MouseEventArgs args)
        {
            base.Clicked(args);
        }

        public override void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            base.ClickedOnPaintable(args,nullablePaintable);
            if(Diagram != null && nullablePaintable != null)
            {
                Diagram.RemoveWithAsoc(nullablePaintable);
            }
        }
    }
}
