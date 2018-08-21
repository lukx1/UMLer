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
            if (nullablePaintable == null || Diagram == null)
                return;
            var linksToDelete = Diagram.GetAllLinks().Where(
                r => r.Start == nullablePaintable || r.Finish == nullablePaintable
                ).ToList();
            foreach (var link in linksToDelete)
            {
                Diagram.ElementPanel.Paintables.Remove((IPaintable)link);
            }
            Diagram.ElementPanel.Paintables.Remove(nullablePaintable);
            Diagram.ElementPanel.Refresh();
        }
    }
}
